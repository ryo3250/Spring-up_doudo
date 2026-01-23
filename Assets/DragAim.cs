using UnityEditor;
using UnityEngine;

public class DragAim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("参照")]
    [SerializeField] private Transform playerTransfrom;

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
    }

    void Update()
    {
        //ボタンが押されたら
        if (Input.GetMouseButtonDown(0)) {
            //startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.enabled = true;
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

            //長さに応じて色を変える
            float t = clampedDir.magnitude / maxDragDistance;
            Color lineColor = Color.Lerp(weakColor, strongColor, t);

            //最大距離で制限
            line.startColor = lineColor;
            line .endColor = lineColor;

            
        }

        //ボタンを離したら
        if (Input.GetMouseButtonUp(0)) 
        {
            line.enabled = false;
        }
    }
}
