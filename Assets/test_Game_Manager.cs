using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class test_Game_Manager : MonoBehaviour 
{
    public static test_Game_Manager Instance;

    [Header("ステージ管理")]
    public int currentStage = 1;

    [Header("バウンド管理")]
    [SerializeField] private int maxHitCount = 3;   // ステージから上書きされる
    private int hitCount = 0;
    private bool isGameOver = false;

    [Header("UI")]
    [SerializeField] private Text hitText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject goalUI;
    /// <summary>
    /// 初期化
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン切り替えでも残す
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ===== ステージから呼ばれる設定 =====
    /// </summary>

    // ★ StageManager から maxHitCount を渡す用
    public void SetMaxHitCount(int value)
    {
        maxHitCount = value;
        ResetHitCount();
        UpdateHitUI();
    }

    /// <summary>
    /// ===== ヒット管理 =====
    /// </summary>

    public void AddHitCount()
    {
        if (isGameOver) return;

        hitCount++;
        UpdateHitUI();

        if (hitCount > maxHitCount)
        {
            GameOver();
        }
    }

    public void ResetHitCount()
    {
        hitCount = 0;
        isGameOver = false;
        Time.timeScale = 1f;
    }

    public int GetHitCount() => hitCount;
    public int GetMaxHitCount() => maxHitCount;

    /// <summary>
    /// ===== UI & 状態 =====
    /// </summary>

    void UpdateHitUI()
    {
        if (hitText != null)
        {
            hitText.text = hitCount + " / " + maxHitCount;
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }

    public void ShowGoalUI()
    {
        isGameOver = true;
        Time.timeScale = 0f;

        if (goalUI != null)
            goalUI.SetActive(true);
    }
    /// <summary>
    /// シーン遷移
    /// </summary>

    // タイトルへ
    public void GoTitle()
    {
        SceneManager.LoadScene("test_Title");
    }

    // ステージセレクトへ
    public void GoStageSelect()
    {
        SceneManager.LoadScene("test_select");
    }

    // ステージ開始
    public void StartStage(int stageNumber)
    {
        currentStage = stageNumber;
        SceneManager.LoadScene("test_Stage" + stageNumber);
    }

    // 次のステージへ
    public void NextStage()
    {
        currentStage++;
        SceneManager.LoadScene("test_Stage" + currentStage);
    }

    // リトライ
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
