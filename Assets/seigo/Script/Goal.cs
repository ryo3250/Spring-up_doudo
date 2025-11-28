using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーがゴールに触れた場合のみ判定
        if (!collision.gameObject.CompareTag("Player")) return;

        int currentHit = Game_Manager.Instance.GetHitCount();
        int maxHit = Game_Manager.Instance.GetMaxHitCount();

        // バウンド回数がまだ足りていない場合、ゲームオーバーにする
        if (currentHit < maxHit)
        {
            Game_Manager.Instance.GameOver();
            return;
        }

        // バウンド回数がクリアされている場合、ゴール成功としてUIを表示
        Time.timeScale = 0f;

        if (Game_Manager.Instance.goalUI != null)
        {
            Game_Manager.Instance.goalUI.SetActive(true);
        }

        Debug.Log("ゴール成功！");
    }
}
