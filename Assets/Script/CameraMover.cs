using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform[] cameraTargets; //50サイズのCameraTarget(ステージの量によっては変更可能)
    public float speed = 5f;//カメラ速度
    private int currentIndex = 0;//どこのターゲットに向かうかの番号
    private bool isMoving = false;//カメラが動いている最中かどうか
    private Vector3 targetPos;//カメラが向かう目標座標
    private bool initializedAfterMove = false;//初期化を1回だけ起こす

    void Start()
    {
        int stage = PlayerPrefs.GetInt("SelectedStage", 0);

        MoveToStage(stage);//開始時に指定ステージに移動
    }
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

            if (Vector3.Distance(transform.position, targetPos) < 0.1f)//カメラが0.1m以内に近づかいたら
            {
                isMoving = false;

                if (!initializedAfterMove) 
                { 
                //ステージ移動後にゲーム初期化
                    Game_Manager.Instance.ResetGame();//初期化
                    initializedAfterMove = true;//初期化済みにする
                }
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
            initializedAfterMove = false;// ← 次の移動が始まる前に解除
            currentIndex++;
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
