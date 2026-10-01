using UnityEngine;

public class Exit : MonoBehaviour
{
    public void OnExitClicked()
    {
        // let the game stop run and exit the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // this part will only run when you build the game, when I build this code can run but I dont know is that different pc can run the same or not.
        Application.Quit();
#endif
    }
}