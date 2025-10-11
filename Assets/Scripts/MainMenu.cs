using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        // unlock cursor when in the main menu scene
        Cursor.lockState = CursorLockMode.Confined;
    }

    // starts the game
    public void StartButton()
    {
        // changes the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // stop the main menu background music
        FindObjectOfType<AudioManager>().Stop("Main_Menu");
    }

    // quits the game leaving the closing the application
    public void QuitButton()
    {
        // quit the game
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
