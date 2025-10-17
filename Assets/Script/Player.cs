using UnityEditor.Purchasing;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public  float power  = 1.0f;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private Vector2 endPos;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;//�������Ȃ��悤�ɂ���(�K�v�ɉ����ĕύX)
    }

    void OnMouseDown()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        rb.linearVelocity = Vector2.zero;//�������Ă���Ԃ͎~�߂�
    }
    // Update is called once per frame
    void OnMouseDrag()
    {
        // ���݂̃}�E�X�̈ʒu���擾(��������������m�F����悤)
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        //�@�h���b�O�I���@���@�͂�������
        isDragging = false;

        Vector2 direction = startPos - endPos;//��������������
        rb.AddForce(direction * power, ForceMode2D.Impulse);
    }
}
