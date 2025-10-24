using UnityEngine;
using UnityEngine.SceneManagement;  // シーン遷移に必要

public class GameManager : MonoBehaviour
{
    public GameObject resultPanel;  // リザルト画面のUI（パネルなど）
    public Text resultText;         // 結果を表示するテキスト

    public void ShowResult()
    {
        // リザルト画面を表示
        resultPanel.SetActive(true);
        resultText.text = "Congratulations! You've finished the race!";  // 結果テキスト

        // 必要に応じてリザルトに基づく追加の処理（タイム表示やスコアなど）もできます
    }

    // ボタンなどでリザルト後に再スタートする場合
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // 現在のシーンを再読み込み
    }
}
