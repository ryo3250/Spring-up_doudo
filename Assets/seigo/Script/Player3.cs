using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player3 : MonoBehaviour
{
    [SerializeField] private float power = 5f;      // 打ち出し力
    [SerializeField] private float m_speed = 15f;   // 速度

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
    }

    void OnMouseDown()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;

        // ドラッグ中は速度をゼロにして停止
        rb.linearVelocity = Vector2.zero;
    }

    void OnMouseDrag()
    {
        // マウスの現在位置を取得
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 衝突時の反射処理
        var inDirection = m_velocity;                        // 現在の進行方向
        var inNormal = other.contacts[0].normal;             // 衝突面の法線ベクトル

        // 反射方向を計算し正規化
        Vector2 reflectDirection = Vector2.Reflect(inDirection, inNormal).normalized;

        // 新しい速度を計算し、Rigidbodyに設定
        m_velocity = reflectDirection * m_speed; // 反射後の速度を計算
        rb.linearVelocity = m_velocity; // Rigidbodyに反映
    }
}
