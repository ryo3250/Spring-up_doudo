using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    /// <summary>
    /// シーン名を指定してロード（Button用）
    /// </summary>
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // 念のため停止解除
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// ビルドインデックス指定（必要なら）
    /// </summary>
    public void LoadSceneByIndex(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// 現在のシーンを再ロード（Retry用）
    /// </summary>
    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// ゲーム終了（UI用）
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ゲーム終了");
    }
}
