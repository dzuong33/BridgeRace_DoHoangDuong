using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    private UI_Manager uiManager;

    private void Start()
    {
        // Find the UIManager in the scene
        uiManager = FindObjectOfType<UI_Manager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player triggered the finish line, show the win panel using the UIManager
            if (uiManager != null)
            {
                uiManager.PopUpWinPanel();
                Time.timeScale = 0;
            }
        }
        if (other.CompareTag("Enemy"))
        {
            // Player triggered the finish line, show the win panel using the UIManager
            if (uiManager != null)
            {
                uiManager.PopUpLosePanel();
                Time.timeScale = 0;
            }
        }
    }
}
