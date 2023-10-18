using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class videosceneswitch : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    void Start()
    {
        // Add an event listener to detect when the video has finished playing
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Unsubscribe from the event to prevent multiple scene switches
        videoPlayer.loopPointReached -= OnVideoFinished;

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
