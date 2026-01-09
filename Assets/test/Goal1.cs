using UnityEngine;

public class Goal1 : MonoBehaviour  // Goal -> Goal1 に変更
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーがゴールに触れた場合のみ判定
        if (!collision.gameObject.CompareTag("Player")) return;

        var gm = GameManager.Instance;  // GameManager の名前を修正
                                        // すでに失敗 or 成功してたら無視
        if (gm.IsGameOver()) return;
        // バウンド回数が足りていない場合、ゲームオーバー
        if (gm.GetHitCount() < gm.GetMaxHitCount())
        {
            gm.SetGameOver();
            return;
        }

        // バウンド回数がクリアされている場合、ゴール成功通知
        gm.NotifyGoalSuccess();  // イベント発火メソッドを呼び出す
        Debug.Log("ゴール成功！");
    }
}
