using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private Button resetButton;

    private void Start()
    {
        if (resetButton != null)
            resetButton.onClick.AddListener(() =>
            {
                Game_Manager.Instance.ResetGame();
            });
    }
}
