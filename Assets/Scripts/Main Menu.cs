using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu; 
    public GameObject creditsMenu;  

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }

    public void ToggleSettings()
    {
        if (settingsMenu != null)
        {
            settingsMenu.SetActive(!settingsMenu.activeSelf);
        }
      
    }

    public void ToggleCredits()
    {
        if (creditsMenu != null)
        {
            creditsMenu.SetActive(!creditsMenu.activeSelf);
        }
      
    }
}
