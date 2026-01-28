using UnityEngine;

public class test_StageUI : MonoBehaviour
{
    public void OnRetry() 
    {
        test_Game_Manager.Instance.Retry();
    }

    public void OnBackToSelect() 
    {
        test_Game_Manager.Instance.GoStageSelect();
    }

    public void OnBackToTitle() 
    {
        test_Game_Manager.Instance.GoTitle();
    }
}
