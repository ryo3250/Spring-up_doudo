using UnityEngine;

public class warp_portal : MonoBehaviour
{
    [SerializeField] private Transform warpTarget;
    [SerializeField] private float cooldown = 0.3f;

    private bool canWarp = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canWarp) return;

        if (other.CompareTag("Player")) 
        {
            StartCoroutine(Warp(other));
        }
    }

    private System.Collections.IEnumerator Warp(Collider2D player) 
    {
        canWarp = false;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null) 
        { 
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        player.transform.position = warpTarget.position;

        yield return new WaitForSeconds(cooldown);

        canWarp = true;
    }
}
