using System.Net.NetworkInformation;
using UnityEditor.Purchasing;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D),typeof(LineRenderer))]
public class Player : MonoBehaviour
{
    [Header("�p�����[�^�ݒ�")]
    public  float power  = 1.0f;
    public float maxArrowLength = 3.0f; // ���̍ő�̒���
    public Color minColor = Color.green;//�������菬: �F
    public Color maxColor = Color.red;  //���������: �F

    [Header("Arrow Head (Inspector�Ŋ��蓖��)")]
    public Transform arrowHead;              //����[��Transform(hierarchy�̎q�I�u�W�F�N�g)
    public Vector2 arrowHeadBaseScale = new Vector2(0.3f, 0.3f);//��{�X�P�[��(x,y)


    private Rigidbody2D rb;
    private Vector2 startPos;
    private Vector2 endPos;
    private bool isDragging = false;
    private LineRenderer line;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;//�������Ȃ��悤�ɂ���(�K�v�ɉ����ĕύX)

        //LineRenderer�����ݒ�
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.1f;
        line.endWidth = 0.05f;
        line.enabled = false;

        //arrowHead �����蓖�Ă��Ă���΍ŏ��͔�\��
        if(arrowHead != null) arrowHead.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        rb.linearVelocity = Vector2.zero;//�������Ă���Ԃ͎~�߂�
        line.enabled = true;
        if (arrowHead != null) arrowHead.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void OnMouseDrag()
    {
        if (!isDragging) return;

        // ���݂̃}�E�X�̈ʒu���擾(��������������m�F����悤)
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //�����������
        Vector2 direction = startPos - endPos;

        //��������
        Vector2 clampedDir = Vector2.ClampMagnitude(direction, maxArrowLength);

        // LineRenderer ���X�V�i�{�[���̈ʒu���������������ցj
        Vector3 origin = transform.position;
        Vector3 endPoint = origin + (Vector3)clampedDir;
        line.SetPosition(0, origin);
        line.SetPosition(1, endPoint);

        //�F�̋����ɉ����ĕ��(0..1)
        float t = Mathf.Clamp01(direction.magnitude / maxArrowLength);
        Color lerped = Color.Lerp(minColor, maxColor, t);
        line.startColor = lerped;
        line.endColor = lerped;

        //arrowHead(�O�p�`)���X�V
        if (arrowHead != null) 
        {
            //�ʒu:LineRenderer�̏I�_(������O�ɃI�t�Z�b�g��������Β���)
            arrowHead.position = endPoint;

            //
            if (direction.sqrMagnitude > 0.0001f) 
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                arrowHead.rotation =Quaternion.Euler(0f, 0f, angle+ 30f);
            }

            float scaleFactor = 0.6f + 0.8f * t;
            arrowHead.localScale = new Vector3(arrowHeadBaseScale.x * scaleFactor,
                                               arrowHeadBaseScale.y * scaleFactor,
                                               1f);

            SpriteRenderer sr =arrowHead.GetComponent<SpriteRenderer>();
            if (sr != null) 
            {
                sr.color = lerped;
            }
        }
    }

    void OnMouseUp()
    {
        //�@�h���b�O�I���@���@�͂�������
        isDragging = false;
        line.enabled = false;
        if (arrowHead != null) arrowHead.gameObject.SetActive(false);

        Vector2 direction = startPos - endPos;//��������������
        rb.AddForce(direction * power, ForceMode2D.Impulse);
    }
}
