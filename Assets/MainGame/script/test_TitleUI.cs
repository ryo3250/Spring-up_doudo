using UnityEngine;

public class test_TitleUI : MonoBehaviour
{
    public void OnStartButton() 
    {
        test_Game_Manager.Instance.GoStageSelect();
    }
}
