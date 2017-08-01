using UnityEngine;

/// <summary>
/// A simple interface for playing some one-shot sounds and loops. This implementation certainly
/// wouldn't scale, but it's the right level of simplicity for this project.
/// </summary>
public class SoundPlayer : MonoBehaviour 
{
    #region Component Configuration

        [Tooltip("An AudioSource with the gunshot sound attached and configued.")]
        [SerializeField] AudioSource _shootSound;

        [Tooltip("An AudioSource with the footsteps loop attached and configued.")]
        [SerializeField] AudioSource _stepsSound;

    #endregion

    bool _playingSteps;

    public void SetFootstepsPlaying(bool playing)
    {
        if (_playingSteps == playing) return;
        _playingSteps = playing;

        if (playing) _stepsSound.Play();
        else _stepsSound.Stop();
    }
    
    public void PlayGunshotSound()
    {
        _shootSound.Play();
    }
}