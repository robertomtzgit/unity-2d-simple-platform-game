using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerMainMenu : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private AudioClip mainMenuSound;

    [Header("Information")]
    [SerializeField] private GameObject informationScreen;

    [Header("BlackScreen")]
    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    private bool isMainMenuActive = true;

    private void Awake()
    {
        mainMenuScreen.SetActive(true);
        informationScreen.SetActive(false);
        FadeFromBlack();
    }

    private void Update()
    {
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

    #region Main Menu

    public void StartGame()
    {
        FadeToBlack();
        isMainMenuActive = false;
        // Cambia "1" al índice de la escena donde comienza tu juego
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        FadeToBlack();
        isMainMenuActive = true;
        SceneManager.LoadScene(0);
    }

    public void GameControls()
    {
        // Implementa la lógica de los controles del juego
    }

    public void Quit()
    {
        FadeToBlack();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
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
}
