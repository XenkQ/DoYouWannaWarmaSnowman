using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
