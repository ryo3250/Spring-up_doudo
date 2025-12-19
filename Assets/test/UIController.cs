using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text hitText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject goalUI;
    [SerializeField] private Player player;

    private void OnEnable()
    {
        GameManager.OnGameOver += ShowGameOver;
        GameManager.OnGoalSuccess += ShowGoal;
    }

    private void OnDisable()
    {
        GameManager.OnGameOver -= ShowGameOver;
        GameManager.OnGoalSuccess -= ShowGoal;
    }

    private void Update()
    {
        hitText.text = GameManager.Instance.GetHitCount() + " / " +
                       GameManager.Instance.GetMaxHitCount();
    }

    private void ShowGameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ShowGoal()
    {
        goalUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnRetryButton()
    {
        Time.timeScale = 1f;
        GameManager.Instance.StartNewStage(null); // å„Ç≈çƒÉçÅ[ÉhÇ∑ÇÈèÍçáÇÕ StageData ÇìnÇ∑
        player.ResetPlayer();
        gameOverUI.SetActive(false);
        goalUI.SetActive(false);
    }
}
