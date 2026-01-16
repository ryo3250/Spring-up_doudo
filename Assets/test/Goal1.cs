using UnityEngine;

public class Goal1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.OnReachGoal();
        Debug.Log("ƒS[ƒ‹‚É‰½‚©G‚ê‚½: " + other.name);
    }
}

