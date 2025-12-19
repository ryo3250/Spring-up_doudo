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
    private bool isDragging = false;
    private Vector3 startWorldPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_velocity = Vector2.zero;
        rb.linearVelocity = m_velocity;

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.enabled = false;
        }

        startWorldPos = transform.position;
    }

    private void OnMouseDown()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        rb.linearVelocity = Vector2.zero;

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
        isDragging = false;
        Vector2 direction = startPos - endPos;
        direction.Normalize();
        m_velocity = direction * power;
        rb.AddForce(m_velocity, ForceMode2D.Impulse);

        if (lineRenderer != null)
            lineRenderer.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 reflectDir = Vector2.Reflect(m_velocity, collision.contacts[0].normal).normalized;
        m_velocity = reflectDir * m_speed;
        rb.linearVelocity = m_velocity;

        // バウンド通知
        GameManager.Instance.AddHitCount();
    }

    public void ResetPlayer()
    {
        transform.position = startWorldPos;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
