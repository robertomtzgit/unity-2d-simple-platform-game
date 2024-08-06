using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float score;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // El puntaje aumenta con el tiempo que pasa jugando el usuario
        //score += Time.deltaTime;
        textMesh.text = score.ToString("0");
    }

    public void SumScore(float scoreEntry)
    {
        score += scoreEntry;
    }
}
