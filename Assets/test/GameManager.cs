using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    // ===== シングルトン =====
    public static GameManager Instance { get; private set; }

    // ===== ステージ管理 =====
    public int CurrentStageIndex { get; private set; }

    // ===== イベント =====
    public static event System.Action OnGoalSuccess;
    public System.Action OnStageSuccess;
    public System.Action OnStageFailed;
    public static event System.Action OnGameOver;

    // ===== ゲーム状態 =====
    private int hitCount;
    private int maxHitCount;
    private bool isGameOver;
    private bool isGoalReached;

    // ===== 初期化 =====
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // =========================
    // ResultUI から呼ばれる遷移関数
    // =========================

    // リトライ
    public void RetryStage()
    {
        ResetFlags();
        SceneManager.LoadScene("Stage");
    }

    // タイトルへ
    public void GoToTitle()
    {
        ResetFlags();
        SceneManager.LoadScene("Title");
    }

    // 次のステージへ
    public void GoToNextStage()
    {
        int next = CurrentStageIndex + 1;

        ResetFlags();

        if (StageDatabase.Instance != null &&
            next < StageDatabase.Instance.StageCount)
        {
            CurrentStageIndex = next;
            SceneManager.LoadScene("Stage");
        }
        else
        {
            SceneManager.LoadScene("Title");
        }
    }

    // StageSelect から呼ぶ
    public void SetStageIndex(int index)
    {
        CurrentStageIndex = index;
    }

    // =========================
    // ゲーム進行ロジック
    // =========================
    public void SetGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        OnStageFailed?.Invoke(); 
        OnGameOver?.Invoke();
    }

    public void StartNewStage(StageData stage)
    {
        hitCount = 0;
        isGameOver = false;
        isGoalReached = false;
        maxHitCount = stage.maxHitCount;

        var player = FindAnyObjectByType<Player>();
        if (player != null)
        {
            player.transform.position = stage.playerStartPos;
            player.ResetPlayer();
        }
    }

    public bool CanCountHit()
    {
        return !isGameOver && !isGoalReached;
    }

    public void AddHitCount()
    {
        if (!CanCountHit()) return;

        hitCount++;

        if (hitCount > maxHitCount)
        {
            isGameOver = true;
            OnStageFailed?.Invoke();
            OnGameOver?.Invoke();
        }
    }

    // ゴール成功
    public void NotifyGoalSuccess()
    {
        if (isGoalReached) return;

        isGoalReached = true;
        OnGoalSuccess?.Invoke();
        OnStageSuccess?.Invoke();
    }

    // =========================
    // 補助
    // =========================

    private void ResetFlags()
    {
        isGameOver = false;
        isGoalReached = false;
    }

    public void RestartStageClean()
    {
        StartCoroutine(RestartStageCoroutine());
    }

    private IEnumerator RestartStageCoroutine()
    {
        // ① 物理・入力を完全停止
        Time.timeScale = 0f;

        // ② 状態完全リセット
        hitCount = 0;
        isGameOver = false;
        isGoalReached = false;

        yield return null; // 1フレーム待つ

        // ③ timeScale を戻す
        Time.timeScale = 1f;

        // ④ シーン再ロード
        SceneManager.LoadScene("Stage");
    }


    public int GetHitCount() => hitCount;
    public int GetMaxHitCount() => maxHitCount;
    public bool IsGameOver() => isGameOver;
}