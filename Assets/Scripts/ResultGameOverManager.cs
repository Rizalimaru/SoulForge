using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultGameOverManager : MonoBehaviour
{
    public PlayerData playerData;
    public GameObject resultPanel;
    public TextMeshProUGUI scoreText;
    public TritsData traitsData;
    public TextMeshProUGUI feedbackText;
    public GameSessionResult sessionResult;
    public TimeElapse timeElapse; // Drag komponen TimeElapse ke sini di Inspector
    private bool isGameOver = true;


    void Update()
    {
        if (playerData.currentHP <= 0 && isGameOver)
        {   
            isGameOver = false; // Prevent multiple triggers
            ShowResultPanel();
        }
    }
    public void ShowResultPanel()
    {
        resultPanel.SetActive(true);
        scoreText.text = playerData.scoreInStage.ToString();
        feedbackText.text = GetFeedback();
        // Simpan hasil session
        SaveSessionResult(timeElapse != null ? timeElapse.timeElapsed : 0f);
        Time.timeScale = 0; // Pause the game
    }
    
    public string GetFeedback()
    {
        return
            DescribeExtraversion(traitsData.Extraversion) + " " +
            DescribeOpenness(traitsData.Openness) + " " +
            DescribeConscientiousness(traitsData.Conscientiousness) + " " +
            DescribeAgreeableness(traitsData.Agreeableness) + " " +
            DescribeNeuroticism(traitsData.Neuroticism);
    }

    string DescribeExtraversion(int score)
    {
        if (score <= 4) return "Kamu tampak lebih tertutup dan cenderung bermain aman, menghindari risiko besar.";
        else if (score <= 7) return "Biasanya kamu memilih taktik jarak jauh atau efek debuff untuk tetap mengontrol situasi.";
        else if (score <= 10) return "Gaya bermainmu cukup fleksibel, bisa agresif atau defensif tergantung kondisi.";
        else if (score <= 13) return "Kamu suka tantangan dan sering berada di tengah aksi, mengambil peran yang lebih aktif.";
        else return "Tanpa ragu, kamu langsung maju ke garis depan dengan gaya bermain yang sangat agresif dan cepat.";
    }

    string DescribeOpenness(int score)
    {
        if (score <= 4) return "Kamu lebih nyaman dengan pendekatan yang sudah terbukti, jarang mencoba hal baru.";
        else if (score <= 7) return "Biasanya kamu tetap pada strategi yang sudah familiar, meskipun terbuka sedikit pada variasi.";
        else if (score <= 10) return "Kamu cukup terbuka untuk mencoba hal baru, tapi tetap punya gaya bermain andalan.";
        else if (score <= 13) return "Ada dorongan untuk bereksperimen dan menciptakan strategi unik dalam setiap sesi permainan.";
        else return "Kreativitasmu menonjol — kamu sering membuat kombinasi build yang tidak biasa tapi efektif.";
    }

    string DescribeConscientiousness(int score)
    {
        if (score <= 4) return "Kamu sering membuat keputusan spontan tanpa banyak perencanaan.";
        else if (score <= 7) return "Meskipun kadang improvisatif, kamu tetap memperhitungkan pilihanmu dengan cukup baik.";
        else if (score <= 10) return "Setiap pilihanmu cukup dipertimbangkan, menunjukkan perencanaan yang matang.";
        else if (score <= 13) return "Strategi dan detail menjadi kekuatanmu — kamu berpikir beberapa langkah ke depan.";
        else return "Kedisiplinanmu luar biasa, dan kamu selalu punya rencana yang rapi serta efisien untuk menghadapi tantangan.";
    }

    string DescribeAgreeableness(int score)
    {
        if (score <= 4) return "Fokusmu lebih ke perkembangan pribadi dibanding kerja sama tim.";
        else if (score <= 7) return "Kamu bisa bekerja sama, tapi masih lebih mengutamakan kekuatan sendiri.";
        else if (score <= 10) return "Kamu mampu menyesuaikan diri dalam tim dan memilih buff yang juga berguna bagi yang lain.";
        else if (score <= 13) return "Ada kepedulian kuat terhadap tim; kamu sering mengambil peran yang mendukung.";
        else return "Kerja sama adalah prioritas utamamu — kamu memilih buff dan senjata yang menjaga dan memperkuat tim.";
    }

    string DescribeNeuroticism(int score)
    {
        if (score <= 4) return "Kamu tetap tenang bahkan dalam tekanan, tidak mudah goyah oleh situasi sulit.";
        else if (score <= 7) return "Secara umum kamu stabil, walau kadang keputusan diambil terlalu cepat saat genting.";
        else if (score <= 10) return "Kamu cukup bisa mengendalikan tekanan dan mengambil tindakan dengan kepala dingin.";
        else if (score <= 13) return "Ada kecenderungan untuk berjaga-jaga dengan memilih buff bertahan atau penyembuhan.";
        else return "Kamu mudah merasa tertekan, dan sering memilih buff pertahanan untuk menghindari risiko langsung.";
    }

    public void GoToMainMenu()
    {
        // Logika untuk kembali ke menu utama
        Debug.Log("Kembali ke menu utama");
        Time.timeScale = 1; // Resume the game time
        resultPanel.SetActive(false); // Sembunyikan panel hasil
        SceneManager.LoadScene("MainMenu"); // Ganti dengan nama scene menu utama yang sesuai
        isGameOver = true; // Reset game over state
    }
    void SaveSessionResult(float timeElapsed)
    {   

        sessionResult.SaveResult(
            timeElapsed,
            playerData.scoreInStage,
            traitsData
        );
    }
}
