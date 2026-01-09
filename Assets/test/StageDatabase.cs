using UnityEngine;

public class StageDatabase : MonoBehaviour
{
    public static StageDatabase Instance { get; private set; }

    [SerializeField] private StageData[] stages;

    public int StageCount => stages.Length;

    public StageData GetStage(int index)
    {
        if (index < 0 || index >= stages.Length)
            return null;

        return stages[index];
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
