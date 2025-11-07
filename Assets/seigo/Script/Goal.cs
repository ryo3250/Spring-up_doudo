using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject goalUI;  // ゴールUI
    [SerializeField] private GameObject gameUI;  // 通常ゲームUI

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ゴール！");

            // ゲーム全体を一時停止
            Time.timeScale = 0f;  // 時間を停止

            // UI切り替え
            if (gameUI != null) gameUI.SetActive(false);
            if (goalUI != null) goalUI.SetActive(true);
        }
    }

    // ゲーム再開のための関数（ボタンなどに設定できる）
    public void ResumeGame()
    {
        Time.timeScale = 1f;  // 時間を元に戻す
    }
}
