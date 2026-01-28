using UnityEngine;

public class test_Stage_Select : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnStartButton(int stageNumber) 
    {
        test_Game_Manager.Instance.StartStage(stageNumber);
    }
}
