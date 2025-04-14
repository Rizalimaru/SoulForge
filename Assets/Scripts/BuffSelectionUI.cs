using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffSelectionUI : MonoBehaviour
{
    [System.Serializable]
    public class BuffButton
    {
        public Button button;
        public Image icon;
        public Text title;
        public Text description;
    }

    public BuffButton[] buffButtons; // isi 4 data di inspector
    public BuffData[] availableBuffs; // list buff yang mungkin ditampilkan
    public PlayerData playerData;
    

    void Start()
    {
        DisplayRandomBuffs();
    }

    void DisplayRandomBuffs()
    {
        // Buat list sementara yang bisa dimodifikasi tanpa merusak data asli
        List<BuffData> tempBuffList = new List<BuffData>(availableBuffs);

        for (int i = 0; i < buffButtons.Length; i++)
        {
            if (tempBuffList.Count == 0)
            {
                Debug.LogWarning("Tidak cukup buff unik untuk mengisi semua tombol!");
                break;
            }

            // Pilih buff secara acak dari list sementara
            int randomIndex = Random.Range(0, tempBuffList.Count);
            BuffData buff = tempBuffList[randomIndex];

            // Tampilkan ke UI
            buffButtons[i].icon.sprite = buff.icon;
            buffButtons[i].title.text = buff.buffName;
            buffButtons[i].description.text = buff.description;

            int index = i; // simpan index untuk closure
            buffButtons[i].button.onClick.RemoveAllListeners();
            buffButtons[i].button.onClick.AddListener(() => SelectBuff(buff));

            // Hapus buff yang sudah dipakai agar tidak terulang
            tempBuffList.RemoveAt(randomIndex);
        }
    }

    void SelectBuff(BuffData selectedBuff)
    {
        Debug.Log("Selected Buff: " + selectedBuff.buffName);
        // Simpan ke player data / berikan efek
    }
}
