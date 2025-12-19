using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/StageData")]
public class StageData : ScriptableObject
{
    public string stageName;          // ステージ名（任意）
    public int maxHitCount = 3;       // このステージの最大バウンド回数
    public Vector3 playerStartPos;    // プレイヤー開始位置
}
