using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectButton : MonoBehaviour
{
    // ステージ1に遷移
    public void SelectStage1()
    {
        SceneManager.LoadScene("0-0");
    }

    // ステージ2に遷移
    public void SelectStage2()
    {
        SceneManager.LoadScene("1-1");
    }

    // 他のステージも同様に設定可能
}
