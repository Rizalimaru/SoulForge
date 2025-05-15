using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryWeaponSelectionUI : MonoBehaviour
{
    [System.Serializable]
    public class WeaponButton
    {
        public Button button;
        public Image Icon;
        public Text weaponName;
        public Text description;
    }

    [System.Serializable]
    public class WeaponEntry
    {
        public SecondaryWeaponData weaponData;
        public GameObject weaponObject;
        public int currentLevel = 0; // Mulai dari level 0 (belum punya/level awal)
    }

    public WeaponButton[] weaponButtons;
    public GameObject UIWeaponSelection;
    public BuffSelectionUI selectionUI;
    public GameObject secondaryWeaponSelectionUI;
    public List<WeaponEntry> weaponEntries; // Drag WeaponData dan GameObject di Inspector

    void Start()
    {
        //DisplayRandomWeapons(); // Tampilkan senjata secara acak saat awal
    }

    public void DisplayRandomWeapons()
    {
        List<WeaponEntry> selectableWeapons = new List<WeaponEntry>();

        // Pilih senjata yang belum diambil (level 0) atau yang bisa di-upgrade
        foreach (var entry in weaponEntries)
        {
            if (entry.currentLevel < entry.weaponData.levels.Count)
            {
                selectableWeapons.Add(entry);
            }
        }

        for (int i = 0; i < weaponButtons.Length; i++)
        {
            if (selectableWeapons.Count == 0)
            {
                weaponButtons[i].button.interactable = false;
                weaponButtons[i].weaponName.text = "No Weapon";
                weaponButtons[i].description.text = "";
                continue;
            }

            int randomIndex = Random.Range(0, selectableWeapons.Count);
            WeaponEntry entry = selectableWeapons[randomIndex];

            // Next level info
            int nextLevel = entry.currentLevel;
            WeaponLevelData nextLevelData = entry.weaponData.levels[nextLevel];

            weaponButtons[i].Icon.sprite = entry.weaponData.imageIcon;
            weaponButtons[i].weaponName.text = $"{entry.weaponData.weaponName} ({nextLevelData.level})";
            weaponButtons[i].description.text = nextLevelData.description;

            weaponButtons[i].button.onClick.RemoveAllListeners();
            weaponButtons[i].button.onClick.AddListener(() => SelectWeapon(entry));

            selectableWeapons.RemoveAt(randomIndex);
        }
    }

    void DisplayBuffSelection()
    {
        // Gunakan weaponButtons untuk menampilkan buff
        BuffData[] availableBuffs = selectionUI.availableBuffs; // Ambil buff dari BuffSelectionUI
        List<BuffData> tempBuffList = new List<BuffData>(availableBuffs);

        for (int i = 0; i < weaponButtons.Length; i++)
        {
            if (tempBuffList.Count == 0)
            {
                Debug.LogWarning("Tidak ada buff yang tersedia untuk ditampilkan!");
                break;
            }

            int randomIndex = Random.Range(0, tempBuffList.Count);
            BuffData buff = tempBuffList[randomIndex];

            // Tampilkan buff ke UI
            weaponButtons[i].Icon.sprite = buff.icon;
            weaponButtons[i].weaponName.text = buff.buffName;
            weaponButtons[i].description.text = buff.description;

            int index = i; // Simpan index untuk closure
            weaponButtons[i].button.onClick.RemoveAllListeners();
            weaponButtons[i].button.onClick.AddListener(() => SelectBuff(buff));

            // Hapus buff dari list sementara
            tempBuffList.RemoveAt(randomIndex);
        }
    }

    void SelectWeapon(WeaponEntry entry)
    {
        // Aktifkan GameObject jika baru diambil (saat naik ke level 1)
        if (entry.currentLevel == 0 && entry.weaponObject != null)
            entry.weaponObject.SetActive(true);

        // Upgrade level jika belum maksimal
        if (entry.currentLevel < entry.weaponData.levels.Count)
            entry.currentLevel++;

        // Update deskripsi pada data utama
        entry.weaponData.description = entry.weaponData.levels[entry.currentLevel - 1].description;

        // Tampilkan pilihan berikutnya atau buff jika semua sudah max
        if (AllWeaponsMaxLevel())
            DisplayBuffSelection();
        else
            DisplayRandomWeapons();

        UIWeaponSelection.SetActive(false);
    }

    void SelectBuff(BuffData selectedBuff)
    {
        Debug.Log("Selected Buff: " + selectedBuff.buffName);

        // Terapkan efek buff ke playerData
        switch (selectedBuff.buffType)
        {
            case BuffType.Attack:
                selectionUI.playerData.attackDamage += selectedBuff.value;
                break;
            case BuffType.Defense:
                //selectionUI.playerData.defense += selectedBuff.value;
                break;
            case BuffType.MoveSpeed:
                selectionUI.playerData.speed += selectedBuff.value;
                break;
            case BuffType.Regen:
                //selectionUI.playerData.Heal(selectedBuff.value);
                break;
            case BuffType.HP:
                selectionUI.playerData.maxHP += selectedBuff.value;
                break;
            case BuffType.pickRadius:
                selectionUI.playerData.pickRadius += selectedBuff.value;
                break;
        }

        // Tampilkan pilihan berikutnya
        DisplayBuffSelection();
    }

    bool AllWeaponsMaxLevel()
    {
        foreach (var entry in weaponEntries)
        {
            if (entry.currentLevel < entry.weaponData.levels.Count)
                return false;
        }
        return true;
    }
}
