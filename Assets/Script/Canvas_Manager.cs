using UnityEngine;
using UnityEngine.Rendering;

public class Canvas_Manager : MonoBehaviour
{
    public Canvas gameCanvas;

    public void ShowCanvas()
    {
        gameCanvas.gameObject.SetActive(true);
    }

    public void HideCanvas()
    {
        gameCanvas.gameObject.SetActive(false);
    }
}
