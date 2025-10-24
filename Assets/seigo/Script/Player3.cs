using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player3 : MonoBehaviour
{
    [SerializeField] private float power = 5f;      // �ł��o����
    [SerializeField] private float m_speed = 15f;   // ���x

    private Rigidbody2D rb;
    private Vector2 m_velocity;        // ���݂̑��x
    private Vector2 startPos;          // �h���b�O�J�n�ʒu
    private Vector2 endPos;            // �h���b�O�I���ʒu
    private bool isDragging = false;   // �h���b�O���

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �������x�̓[���ŊJ�n�A�ł��o���O�ɐݒ肷��
        m_velocity = Vector2.zero;
        rb.linearVelocity = m_velocity;
    }

    void OnMouseDown()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;

        // �h���b�O���͑��x���[���ɂ��Ē�~
        rb.linearVelocity = Vector2.zero;
    }

    void OnMouseDrag()
    {
        // �}�E�X�̌��݈ʒu���擾
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        // �h���b�O�I�����ɗ͂�������
        isDragging = false;

        // ���������������istartPos - endPos�j�ɗ͂�������
        Vector2 direction = startPos - endPos;
        direction.Normalize();  // ���K�����ĕ����x�N�g���Ƃ��Ďg�p
        m_velocity = direction * power; // �����Ɋ�Â��đ��x���X�V
        rb.AddForce(m_velocity, ForceMode2D.Impulse); // ���x�ł͂Ȃ��͂�������
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // �Փˎ��̔��ˏ���
        var inDirection = m_velocity;                        // ���݂̐i�s����
        var inNormal = other.contacts[0].normal;             // �Փ˖ʂ̖@���x�N�g��

        // ���˕������v�Z�����K��
        Vector2 reflectDirection = Vector2.Reflect(inDirection, inNormal).normalized;

        // �V�������x���v�Z���ARigidbody�ɐݒ�
        m_velocity = reflectDirection * m_speed; // ���ˌ�̑��x���v�Z
        rb.linearVelocity = m_velocity; // Rigidbody�ɔ��f
    }
}
