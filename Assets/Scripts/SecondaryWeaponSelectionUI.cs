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
    public TritsData traitsData; // Data kepribadian

    void Start()
    {
        DisplayRandomWeapons(); // Tampilkan senjata secara acak saat awal
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
        {
            // Di sini bisa dipanggil sistem lain jika semua senjata sudah max
            // Contoh: Tampilkan UI lain, dsb.
        }
        else
        {
            DisplayRandomWeapons();
        }

        UIWeaponSelection.SetActive(false);
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
