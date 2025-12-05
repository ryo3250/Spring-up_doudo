using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectButton : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("SelectedStage", -1);
    }

    public void SelectStage(int stageNumber) 
    {
        PlayerPrefs.SetInt("SelectedStage", stageNumber);
        SceneManager.LoadScene("main ground");
    }
}
