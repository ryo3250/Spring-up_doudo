using UnityEngine;

public class test_Stage_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StageClear() 
    {
        test_Game_Manager.Instance.NextStage();
    }

    public void StageFailed() 
    {
        test_Game_Manager.Instance.Retry();
    }
}
