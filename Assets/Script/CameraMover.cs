using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform[] cameraTargets; //50サイズのCameraTarget(ステージの量によっては変更可能)
    public float speed = 5f;//カメラ速度
    private int currentIndex = 0;//どこのターゲットに向かうかの番号
    private bool isMoving = false;//カメラが動いている最中かどうか
    private Vector3 targetPos;//カメラが向かう目標座標

    void Update()
    {
        if (isMoving) //カメラが移動中なら動かす
        {
            transform.position = Vector3.Lerp//いっきに移動しないように
            (
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position,targetPos) < 0.1f)//カメラが0.1m以内に近づかいたら
            { 
                isMoving = false;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void MoveToNextStage()//ボタンが押されたときの処理
    {
        if (currentIndex < cameraTargets.Length)//まだ移動先が残っているのか
        {
            targetPos = new Vector3(
                cameraTargets[currentIndex].position.x,
                cameraTargets[currentIndex].position.y,
                transform.position.z
            );
            isMoving = true;//移動開始
            currentIndex++;
        }
    }
}
