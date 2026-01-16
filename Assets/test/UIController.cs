using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text hitCountText;
    [SerializeField] private GameObject clearUI;
    [SerializeField] private GameObject gameOverUI;

    private void OnEnable()
    {
        GameManager.OnGoalSuccess += ShowClear;
        GameManager.OnGameOver += ShowGameOver;
    }

    private void OnDisable()
    {
        GameManager.OnGoalSuccess -= ShowClear;
        GameManager.OnGameOver -= ShowGameOver;
    }

    private void Update()
    {
        hitCountText.text =
            GameManager.Instance.GetHitCount() + " / " +
            GameManager.Instance.GetMaxHitCount();
    }

    private void ShowClear()
    {
        clearUI.SetActive(true);
    }

    private void ShowGameOver()
    {
        gameOverUI.SetActive(true);
    }
}
