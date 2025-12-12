using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectButton : MonoBehaviour
{
    public void SelectStage(int stageNumber) 
    {
        PlayerPrefs.SetInt("SelectedStage", stageNumber);
        SceneManager.LoadScene("main ground");
    }
}
