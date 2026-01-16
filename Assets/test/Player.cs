using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float power = 5f;
    [SerializeField] private float m_speed = 15f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float lineWidth = 0.1f;

    private Rigidbody2D rb;
    private Vector2 m_velocity;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector3 startWorldPos;

    // ★ 追加：ゴール到達フラグ
    private bool hasReachedGoal = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_velocity = Vector2.zero;
        rb.linearVelocity = Vector2.zero;

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.enabled = false;
        }

 
    }
    public void SetStartPosition(Vector3 pos)
    {
        startWorldPos = pos;
        transform.position = pos;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private void OnMouseDown()
    {
        rb.linearVelocity = Vector2.zero;

        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        if (lineRenderer != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, startPos);
        }
    }

    private void OnMouseDrag()
    {
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (lineRenderer != null)
            lineRenderer.SetPosition(1, endPos);
    }

    private void OnMouseUp()
    {
        Vector2 direction = startPos - endPos;
        direction.Normalize();

        m_velocity = direction * power;
        rb.AddForce(m_velocity, ForceMode2D.Impulse);

        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ★ ゴール最優先
        if (collision.gameObject.CompareTag("Goal"))
        {
            hasReachedGoal = true;
            GameManager.Instance.NotifyGoalSuccess();
            return;
        }

        // ★ ゴール後 or 数えられない状態なら完全停止
        if (hasReachedGoal || !GameManager.Instance.CanCountHit())
            return;

        Vector2 normal = collision.contacts[0].normal;

        // 正面衝突のみ
        if (Vector2.Dot(m_velocity.normalized, normal) >= 0f)
            return;

        Vector2 reflectDir = Vector2.Reflect(m_velocity, normal).normalized;
        m_velocity = reflectDir * m_speed;
        rb.linearVelocity = m_velocity;

        GameManager.Instance.AddHitCount();
    }

    public void ResetPlayer()
    {
        transform.position = startWorldPos;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

}
