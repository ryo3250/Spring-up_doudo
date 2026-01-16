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
    public static event System.Action OnGameOver;
    public System.Action OnStageFailed;

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
    // シーン遷移（ResultUI用）
    // =========================

    public void RetryStage()
    {
        ResetFlags();
        SceneManager.LoadScene("Stage");
    }

    public void GoToTitle()
    {
        ResetFlags();
        SceneManager.LoadScene("Title");
    }

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

    public void SetStageIndex(int index)
    {
        CurrentStageIndex = index;
    }

    // =========================
    // ゲーム進行
    // =========================

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
            SetGameOver();
            OnStageFailed?.Invoke();
        }
    }
    public void OnReachGoal()
    {
        if (isGameOver || isGoalReached) return;

        isGoalReached = true;
        OnGoalSuccess?.Invoke();
    }

    // ===== GameOver =====
    public void SetGameOver()
    {
        if (isGameOver || isGoalReached) return;

        isGameOver = true;
        OnGameOver?.Invoke();
    }

    // ===== ゴール成功 =====
    public void NotifyGoalSuccess()
    {
        if (isGameOver || isGoalReached) return;

        isGoalReached = true;
        OnGoalSuccess?.Invoke();
    }

    // =========================
    // 補助
    // =========================

    private void ResetFlags()
    {
        isGameOver = false;
        isGoalReached = false;
        hitCount = 0;
    }

    public int GetHitCount() => hitCount;
    public int GetMaxHitCount() => maxHitCount;
    public bool IsGameOver() => isGameOver;
    public bool IsGoalReached() => isGoalReached;
}
