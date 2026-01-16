using UnityEngine;

public class Warp_Portal_ver_2 : MonoBehaviour
{
    [SerializeField] private Transform warpTarget_2;
    [SerializeField] private float cooldown_2 = 0.3f;

    private bool canWarp_2 = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canWarp_2) return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(Warp_2(other));
        }
    }

    private System.Collections.IEnumerator Warp_2(Collider2D player)
    {
        canWarp_2 = false;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        //ワープ前の移動速度を保存
        Vector2 savedVelocity = Vector2.zero;
        float savedAngular = 0f;

        if (rb != null)
        {
            savedVelocity = rb.linearVelocity;
            savedAngular = rb.angularVelocity;
        }

        //位置だけ変更
        player.transform.position = warpTarget_2.position;

        yield return null;

        if (rb != null) 
        { 
            rb.linearVelocity = savedVelocity;
            rb.angularVelocity = savedAngular;
        }

        yield return new WaitForSeconds(cooldown_2);
        canWarp_2 = true;
    }
}
