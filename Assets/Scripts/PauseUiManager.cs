using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUiManager : MonoBehaviour
{
    public GameObject pauseUI;
    public bool canPause = true;

    void Start()
    {
        pauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (!pauseUI.activeSelf)
            {
                pauseUI.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
                pauseUI.SetActive(false);
            }
        }
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }

    public void ExitButton()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        // Assuming you want to load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
