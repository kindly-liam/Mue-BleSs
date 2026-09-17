using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Play;
    public GameObject Summary;
    public GameObject Exit;
    public GameObject Title;
    public GameObject TextoResumen;
    public GameObject GoBack;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumenOn()
    {
        Play.SetActive(false);
        Summary.SetActive(false);
        Exit.SetActive(false);
        Title.SetActive(false);
        TextoResumen.SetActive(true);
        GoBack.SetActive(true);
        
    }

    public void ResumenOff()
    {
        Play.SetActive(true);
        Summary.SetActive(true);
        Exit.SetActive(true);
        Title.SetActive(true);
        TextoResumen.SetActive(false);
        GoBack.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("BUILD FINAL"); 
    }
}