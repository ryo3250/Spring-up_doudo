using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance { get; private set; }

    [Header("ゲーム設定")]
    [SerializeField] private int maxHitCount = 3;  // 最大バウンド回数

    [Header("UI")]
    [SerializeField] private Text hitStatusText;    // バウンド数表示用
    [SerializeField] private GameObject gameOverUI; // ゲームオーバー画面
    [SerializeField] public GameObject goalUI;     // ゴール画面

    [Header("プレイヤー設定")]
    [SerializeField] private Transform player;      // プレイヤー（ボール）
    private Vector3 playerStartPos;
    private Rigidbody2D playerRb;

    private int hitCount = 0;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // プレイヤーの最初の位置と Rigidbody を保存
        if (player != null)
        {
            playerStartPos = player.position;
            playerRb = player.GetComponent<Rigidbody2D>();
        }

        UpdateHitUI();
    }

    public void AddHitCount()
    {
        if (isGameOver) return;

        hitCount++;
        UpdateHitUI();

        // バウンド回数が最大を超えたらゲームオーバー
        if (hitCount > maxHitCount)
        {
            GameOver();
        }
    }

    private void UpdateHitUI()
    {
        if (hitStatusText != null)
        {
            hitStatusText.text = hitCount + " / " + maxHitCount;
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Debug.Log("ゲームオーバー！");
    }

    // ゲームリセット処理
    public void ResetGame()
    {
        // バウンド数リセット
        hitCount = 0;
        isGameOver = false;

        // UI全て消す
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        if (goalUI != null)
            goalUI.SetActive(false);

        // 時間を戻す
        Time.timeScale = 1f;

        // プレイヤー初期位置に戻す
        if (player != null)
        {
            player.position = playerStartPos;

            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.angularVelocity = 0f;
            }
        }

        UpdateHitUI();

        Debug.Log("ゲームリセット完了！");
    }

    public int GetHitCount() => hitCount;
    public int GetMaxHitCount() => maxHitCount;
}
