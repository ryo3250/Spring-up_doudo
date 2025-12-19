using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    [SerializeField] private GameManager gameManagerPrefab;

    private void Awake()
    {
        if (GameManager.Instance == null)
            Instantiate(gameManagerPrefab);
    }

    private void Start()
    {
        SceneManager.LoadScene("Title"); // 最初はタイトルシーンへ
    }
}
