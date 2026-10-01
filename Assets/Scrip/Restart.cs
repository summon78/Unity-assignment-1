using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void OnRestartClicked()
    {
        // restart the timer
        Time.timeScale = 1f;

        // reload the scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}