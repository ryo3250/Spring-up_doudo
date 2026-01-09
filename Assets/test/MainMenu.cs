using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ゲーム開始
    public void StartGame()
    {
        SceneManager.LoadScene("select"); // 実際のゲームシーンに遷移
    }

    // オプション設定画面
    public void OpenOptions()
    {
        SceneManager.LoadScene("OptionsMenu"); // オプションシーンに遷移
    }

    // ゲーム終了
    public void QuitGame()
    {
        Application.Quit(); // ゲーム終了
        Debug.Log("ゲーム終了");
    }
}
