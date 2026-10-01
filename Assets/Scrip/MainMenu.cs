using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public async void OnPlayButtonClicked()
    {
        await SceneLoadingManager.LoadScene("Level");
        Debug.Log("Loading complete!");
    }
    
    void Start()
    {

    }

    void Update()
    {

    }
}

