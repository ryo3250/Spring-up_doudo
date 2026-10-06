using UnityEngine;

public class ResetButton : MonoBehaviour
{
    // リセットボタンが押された時に呼ばれるメソッド
    public void OnResetButtonClicked()
    {
        if (Game_Manager.Instance != null)
        {
            Game_Manager.Instance.ResetGame(); // ゲームリセット
        }
    }
}
