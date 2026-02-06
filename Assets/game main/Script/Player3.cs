using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player3 : MonoBehaviour
{
    [SerializeField] private float power = 5f;      // 打ち出し力
    [SerializeField] private float m_speed = 15f;   // 速度

    [SerializeField] private Transform arrowTransform;
    [SerializeField] private float arrowLength = 2f;

    [SerializeField] private float rotateSpeed = 150f; // スクロール回転速度


    private Rigidbody2D rb;
    private Vector2 m_velocity;        // 現在の速度
    private Vector3 initialPosition;   //初期値を保存

    private float currentAngle = 0f;
    private bool canShoot = true;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; //最初の位置を記録
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (!canShoot) return;

        RotateByScroll();
        ShootBySpace();
    }

    //初期化メソッド
    public void Initialize() 
    {
        //位置のリセット
        transform.position = initialPosition;

        //速度のリセット
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        m_velocity = Vector2.zero;

        currentAngle = 0f;
        canShoot = true;

        if (arrowTransform != null)
        {

            arrowTransform.gameObject.SetActive(true);
            UpdateArrow();
        }
    }

    

    void RotateByScroll() 
    {
        float scroll = Input.mouseScrollDelta.y;

        if (Mathf.Abs(scroll)> 0.01f)
        {
            currentAngle += scroll * rotateSpeed * Time.deltaTime;
            UpdateArrow();
        }
    }

    void ShootBySpace() 
    {
        if (!canShoot) return;

        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            Vector2 dir = AngleToVector(currentAngle);

            rb.AddForce(dir * power, ForceMode2D.Impulse);
            m_velocity = dir * m_speed;

            canShoot = false;
        }
    }

    void UpdateArrow() 
    {
        if (arrowTransform == null) return;

        arrowTransform.position = transform.position;

        arrowTransform.rotation = Quaternion.Euler(0, 0, currentAngle - 90f);
        arrowTransform.localScale = Vector3.one;
    }

    Vector2 AngleToVector(float angle) 
    { 
        float rad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (test_Stage_Manager.Instance == null)
        {
            return;
        }

        var normal = other.contacts[0].normal;
        m_velocity = Vector2.Reflect(m_velocity, normal).normalized * m_speed;
        rb.linearVelocity = m_velocity;

        test_Stage_Manager.Instance.AddHitCount();

        if (other.gameObject.CompareTag("Dead_Wall"))
        {
            test_Stage_Manager.Instance.GameOver();
        }

        canShoot = true;
        UpdateArrow();
    }
}
