using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class test_Game_Manager : MonoBehaviour 
{
    public static test_Game_Manager Instance;

    [Header("ステージ管理")]
    public int currentStage = 1;

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
