using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Pastikan namespace ini ditambahkan

public class MainMenuButton : MonoBehaviour
{
    private Animator aniCamera;
    public GameObject descrptionButtonPanel;
    public TextMeshProUGUI descriptionText;

    public string[] buttonDescriptions; // Isi di Inspector
    public Button[] menuButtons;        // Isi di Inspector

    void Start()
    {
        aniCamera = GameObject.FindWithTag("MainCamera").GetComponent<Animator>();

        for (int i = 0; i < menuButtons.Length; i++)
        {
            int idx = i;
            EventTrigger trigger = menuButtons[i].gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = menuButtons[i].gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((eventData) => ShowDescription(idx));
            trigger.triggers.Add(entryEnter);

            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((eventData) => ClearDescription());
            trigger.triggers.Add(entryExit);
        }
    }

    public void goToShop()
    {
        aniCamera.SetTrigger("goToShop");
    }

    public void shopToMainMenu()
    {
        aniCamera.SetTrigger("shopToMainMenu");
    }

    public void goToRecord()
    {
        aniCamera.SetTrigger("goToRecord");
    }

    public void recordToMainMenu()
    {
        aniCamera.SetTrigger("recordToMainMenu");
    }

    public void goToEndless()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    void ShowDescription(int idx)
    {
        if (buttonDescriptions != null && idx < buttonDescriptions.Length)
        {
            descrptionButtonPanel.SetActive(true);
            descriptionText.text = buttonDescriptions[idx];
        }
    }

    void ClearDescription()
    {
        descrptionButtonPanel.SetActive(false);
        descriptionText.text = "";
    }
}
