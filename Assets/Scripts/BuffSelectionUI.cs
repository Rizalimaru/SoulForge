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
        public Text statSummary;
    }

    public BuffButton[] buffButtons; // Hanya isi 2 di inspector
    public List<BuffPairData> buffPairs; // Drag semua pasangan buff di sini
    public PlayerData playerData;
    public TritsData bigFivePersonalityData;

    [Header("Buff Data UI")]
    public TextMeshProUGUI Hpdata;
    public TextMeshProUGUI attackSpeedData;
    public TextMeshProUGUI movementSpdeedData;
    public TextMeshProUGUI HpRegenData;
    public TextMeshProUGUI pickRadiusData;

    [Header("UI")]
    public GameObject UIBuffSelection;

    BuffPairData currentPair;

    void Start()
    {
        DisplayRandomBuffPair();
    }

    void Update()
    {
        Hpdata.text = playerData.maxHP.ToString();
        attackSpeedData.text = playerData.attackDamage.ToString();
        movementSpdeedData.text = playerData.speed.ToString();
        HpRegenData.text = playerData.regen.ToString();
        pickRadiusData.text = playerData.pickRadius.ToString();
    }

    public void DisplayRandomBuffPair()
    {
        if (buffPairs.Count == 0) return;

        int randomIndex = Random.Range(0, buffPairs.Count);
        currentPair = buffPairs[randomIndex];

        BuffCardData[] cards = new BuffCardData[] { currentPair.buffA, currentPair.buffB };

        for (int i = 0; i < buffButtons.Length; i++)
        {
            BuffCardData selectedCard = cards[i];
            buffButtons[i].icon.sprite = selectedCard.icon;
            buffButtons[i].title.text = selectedCard.buffName;
            buffButtons[i].description.text = selectedCard.description;
            buffButtons[i].statSummary.text = selectedCard.statSummary;

            buffButtons[i].button.onClick.RemoveAllListeners();
            buffButtons[i].button.onClick.AddListener(() => SelectBuff(selectedCard));
        }
    }

    void SelectBuff(BuffCardData selectedBuff)
    {
        // Terapkan efek stat
        foreach (var effect in selectedBuff.effects)
        {
            switch (effect.statType)
            {
                case BuffStatType.Attack:
                    playerData.attackDamage += Mathf.RoundToInt(playerData.baseAttackDamage * effect.value / 100f);
                    break;
                case BuffStatType.Defense:
                    //playerData.defense += Mathf.RoundToInt(playerData.baseDefense * effect.value / 100f);
                    break;
                case BuffStatType.AttackSpeed:
                    playerData.attackSpeed -= playerData.baseAttackSpeed * (effect.value / 100f);
                    if (playerData.attackSpeed < 0.05f) playerData.attackSpeed = 0.05f;
                    break;
                case BuffStatType.Regen:
                    playerData.regen += Mathf.RoundToInt(playerData.baseRegen * effect.value / 100f);
                    break;
                case BuffStatType.HP:
                    playerData.maxHP += Mathf.RoundToInt(playerData.baseHP * effect.value / 100f);
                    playerData.currentHP = playerData.maxHP;
                    break;
                case BuffStatType.PickRadius:
                    playerData.pickRadius += effect.value;
                    break;
            }
        }

        // Terapkan efek trait
        foreach (var trait in selectedBuff.traitEffects)
        {
            switch (trait.traitType)
            {
                case TraitType.Extraversion: bigFivePersonalityData.Extraversion += trait.value; break;
                case TraitType.Conscientiousness: bigFivePersonalityData.Conscientiousness += trait.value; break;
                case TraitType.Agreeableness: bigFivePersonalityData.Agreeableness += trait.value; break;
                case TraitType.Neuroticism: bigFivePersonalityData.Neuroticism += trait.value; break;
                case TraitType.Openness: bigFivePersonalityData.Openness += trait.value; break;
            }
        }

        UIBuffSelection.SetActive(false);
        Time.timeScale = 1;
    }
}
