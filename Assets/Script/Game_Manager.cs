using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    //シングルトンの宣言
    public static Game_Manager Instance {  get; private set; }

    private int hitCount = 0; // あたり回数

    [SerializeField] private Text hitCountText; 
    private void Awake()
    {
        //シングルトン(どこからでも呼べるようにする）
        if (Instance == null)
        {
            Instance = this;
        }
        else //それ以外は削除
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        UpdateHitText();//ゲーム開始時に初期値を表示
    }
    public void AddHitCount()
    {
        hitCount++;//ヒット回数を増やす
        Debug.Log("現在のヒット数:" + hitCount);

        UpdateHitText();//カウントが増えたら表示を更新
    }

    public int GetHitCount() //このスクリプト外から見るための関数
    {
       return hitCount;
    }

    private void UpdateHitText()
    {
        if (hitCountText != null) 
        {
            hitCountText.text = "ヒット数:" + hitCount.ToString();
        }
    }
}
