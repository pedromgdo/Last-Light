using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadScene(int buildIndex) {
        SceneManager.LoadScene(buildIndex);
        Debug.Log("Loaded new scene.");
    }
    public void ReloadSene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reloaded current scene.");
    }
    public void QuitGame() {
        Application.Quit();
        Debug.Log("Quit the application.");
    }
}
