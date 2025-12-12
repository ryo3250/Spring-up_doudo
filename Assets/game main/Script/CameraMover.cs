using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform[] cameraTargets; //50サイズのCameraTarget(ステージの量によっては変更可能)
    public float speed = 5f;//カメラ速度
    private int currentIndex = 0;//どこのターゲットに向かうかの番号
    private bool isMoving = false;//カメラが動いている最中かどうか
    private Vector3 targetPos;//カメラが向かう目標座標
    public Transform[] playerSpawnPositions;

    void Start()
    {
        int stage = PlayerPrefs.GetInt("SelectedStage", 0);

        currentIndex = stage;

        MoveToStage(stage);//開始時に指定ステージに移動
        
    }
    void Update()
    {
        if (isMoving) //カメラが移動中なら動かす
        {
            Debug.Log("Moving... 現在位置 = " + transform.position + " / target = " + targetPos);
            transform.position = Vector3.Lerp//いっきに移動しないように
            (
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position,targetPos) < 0.1f)//カメラが0.1m以内に近づかいたら
            { 
                isMoving = false;

                // プレイヤー初期位置変更
                if (playerSpawnPositions.Length > currentIndex) 
                {
                    //ステージごとに初期位置を変えたい場合
                    Game_Manager.Instance.SetPlayerStartPosition(playerSpawnPositions[currentIndex].position);

                }

                //初期化
                Game_Manager.Instance.ResetGame();
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void MoveToNextStage()//ボタンが押されたときの処理
    {

        Debug.Log("---- MoveToNextStage ----");
        Debug.Log("currentIndex BEFORE = " + currentIndex);

        if (currentIndex + 1 < cameraTargets.Length)
        {
            currentIndex++;
            Debug.Log("currentIndex AFTER = " + currentIndex);

            targetPos = new Vector3(
                cameraTargets[currentIndex].position.x,
                cameraTargets[currentIndex].position.y,
                transform.position.z
            );

            Debug.Log("targetPos = " + targetPos);

            isMoving = true;
            Time.timeScale = 1f;
            Debug.Log("isMoving = " + isMoving);
        }
        else
        {
            Debug.Log("次のステージはありません");
        }
    }

    public void MoveToStage(int index) 
    {
        if (index >= 0 && index < cameraTargets.Length)//有効な範囲かの確認
        {
            Vector3 pos = cameraTargets[index].position;//indexを→ワールド座標を取り出し→posに保存
            targetPos = new Vector3(pos.x, pos.y, transform.position.z);//カメラが目指す目標座標を決めている(zは深度になるので変えると消えるため固定)
            isMoving = true;//カメラを動かすフラグ
        }
    }
}
