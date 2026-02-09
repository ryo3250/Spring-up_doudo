using UnityEngine;
using UnityEngine.UI;

public class test_Stage_Manager : MonoBehaviour
{
    public static test_Stage_Manager Instance;

    [Header("ƒ|[ƒYUI")]
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Player3 player;
    [SerializeField] private int maxHitCount = 3;
    [SerializeField] private Text hitText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject goalUI;

    private bool isPaused = false;
    private int hitCount;
    private bool isGameOver;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializeStage();
    }
    void InitializeStage()
    {
        hitCount = 0;
        isGameOver = false;
        Time.timeScale = 1f;

        if (player != null)
        {
            player.Initialize();
        }

        if (gameOverUI)
        {
            gameOverUI.SetActive(false);
        }

        if (goalUI)
        {
            goalUI.SetActive(false);
        }
    }

    public void AddHitCount()
    {
        if (isGameOver) return;

        hitCount++;
        Update();

        if (hitCount > maxHitCount)
        {
            GameOver();
        }
    }

    public void TogglePause()
    {
        if (isGameOver) return;

        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            if (pauseUI) pauseUI.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1f;
            if(pauseUI) pauseUI.SetActive(false);
        }
    }

    public void Resume() 
    {
        if (!isPaused) return;

        isPaused = false;
        Time.timeScale = 1f;

        if(pauseUI) pauseUI.SetActive(false);
    }

    public void PauseGoTitle() 
    {
        Time.timeScale = 1f;
        test_Game_Manager.Instance.GoTitle();
    }
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void TryStageClear()
    {
        if (isGameOver) return;

        if (hitCount == maxHitCount)
        {
            StageClear();
        }
        else
        {
            GameOver();
        }
    }

    public void StageClear()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;
        Time.timeScale = 0f;

        if (goalUI != null)
        {
            goalUI.SetActive(true);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            TogglePause();
        }

        if (hitText)
        {
            hitText.text = hitCount + "/" + maxHitCount;
        }
    }

    public int GetHitCount() => hitCount;
    public int GetMaxHitCount() => maxHitCount;
}
