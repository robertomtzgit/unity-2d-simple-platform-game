using UnityEngine;
using UnityEngine.SceneManagement;

public class Trophy : MonoBehaviour
{
    public Animator trophyAnimator;
    private bool trophyTouched = false;
    [SerializeField] private AudioClip trophy;
    private UIManager uiManager;
    private PlayerMovement player;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !trophyTouched)
        {
            trophyAnimator.SetTrigger("WinTrigger");
            trophyTouched = true;
            SoundManager.instance.PlaySound(trophy);
            uiManager.ShowLevelCompleteMessage(); // Mostrar mensaje de nivel completado
            player.DisableControl(); // Desactivar control del jugador
            Invoke("LoadNextScene", 5f);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
