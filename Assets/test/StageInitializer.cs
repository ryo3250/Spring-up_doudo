using UnityEngine;

public class StageInitializer : MonoBehaviour
{
    private void Start()
    {
        int index = GameManager.Instance.CurrentStageIndex;
        StageData stage = StageDatabase.Instance.GetStage(index);

        GameManager.Instance.StartNewStage(stage);
    }
}
