using System.Collections.Generic;
using TMPro;
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

    [Header("Buff Data UI")]
    public TextMeshProUGUI Hpdata;
    public TextMeshProUGUI attackSpeedData;
    public TextMeshProUGUI movementSpdeedData;
    public TextMeshProUGUI HpRegenData;
    public TextMeshProUGUI pickRadiusData;

    [Header("UI")]
    public GameObject UIBuffSelection;

    void Start()
    {
        DisplayRandomBuffs();
    }

    void Update()
    {
        Hpdata.text = playerData.maxHP.ToString();
        attackSpeedData.text = playerData.attackDamage.ToString();
        movementSpdeedData.text = playerData.speed.ToString();
        HpRegenData.text = playerData.regen.ToString();
        pickRadiusData.text = playerData.pickRadius.ToString();
    }

    public void DisplayRandomBuffs()
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
        
        // Tambahkan efek buff ke PlayerData berdasarkan jenis buff
        switch (selectedBuff.buffType)
        {
            case BuffType.Attack:
                playerData.attackDamage += Mathf.RoundToInt(playerData.baseAttackDamage * selectedBuff.value / 100f);
                UIBuffSelection.SetActive(false); // Menyembunyikan UI Buff Selection
                Time.timeScale = 1; // Resume game setelah buff dipilih
                break;
            case BuffType.Speed:
                playerData.speed += Mathf.RoundToInt(playerData.baseSpeed * selectedBuff.value / 100f);
                UIBuffSelection.SetActive(false); // Menyembunyikan UI Buff Selection
                Time.timeScale = 1; // Resume game setelah buff dipilih
                break;
            // case BuffType.Healing:
            //     playerData.health = Mathf.Min(playerData.maxHealth, playerData.health + selectedBuff.value);
            //     break;
            case BuffType.HP:
                playerData.maxHP += Mathf.RoundToInt(playerData.baseHP * selectedBuff.value / 100f);
                playerData.currentHP = playerData.maxHP; // Set HP saat ini ke max HP setelah buff
                UIBuffSelection.SetActive(false); // Menyembunyikan UI Buff Selection
                Time.timeScale = 1; // Resume game setelah buff dipilih
                break;
            case BuffType.pickRadius:
                playerData.pickRadius += selectedBuff.value;
                break;
            default:
                Debug.LogWarning("Buff type not handled: " + selectedBuff.buffType);
                break;
        }

        // Tambahkan log untuk memverifikasi perubahan
        Debug.Log("Player Data Updated: " + playerData);
        }
}
