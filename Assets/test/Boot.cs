using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    public GameObject loadingUI;  // ローディング画面のUIをアタッチ
    public GameObject splashScreenUI;  // スプラッシュ画面（ロゴなど）

    private void Start()
    {
        // ゲームの初期設定を行う
        InitializeGameSettings();

        // スプラッシュ画面を表示する場合、ここで表示を開始
        if (splashScreenUI != null)
        {
            splashScreenUI.SetActive(true);
        }

        // ゲームの準備ができたらメインメニューに遷移
        Invoke("LoadMainMenu", 2f); // 2秒後に遷移
    }

    // ゲーム設定の初期化
    private void InitializeGameSettings()
    {
        Debug.Log("ゲーム設定の初期化中...");
        // ここで設定を読み込んだり、PlayerPrefs からデータを取得するなど
        // 例えば音量設定を読み込む
        float volume = PlayerPrefs.GetFloat("volume", 1f);
        Debug.Log("音量: " + volume);
    }

    // メインメニューシーンへの遷移
    private void LoadMainMenu()
    {
        // スプラッシュ画面を非表示にする
        if (splashScreenUI != null)
        {
            splashScreenUI.SetActive(false);
        }

        // メインメニューシーンに遷移
        SceneManager.LoadScene("MainMenu");
    }
}
