using UnityEditor;
using UnityEngine;

public class DragAim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("参照")]
    [SerializeField] private Transform playerTransfrom;

    [Header("矢印")]
    [SerializeField] private Transform arrowTransform;
    private SpriteRenderer arrowRenderer;

    [Header("矢印のサイズ")]
    [SerializeField] private float minArrowScale = 0.2f;
    [SerializeField] private float maxArrowScale = 0.6f;

    [Header("引っ張り設定")]
    [SerializeField] private float maxDragDistance = 3f;

    [Header("色設定")]
    [SerializeField] private Color weakColor = Color.cyan;
    //[SerializeField] private Color midColor = Color.yellow;
    [SerializeField] private Color strongColor = Color.red;

    private LineRenderer line;

    void Start()
    {
        line= GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.enabled = false;

        arrowRenderer = arrowTransform.GetComponent<SpriteRenderer>();
        arrowTransform.gameObject.SetActive(false);
    }

    void Update()
    {
        //ボタンが押されたら
        if (Input.GetMouseButtonDown(0)) {
            
            line.enabled = true;
            arrowTransform.gameObject.SetActive(true);
        }

        //押しているあいだは(引っ張り中)
        if (Input.GetMouseButton(0)) 
        {

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 playerPos = playerTransfrom.position;

            //引っ張った方向の逆
            Vector2 dir = playerPos - mousePos;

            //長さ制限
            Vector2 clampedDir = Vector2.ClampMagnitude(dir, maxDragDistance);

            //線の位置
            line.SetPosition(0, playerPos);
            line.SetPosition(1, playerPos + clampedDir);

            // 矢印の位置（線の先端）
            arrowTransform.position = playerPos + clampedDir;

            // 矢印の向き（飛ぶ方向を向かせる）
            float angle = Mathf.Atan2(clampedDir.y, clampedDir.x) * Mathf.Rad2Deg -90f;
            arrowTransform.rotation = Quaternion.Euler(0, 0, angle);


            //長さに応じて色を変える
            float t = clampedDir.magnitude / maxDragDistance;
            Color line_aimColor = Color.Lerp(weakColor, strongColor, t);
            //矢印の色を変える
            float arrowScale = Mathf.Lerp(minArrowScale, maxArrowScale, t);
            arrowTransform.localScale = new Vector3(arrowScale, arrowScale, 1f);

            //最大距離で制限
            line.startColor = line_aimColor;
            line .endColor = line_aimColor;
            arrowRenderer.color = line_aimColor;

            
        }

        //ボタンを離したら
        if (Input.GetMouseButtonUp(0)) 
        {
            line.enabled = false;
            arrowTransform.gameObject.SetActive(false);
        }
    }
}
