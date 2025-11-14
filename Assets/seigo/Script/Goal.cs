using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        int currentHit = Game_Manager.Instance.GetHitCount();
        int maxHit = Game_Manager.Instance.GetMaxHitCount();

        if (currentHit < maxHit)
        {
            Game_Manager.Instance.GameOver();
            return;
        }

        // ƒS[ƒ‹¬Œ÷
        Time.timeScale = 0f;

        if (Game_Manager.Instance.goalUI != null)
            Game_Manager.Instance.goalUI.SetActive(true);
    }
}
