using UnityEngine;

public class Fruits : MonoBehaviour
{
    [SerializeField] private float scoreNumber;
    [SerializeField] private Score score;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            score.SumScore(scoreNumber);
            SoundManager.instance.PlaySound(pickupSound);
            LoadingManager.Instance.GamePoints(scoreNumber);
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject, 0.5f);
        }
    }
}
