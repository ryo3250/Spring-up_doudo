using System.Net.NetworkInformation;
using UnityEditor.Purchasing;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D),typeof(LineRenderer))]
public class Player : MonoBehaviour
{
    [Header("パラメータ設定")]
    public  float power  = 1.0f;
    public float maxArrowLength = 3.0f; // 矢印の最大の長さ
    public Color minColor = Color.green;//引っ張り小: 色
    public Color maxColor = Color.red;  //引っ張り大: 色

    [Header("Arrow Head (Inspectorで割り当て)")]
    public Transform arrowHead;              //矢印先端のTransform(hierarchyの子オブジェクト)
    public Vector2 arrowHeadBaseScale = new Vector2(0.3f, 0.3f);//基本スケール(x,y)


    private Rigidbody2D rb;
    private Vector2 startPos;
    private Vector2 endPos;
    private bool isDragging = false;
    private LineRenderer line;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;//落下しないようにする(必要に応じて変更)

        //LineRenderer初期設定
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.1f;
        line.endWidth = 0.05f;
        line.enabled = false;

        //arrowHead が割り当てられていれば最初は非表示
        if(arrowHead != null) arrowHead.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        rb.linearVelocity = Vector2.zero;//引っ張ている間は止める
        line.enabled = true;
        if (arrowHead != null) arrowHead.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void OnMouseDrag()
    {
        if (!isDragging) return;

        // 現在のマウスの位置を取得(引っ張り方向を確認するよう)
        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //引っ張り方向
        Vector2 direction = startPos - endPos;

        //長さ制限
        Vector2 clampedDir = Vector2.ClampMagnitude(direction, maxArrowLength);

        // LineRenderer を更新（ボールの位置から引っ張り方向へ）
        Vector3 origin = transform.position;
        Vector3 endPoint = origin + (Vector3)clampedDir;
        line.SetPosition(0, origin);
        line.SetPosition(1, endPoint);

        //色の強さに応じて補間(0..1)
        float t = Mathf.Clamp01(direction.magnitude / maxArrowLength);
        Color lerped = Color.Lerp(minColor, maxColor, t);
        line.startColor = lerped;
        line.endColor = lerped;

        //arrowHead(三角形)を更新
        if (arrowHead != null) 
        {
            //位置:LineRendererの終点(少し手前にオフセットをつけたれば調整)
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
        //　ドラッグ終了　→　力を加える
        isDragging = false;
        line.enabled = false;
        if (arrowHead != null) arrowHead.gameObject.SetActive(false);

        Vector2 direction = startPos - endPos;//引っ張った方向
        rb.AddForce(direction * power, ForceMode2D.Impulse);
    }
}
