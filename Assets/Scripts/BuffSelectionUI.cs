using UnityEngine;
using UnityEngine.UI;

public class BuffSelectionUI : MonoBehaviour
{
    [System.Serializable]
    public class BuffButton
    {
        public Button button;
        public Image icon;
        public Text description;
    }

    public BuffButton[] buffButtons; // isi 4 data di inspector
    public BuffData[] availableBuffs; // list buff yang mungkin ditampilkan

    void Start()
    {
        DisplayRandomBuffs();
    }

    void DisplayRandomBuffs()
    {
        // Misalnya pilih 4 buff acak dari list
        for (int i = 0; i < buffButtons.Length; i++)
        {
            BuffData buff = availableBuffs[Random.Range(0, availableBuffs.Length)];
            buffButtons[i].icon.sprite = buff.icon;
            buffButtons[i].description.text = buff.description;

            int index = i; // simpan index untuk closure
            buffButtons[i].button.onClick.RemoveAllListeners();
            buffButtons[i].button.onClick.AddListener(() => SelectBuff(buff));
        }
    }

    void SelectBuff(BuffData selectedBuff)
    {
        Debug.Log("Selected Buff: " + selectedBuff.buffName);
        // Simpan ke player data / berikan efek
    }
}
