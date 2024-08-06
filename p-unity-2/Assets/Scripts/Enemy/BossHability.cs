using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHability : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] Vector2 boxDimencions;
    [SerializeField] private Transform boxPosition;
    [SerializeField] private float lifeTime;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Punch()
    {
        Collider2D[] objects = Physics2D.OverlapBoxAll(boxPosition.position, boxDimencions, 0f);

        foreach (Collider2D collision in objects)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxPosition.position, boxDimencions);
    }
}
