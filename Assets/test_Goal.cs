using UnityEngine;

public class test_Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            test_Stage_Manager.Instance.StageClear();
        }
    }
}
