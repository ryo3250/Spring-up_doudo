using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("Stage Data List（順番＝ステージ順）")]
    [SerializeField] private StageData[] stages;

    [Header("現在のステージを入れる親")]
    [SerializeField] private Transform currentStageRoot;

    private GameObject currentStageInstance;

    private void Start()
    {
        LoadCurrentStage();
    }

    public void LoadCurrentStage()
    {
        // 念のため GameManager チェック
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager が存在しません");
            return;
        }

        int index = GameManager.Instance.CurrentStageIndex;

        if (index < 0 || index >= stages.Length)
        {
            Debug.LogError($"StageIndex が不正です : {index}");
            return;
        }

        StageData data = stages[index];

        // ===== ① 前のステージを完全削除 =====
        if (currentStageInstance != null)
        {
            Destroy(currentStageInstance);
        }

        // ===== ② ステージ生成 =====
        currentStageInstance = Instantiate(
            data.stagePrefab,
            Vector3.zero,
            Quaternion.identity,
            currentStageRoot
        );

        // ===== ③ プレイヤー初期化 =====
        Player player = FindAnyObjectByType<Player>();
        if (player != null)
        {
            player.transform.position = data.playerStartPos;
            player.ResetPlayer();
        }

        // ===== ④ GameManager にステージ情報を渡す =====
        GameManager.Instance.StartNewStage(data);
    }
}
