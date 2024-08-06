using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Text Settings")]
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    bool isPaused = false;

    void Update()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Método para pausar el tiempo
    public void PauseTimer()
    {
        isPaused = true;
    }

    // Método para reanudar el tiempo
    public void ResumeTimer()
    {
        isPaused = false;
    }
}
