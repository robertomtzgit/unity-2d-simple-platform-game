using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class MovingPlatformFixed : MonoBehaviour
{
    public float speed = 0.5f;

    private float waitTime;

    public Transform[] moveSpots;

    public float startWaitTime = 2;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.1f)
        {
            if(moveSpots[i] != moveSpots[moveSpots.Length - 1])
            {
                i++;
            }
            else
            {
                i = 0;
            }

            waitTime = startWaitTime;

        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    // This function makes the player move with the platform at the same time
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    //Exits the up function
    private void OnCollisionExit2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
