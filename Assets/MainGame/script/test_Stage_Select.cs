using UnityEngine;

public class test_Stage_Select : MonoBehaviour
{
    void Start()
    {
        if (test_Game_Manager.Instance == null) 
        {
            Debug.LogWarning("GameManagerが無いので自動生成します(デバック用)");

            GameObject gm = new GameObject("GameManager");
            gm.AddComponent<test_Game_Manager>();
        }    
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnStartButton(int stageNumber) 
    {
        test_Game_Manager.Instance.StartStage(stageNumber);
    }
}
