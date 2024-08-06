using UnityEngine;
using System.Collections;

public enum CharacterType
{
    Player,
    Enemy
}

public class Health : MonoBehaviour
{
    [Header("CharacterType")]
    [SerializeField] private CharacterType characterType;

    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    // Reference to UIManager
    private UIManager uiManager;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        // Get the UIManager reference
        uiManager = FindObjectOfType<UIManager>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable || dead) return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                if (characterType == CharacterType.Player)
                {
                    // Dead: Trigger fade to black only if not already fading
                    if (!uiManager.IsFading())
                    {
                        uiManager.FadeToBlack();
                        dead = true;
                        SoundManager.instance.PlaySound(deathSound);
                    }
                }
                else if (characterType == CharacterType.Enemy)
                {
                    // Keep the enemy's death animation and sound playing
                    dead = true;
                    SoundManager.instance.PlaySound(deathSound);
                    StartCoroutine(DeactivateAfterDeath());
                }
            }
        }
    }

    private IEnumerator DeactivateAfterDeath()
    {
        // Esperar la duración de la animación de muerte
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 0.1f);

        // Desactivar el objeto enemigo
        gameObject.SetActive(false);

        // Desactivar el componente EnemyPatrol si está presente
        EnemyPatrol enemyPatrol = GetComponent<EnemyPatrol>();
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = false;
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    // Respawn
    public void Respawn()
    {
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(Invulnerability());
        dead = false;

        // Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }
}
