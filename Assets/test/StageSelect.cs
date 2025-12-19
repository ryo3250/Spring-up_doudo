using UnityEngine;

public class StageSelect : MonoBehaviour
{
    [SerializeField] private StageData[] stages;

    public void LoadStage(int index)
    {
        if (index < 0 || index >= stages.Length) return;

        StageData stage = stages[index];
        GameManager.Instance.StartNewStage(stage);

        // ステージを別Sceneにしたい場合はここでロード
        // SceneManager.LoadScene(stage.sceneName);
    }
}
