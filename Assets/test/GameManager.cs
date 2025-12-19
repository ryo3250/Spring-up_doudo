using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event System.Action OnGoalSuccess;  // ゴール成功時のイベント

    private int hitCount;
    private int maxHitCount;
    private bool isGameOver;

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

    // ゴール成功通知用のメソッドを追加
    public void NotifyGoalSuccess()
    {
        OnGoalSuccess?.Invoke(); // イベントを発火
    }

    public void StartNewStage(StageData stage)
    {
        hitCount = 0;
        isGameOver = false;
        maxHitCount = stage.maxHitCount;

        var player = FindAnyObjectByType<Player>();
        if (player != null)
        {
            player.transform.position = stage.playerStartPos;
            player.ResetPlayer();
        }
    }

    public void AddHitCount()
    {
        if (isGameOver) return;

        hitCount++;
        if (hitCount > maxHitCount)
            SetGameOver();
    }

    public void SetGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        OnGameOver?.Invoke(); // ゲームオーバーイベント
    }

    public int GetHitCount() => hitCount;
    public int GetMaxHitCount() => maxHitCount;
    public bool IsGameOver() => isGameOver;

    public static event System.Action OnGameOver; // ゲームオーバーイベント
}