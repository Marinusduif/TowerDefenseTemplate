using UnityEngine;
using UnityEngine.SceneManagement;

public class inputsceneloader : MonoBehaviour
{
    // Define a public KeyCode variable for the input key
    public KeyCode loadSceneKey = KeyCode.Space;

    // Define a public string variable for the scene name to load
    public string sceneNameToLoad;

    // Update is called once per frame
    void Update()
    {
        Input();
    }

    public void Input()
    {
        // Check if the specified key is pressed
        if (Input.GetKeyDown(loadSceneKey))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
