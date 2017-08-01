using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// This component triggers a fade-out followed by a scene change when any object
/// intersects with it.
/// </summary>
public class WinPointController : MonoBehaviour 
{
    #region Component Configuration

        [Tooltip("Name of the scene to load after fading out.")]
        [SerializeField] string _winSceneName;

    #endregion

    bool _triggered;

    void OnTriggerEnter(Collider collider)
    {
        if (_triggered) return;
        _triggered = true;

        ScoreTracker.EndTime = Time.time;

        FindObjectOfType<CameraFader>().FadeOut(() => {
            SceneManager.LoadScene(_winSceneName);
        });
    }
}