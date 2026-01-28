
using UnityEngine;
using UnityEngine.SceneManagement;

public class test_Game_Manager : MonoBehaviour 
{
    public static test_Game_Manager Instance;

    public int currentStage = 1;

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

    // タイトルへ
    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
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
        SceneManager.LoadScene("Stage" + stageNumber);
    }

    // 次のステージへ
    public void NextStage()
    {
        currentStage++;
        SceneManager.LoadScene("Stage" + currentStage);
    }

    // リトライ
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
