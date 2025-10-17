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
        rb.gravityScale = 0;//落下しないようにする(必要に応じて変更)
    }

    void OnMouseDown()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        rb.linearVelocity = Vector2.zero;//引っ張ている間は止める
    }
    // Update is called once per frame
    void OnMouseDrag()
    {
        // 現在のマウスの位置を取得(引っ張り方向を確認するよう)
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        //　ドラッグ終了　→　力を加える
        isDragging = false;

        Vector2 direction = startPos - endPos;//引っ張った方向
        rb.AddForce(direction * power, ForceMode2D.Impulse);
    }
}
