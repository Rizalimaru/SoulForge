using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopUI : MonoBehaviour
{
    [System.Serializable]
    public class ShopButton
    {
        public Button button;
        public Image icon;
        public TextMeshProUGUI priceText;
        public TextMeshProUGUI levelText;
    }

    public ShopButton[] shopButtons; // Isi 6 di Inspector
    public ShopUpgradeData[] upgrades; // Isi 6 di Inspector, urut sesuai stat
    public PlayerData playerData;
    public TextMeshProUGUI descTextGlobal; // Drag TMP UI di bawah shop ke sini
    public TextMeshProUGUI coinText; // Drag TMP UI di bawah shop ke sini
    public GameObject descriptionPanel; // Drag panel shop ke sini

    private int[] upgradeLevels
    {
        get { return playerData.shopUpgradeLevels; }
        set { playerData.shopUpgradeLevels = value; }
    }

    void Start()
    {   
        coinText.text = playerData.coin.ToString();
        RefreshUI();
        for (int i = 0; i < shopButtons.Length; i++)
        {
            int idx = i;
            shopButtons[i].button.onClick.AddListener(() => BuyUpgrade(idx));

            // Tambahkan event hover
            EventTrigger trigger = shopButtons[i].button.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = shopButtons[i].button.gameObject.AddComponent<EventTrigger>();

            // Pointer Enter
            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((eventData) => ShowDescription(idx));
            trigger.triggers.Add(entryEnter);

            // Pointer Exit (optional: clear desc)
            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((eventData) => ClearDescription());
            trigger.triggers.Add(entryExit);
        }
        ClearDescription();
    }

    void RefreshUI()
    {
        for (int i = 0; i < shopButtons.Length; i++)
        {
            var data = upgrades[i];
            int lvl = upgradeLevels[i];
            shopButtons[i].icon.sprite = data.icon;
            if (lvl < data.levels.Length)
            {
                shopButtons[i].priceText.text = data.levels[lvl].price.ToString();
                shopButtons[i].levelText.text = $"Lv.{lvl + 1}";
                shopButtons[i].button.interactable = true;
            }
            else
            {
                shopButtons[i].priceText.text = "-";
                shopButtons[i].levelText.text = "Lv.MAX";
                shopButtons[i].button.interactable = false;
            }
        }
    }

    void BuyUpgrade(int idx)
    {
        var data = upgrades[idx];
        int lvl = upgradeLevels[idx];
        if (lvl >= data.levels.Length) return;
        int price = data.levels[lvl].price;
        if (playerData.coin < price) return;

        playerData.coin -= price;
        ApplyUpgrade(data.statType, data.levels[lvl].value);
        upgradeLevels[idx]++;
        RefreshUI();
        ShowDescription(idx); // Update deskripsi setelah upgrade
        coinText.text = playerData.coin.ToString();
    }

    void ApplyUpgrade(StatType stat, float value)
    {
        switch (stat)
        {
            case StatType.HP: playerData.maxHP += value; break;
            case StatType.Attack: playerData.attackDamage += value; break;
            case StatType.PickRadius: playerData.pickRadius += value; break;
            case StatType.MovementSpeed: playerData.speed += value; break;
            case StatType.HPRegen: playerData.regen += value; break;
            case StatType.AttackSpeed: playerData.attackSpeed -= value; if (playerData.attackSpeed < 0.05f) playerData.attackSpeed = 0.05f; break;
        }
    }

    void ShowDescription(int idx)
    {   
        
        descriptionPanel.SetActive(true);
        var data = upgrades[idx];
        int lvl = upgradeLevels[idx];
        if (lvl < data.levels.Length)
            descTextGlobal.text = data.levels[lvl].description;
        else
            descTextGlobal.text = "Upgrade MAX";
    }

    void ClearDescription()
    {   
        descriptionPanel.SetActive(false);
        descTextGlobal.text = "";
    }
}
