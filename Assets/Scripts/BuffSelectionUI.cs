using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
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
    public List<BuffData> singleBuffs;   // Drag semua single buff di sini
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
    public PauseUiManager pauseUiManager; // Optional: jika ingin kontrol pause state

    [Header("Object Buff")]
    public List<GameObject> pairbuffObjects;

    BuffPairData currentPair;
    private int nextPairIndex = 0;
    private bool allPairsShown = false;
    private List<int> shownPairIndices = new List<int>();

    // Tambahan untuk single buff mode
    private bool showingSingleBuff = false;
    private BuffData[] currentSingleBuffs = new BuffData[2];

    void Start()
    {
        //DisplayRandomBuffPair();
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
        if (pauseUiManager != null)
            pauseUiManager.canPause = false; // Tidak bisa pause saat memilih buff

        if (allPairsShown)
        {
            DisplayRandomSingleBuffs();
            return;
        }

        if (buffPairs.Count == 0) return;

        int pairIndex;

        // Jika semua pair belum pernah ditampilkan, tampilkan berurutan
        if (!allPairsShown)
        {
            pairIndex = nextPairIndex;
            shownPairIndices.Add(pairIndex);
            nextPairIndex++;

            // Jika sudah semua, set flag agar berikutnya single buff
            if (nextPairIndex >= buffPairs.Count)
                allPairsShown = true;
        }
        else
        {
            // Tidak akan masuk sini karena sudah dicek di atas
            pairIndex = UnityEngine.Random.Range(0, buffPairs.Count);
        }

        currentPair = buffPairs[pairIndex];

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
        showingSingleBuff = false;
    }

    // Tampilkan 2 buff random dari singleBuffs
    void DisplayRandomSingleBuffs()
    {
        if (pauseUiManager != null)
            pauseUiManager.canPause = false; // Tidak bisa pause saat memilih buff

        if (singleBuffs.Count < 2) return;

        int idxA = UnityEngine.Random.Range(0, singleBuffs.Count);
        int idxB;
        do
        {
            idxB = UnityEngine.Random.Range(0, singleBuffs.Count);
        } while (idxB == idxA);

        currentSingleBuffs[0] = singleBuffs[idxA];
        currentSingleBuffs[1] = singleBuffs[idxB];

        for (int i = 0; i < buffButtons.Length; i++)
        {
            BuffData selectedBuff = currentSingleBuffs[i];
            buffButtons[i].icon.sprite = selectedBuff.icon;
            buffButtons[i].title.text = selectedBuff.buffName;
            buffButtons[i].description.text = selectedBuff.description;
            buffButtons[i].statSummary.text = ""; // Atur jika ingin ringkasan stat

            buffButtons[i].button.onClick.RemoveAllListeners();
            buffButtons[i].button.onClick.AddListener(() => SelectSingleBuff(selectedBuff));
        }
        showingSingleBuff = true;
    }

    void SelectBuff(BuffCardData selectedBuff)
    {   
        // Terapkan efek stat umum
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

        // === Tempat untuk efek khusus berdasarkan nama buff atau tipe buff ===
        switch (selectedBuff.buffName)
        {
            case "Mind Lock":
                pairbuffObjects[0].SetActive(true);
                break;
            case "Predator’s Edge":
                pairbuffObjects[1].SetActive(true);
                break;
            case "Clockwork Routine":
                pairbuffObjects[2].SetActive(true);
                break;
            case "Fractured Nerves":
                pairbuffObjects[3].SetActive(true);
                break;
            case "Phantom Ally":
                pairbuffObjects[4].SetActive(true);
                break;
            case "Lone Fang":
                pairbuffObjects[5].SetActive(true);
                break;
        }
        // === Akhir efek khusus ===

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
        Debug.Log($"Extraversion: {bigFivePersonalityData.Extraversion}, Conscientiousness: {bigFivePersonalityData.Conscientiousness}, ...");

        // Setelah pilih, tampilkan buff berikutnya
        if (!allPairsShown)
            DisplayRandomBuffPair();
        else
            DisplayRandomSingleBuffs();

        // Hide UI setelah memilih buff
        UIBuffSelection.SetActive(false);
        Time.timeScale = 1f;

        if (pauseUiManager != null)
            pauseUiManager.canPause = true; // Bisa pause lagi setelah memilih buff
    }

    // Untuk single buff
    void SelectSingleBuff(BuffData selectedBuff)
    {
        // Terapkan efek stat
        switch (selectedBuff.buffType)
        {
            case BuffType.Attack:
                playerData.attackDamage += Mathf.RoundToInt(playerData.baseAttackDamage * selectedBuff.value / 100f);
                break;
            case BuffType.Defense:
                //playerData.defense += selectedBuff.value;
                break;
            case BuffType.MoveSpeed:
                playerData.speed += Mathf.RoundToInt(playerData.baseSpeed * selectedBuff.value / 100f);
                break;
            case BuffType.Regen:
                playerData.regen += Mathf.RoundToInt(playerData.baseRegen * selectedBuff.value / 100f);
                break;
            case BuffType.HP:
                playerData.maxHP += Mathf.RoundToInt(playerData.baseHP * selectedBuff.value / 100f);
                playerData.currentHP = playerData.maxHP;
                break;
            case BuffType.pickRadius:
                playerData.pickRadius += Mathf.RoundToInt(playerData.basePickRadius * selectedBuff.value / 100f);
                break;
            case BuffType.AtkSpeed:
                playerData.attackSpeed -= selectedBuff.value;
                if (playerData.attackSpeed < 0.05f) playerData.attackSpeed = 0.05f;
                break;
        }

        // Terapkan efek trait
        if (selectedBuff.traitEffects != null)
        {
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
        }

        // Setelah pilih, tampilkan buff single lagi
        DisplayRandomSingleBuffs();

        // Hide UI setelah memilih buff
        UIBuffSelection.SetActive(false);
        Time.timeScale = 1f;

        if (pauseUiManager != null)
            pauseUiManager.canPause = true; // Bisa pause lagi setelah memilih buff
    }
}
