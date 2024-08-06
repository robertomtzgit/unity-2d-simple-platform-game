using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance;
    [SerializeField] private float scorePoints;
    public Text scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GamePoints(float points)
    {
        scorePoints += points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Total Score: " + scorePoints.ToString();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Busca el objeto de texto por su etiqueta en la escena cargada
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        UpdateScoreText();
    }

    private void Update()
    {
        // Esto es solo para probar, puede que quieras removerlo más adelante
        if (Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene(1);
    }
}
