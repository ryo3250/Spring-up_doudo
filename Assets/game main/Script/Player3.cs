using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player3 : MonoBehaviour
{
    [SerializeField] private float power = 5f;      // 打ち出し力
    [SerializeField] private float m_speed = 15f;   // 速度
    [SerializeField] private LineRenderer lineRenderer; // 矢印用のLineRenderer
    [SerializeField] private float lineWidth = 0.1f; // 矢印線の太さ

    private Rigidbody2D rb;
    private Vector2 m_velocity;        // 現在の速度
    private Vector2 startPos;          // ドラッグ開始位置
    private Vector2 endPos;            // ドラッグ終了位置
    private bool isDragging = false;   // ドラッグ状態

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 初期速度はゼロで開始、打ち出し前に設定する
        m_velocity = Vector2.zero;
        rb.linearVelocity = m_velocity;

        // LineRendererの初期設定
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2; // 始点と終点の2点だけ
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.enabled = false; // 最初は非表示
        }
    }

    void OnMouseDown()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;

        // ドラッグ中は速度をゼロにして停止
        rb.linearVelocity = Vector2.zero;

        // LineRendererを表示して、始点と終点を設定
        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, startPos);
        }
    }

    void OnMouseDrag()
    {
        // マウスの現在位置を取得
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // LineRendererの終点をドラッグ先に設定
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(1, endPos);
        }
    }

    void OnMouseUp()
    {
        // ドラッグ終了時に力を加える
        isDragging = false;

        // 引っ張った方向（startPos - endPos）に力を加える
        Vector2 direction = startPos - endPos;
        direction.Normalize();  // 正規化して方向ベクトルとして使用
        m_velocity = direction * power; // 方向に基づいて速度を更新
        rb.AddForce(m_velocity, ForceMode2D.Impulse); // 速度ではなく力を加える

        // LineRendererを非表示にする
        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 反射処理
        var inDirection = m_velocity;
        var inNormal = other.contacts[0].normal;

        // 反射方向を計算し、速度を更新
        Vector2 reflectDirection = Vector2.Reflect(inDirection, inNormal).normalized;
        m_velocity = reflectDirection * m_speed;

        // Rigidbodyに反映
        rb.linearVelocity = m_velocity;

        // ボールが何かに当たったらバウンドとしてカウント
        if (Game_Manager.Instance != null)
        {
            Game_Manager.Instance.AddHitCount();
        }

        //// 壁に触れた場合、バウンド回数が足りていればゲームオーバー
        //if (other.gameObject.CompareTag("Wall") && Game_Manager.Instance.GetHitCount() < Game_Manager.Instance.GetMaxHitCount())
        //{
        //    Game_Manager.Instance.GameOver();
        //}
    }
}
