using UnityEngine;

public sealed class Example : MonoBehaviour
{
    [SerializeField] private float m_speed = 15;
    [SerializeField] private Vector2 m_direction = new(1, 1);

    private Rigidbody2D m_rigidbody2D;
    private Vector2 m_velocity;

    private void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();

        m_direction.Normalize();

        m_velocity = m_direction * m_speed;
        m_rigidbody2D.linearVelocity = m_velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var inDirection = m_velocity;
        var inNormal = other.contacts[0].normal;

        m_direction = Vector2.Reflect(inDirection, inNormal).normalized;

        m_velocity = m_direction * m_speed;
        m_rigidbody2D.linearVelocity = m_velocity;
    }
}