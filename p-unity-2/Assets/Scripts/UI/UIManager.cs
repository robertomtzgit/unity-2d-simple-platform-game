using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("BlackScreen")]
    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    [Header("Level Complete")]
    public GameObject levelCompleteMessage;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        levelCompleteMessage.SetActive(false);
    }

    void Start()
    {
        FadeFromBlack();
    }

    private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //If pause screen already active unpause and viceversa
                if (pauseScreen.activeInHierarchy)
                    PauseGame(false);
                else
                    PauseGame(true);
            }

            if (shouldFadeToBlack)
            {
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
                if (fadeScreen.color.a == 1f)
                {
                    shouldFadeToBlack = false;
                }
            }

            if (shouldFadeFromBlack)
            {
                fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
                if (fadeScreen.color.a == 0f)
                {
                    shouldFadeFromBlack = false;
                }
            }
        }

    #region Game Over Functions
    //Game over function
    public void GameOver()
    {
        FadeFromBlack();
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    //Restart level
    public void Restart()
    {
        FadeFromBlack();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Activate game over screen
    public void MainMenu()
    {
        SceneManager.LoadScene("_MainMenu");
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
        #endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        //If status == true pause | if status == false unpause
        pauseScreen.SetActive(status);

        //When pause status is true change timescale to 0 (time stops)
        //when it's false change it back to 1 (time goes by normally)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }

    // Método para activar el mensaje de nivel completado
    public void ShowLevelCompleteMessage()
    {
        levelCompleteMessage.SetActive(true);
    }

    // Método para desactivar el mensaje de nivel completado
    public void HideLevelCompleteMessage()
    {
        levelCompleteMessage.SetActive(false);
    }

    public bool IsFading()
    {
        // Devuelve true si la pantalla se está desvaneciendo, de lo contrario, devuelve false
        return shouldFadeToBlack || shouldFadeFromBlack;
    }
}