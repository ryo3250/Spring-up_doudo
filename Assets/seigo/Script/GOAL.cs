using UnityEngine;

public class GOAL : MonoBehaviour
{
    public GameManager gameManager;  // ゲームマネージャー（リザルトを表示する役割）

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // プレイヤーがゴールに到達したら
        {
            gameManager.ShowResult();  // リザルト画面を表示する
        }
    }
}
