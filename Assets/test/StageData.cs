using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/StageData")]
public class StageData : ScriptableObject
{
    [Header("表示用（任意）")]
    public string stageName;

    [Header("ステージ構造（壁・ゴール全部入り）")]
    public GameObject stagePrefab;

    [Header("プレイヤー設定")]
    public Vector3 playerStartPos;

    [Header("ルール")]
    public int maxHitCount = 3;
}
