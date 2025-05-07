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

    public WeaponButton[] weaponButtons;
    public List<SecondaryWeaponData> availableWeapons; // List senjata yang mungkin ditampilkan
    public List<SecondaryWeaponData> selectedWeapons = new List<SecondaryWeaponData>(); // List senjata yang sudah dipilih
    public List<GameObject> weaponObjects;
    public GameObject UIWeaponSelection;
    public BuffSelectionUI selectionUI;
    public GameObject secondaryWeaponSelectionUI;

    void Start()
    {
        //DisplayRandomWeapons(); // Tampilkan senjata secara acak saat awal
        
    }
    public void DisplayRandomWeapons()
    {
        // Jika semua senjata sudah level maksumal, tampilkan buff selection
        if (selectedWeapons.Count == 3 && AllWeaponsMaxLevel())
        {
            DisplayBuffSelection();
            return;
        }

        //List untuk randomisasi
        List<SecondaryWeaponData> tempWeaponList = new List<SecondaryWeaponData>();

        //jika belum mencapai 3 senjata, tambahkan senjata level 1
        if (selectedWeapons.Count < 3)
        {
            foreach (var weapon in availableWeapons)
            {
                if(weapon.level == 1 && !selectedWeapons.Contains(weapon))
                {
                    tempWeaponList.Add(weapon);
                }
            }
        }else
        {
            // jika sudah 3 senjata, hanya tampilkan senjata yang sudah dipilih untuk upgrade
            foreach (var weapon in selectedWeapons)
            {
                if(weapon.level < weapon.maxLevel)
                {
                    tempWeaponList.Add(weapon);
                }
            }
        }

        //randomisasi senjata untuk ditampilkan 
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            if(tempWeaponList.Count == 0)
            {
                Debug.LogWarning("Tidak ada senjata yang tersedia untuk ditampilkan!");
                break;
            }

            int randomIndex = Random.Range(0, tempWeaponList.Count);
            SecondaryWeaponData weapon = tempWeaponList[randomIndex];

            //Tampilkan senjata ke UI
            weaponButtons[i].Icon.sprite = weapon.imageIcon;
            weaponButtons[i].weaponName.text = weapon.weaponName;
            weaponButtons[i].description.text = weapon.description;

            int index = i; // Simpan index untuk closeure
            weaponButtons[i].button.onClick.RemoveAllListeners();
            weaponButtons[i].button.onClick.AddListener(() => SelectWeapon(weapon));

            //hapus senjata dari list sementara
            tempWeaponList.RemoveAt(randomIndex);

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

    void SelectWeapon(SecondaryWeaponData selectedWeapon)
    {
        Debug.Log("Selected Weapon: " + selectedWeapon.weaponName);

        // Jika senjata belum dipilih, tambahkan ke daftar senjata yang dipilih
        if (!selectedWeapons.Contains(selectedWeapon))
        {
            selectedWeapons.Add(selectedWeapon);

            // Aktifkan GameObject senjata jika pertama kali diambil
            foreach (GameObject weaponObject in weaponObjects)
            {
                if (weaponObject.name == selectedWeapon.weaponName)
                {
                    weaponObject.SetActive(true);
                    break;
                }
            }
        }
        else
        {
            // Jika senjata sudah dipilih sebelumnya, tingkatkan statistiknya
            Debug.Log("Upgrading Weapon: " + selectedWeapon.weaponName);
        }

        // Tingkatkan level senjata
        selectedWeapon.level++;

        // Jika semua senjata sudah level maksimal, tampilkan buff selection
        if (selectedWeapons.Count == 3 && AllWeaponsMaxLevel())
        {
            DisplayBuffSelection();
        }
        else
        {
            // Tampilkan pilihan senjata berikutnya
            DisplayRandomWeapons();
        }

        // Nonaktifkan UI Weapon Selection setelah senjata dipilih
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
            case BuffType.Speed:
                selectionUI.playerData.speed += selectedBuff.value;
                break;
            case BuffType.Healing:
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
        foreach(var weapon in selectedWeapons)
        {
            if(weapon.level < weapon.maxLevel)
            {
                return false;
            }
        }
        return true;
    }
}
