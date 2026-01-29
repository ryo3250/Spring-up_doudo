using UnityEngine;
using UnityEngine.UI;

public class test_Stage_Manager : MonoBehaviour
{
    public static test_Stage_Manager Instance;

    [Header("ステージ設定")]
    [SerializeField] private int stageMaxHitCount = 3;

    void Start()
    {
        test_Game_Manager.Instance.SetMaxHitCount(stageMaxHitCount);
    }

    public void StageClear() 
    {
        test_Game_Manager.Instance.ShowGoalUI();
    }
}
