using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class DialogData
{
    public string characterName;
    [TextArea(2, 5)]
    public string dialogText;
}

public class IntroHandler : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public float typeSpeed = 0.04f;
    private Animator deathAnimator; // Optional: if you want to control death animation
    public PlayerData playerData; // Referensi ke PlayerData
    public GameObject death;

    public List<DialogData> dialogs; // Isi di Inspector
    private int currentDialogIndex = 0;
    private Coroutine typingCoroutine;
    public GameObject enemySpawneManager;
    public GameObject primaryWeaponChoseManager;


    void Start()
    {   
        if(playerData.isintroEndlessComplated) // Cek apakah intro sudah selesai
        {
            CutSceneDone(); // Jika sudah, langsung panggil CutSceneDone
            return;
        }
        ShowDialog(0);
        enemySpawneManager.SetActive(false);
        primaryWeaponChoseManager.SetActive(false);
        deathAnimator = GameObject.FindGameObjectWithTag("Death").GetComponent<Animator>();
    }

    public void ShowDialog(int index)
    {   

        if (index < 0 || index >= dialogs.Count) return;
        currentDialogIndex = index;
        dialogPanel.SetActive(true);
        nameText.text = dialogs[index].characterName;
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeDialog(dialogs[index].dialogText));
    }

    IEnumerator TypeDialog(string text)
    {
        dialogText.text = "";
        foreach (char c in text)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    public void NextDialog()
    {
        if (currentDialogIndex + 1 < dialogs.Count)
        {
            ShowDialog(currentDialogIndex + 1);
        }
        else
        {

            StartCoroutine(dialogDone());
            
        }
    }

    IEnumerator dialogDone()
    {
        dialogPanel.SetActive(false);
        deathAnimator.SetTrigger("menghilang"); // Optional: trigger death animation
        yield return new WaitForSeconds(1f); // Tunggu animasi selesai
        enemySpawneManager.SetActive(true);
        primaryWeaponChoseManager.SetActive(true);
        playerData.isintroEndlessComplated = true; // Set status intro selesai
    }

    void CutSceneDone()
    {   
        death.SetActive(false); // Nonaktifkan objek death
        dialogPanel.SetActive(false);
        enemySpawneManager.SetActive(true);
        primaryWeaponChoseManager.SetActive(true);
    }

    void Update()
    {
        // Lanjut dialog jika panel aktif dan klik kiri ditekan
        if (dialogPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            NextDialog();
        }
    }
}
