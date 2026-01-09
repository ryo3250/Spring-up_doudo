using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] GameObject successPanel;
    [SerializeField] GameObject failPanel;

    public void ShowSuccess()
    {
        successPanel.SetActive(true);
        failPanel.SetActive(false);
    }

    public void ShowFail()
    {
        successPanel.SetActive(false);
        failPanel.SetActive(true);
    }

    // ƒ{ƒ^ƒ“—p
    public void OnNextStage()
    {
        GameManager.Instance.GoToNextStage();
    }

    public void OnRetry()
    {
        GameManager.Instance.RestartStageClean();
    }

    public void OnTitle()
    {
        GameManager.Instance.GoToTitle();
    }
}
