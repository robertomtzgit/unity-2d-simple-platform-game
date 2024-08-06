using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb2D;
    public Transform player;
    private bool mirandoDerecha = true;

    [Header("Attack")]
    [SerializeField] private Transform attackController;
    [SerializeField] private float attackRatio;
    [SerializeField] private float attackDamage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("playerDistance", playerDistance);
    }

    public void FacePlayer()
    {
        if ((player.position.x > transform.position.x && mirandoDerecha) || (player.position.x < transform.position.x && !mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    public void Attack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(attackController.position, attackRatio);

        foreach (Collider2D collision in objects)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackController.position, attackRatio);
    }
}
