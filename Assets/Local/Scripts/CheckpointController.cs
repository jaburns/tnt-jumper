using UnityEngine;

/// <summary>
/// This component saves its position to a static field when it has been intersected.
/// It also changes color and applies an angular velocity to its attached Rigidbody.
/// </summary>
public class CheckpointController : MonoBehaviour 
{
    #region Component Configuration

        [Tooltip("Color to become when the player hits this checkpoint.")]
        [SerializeField] Color _checkedColor;

        [Tooltip("Angular velocity to take when hit.")]
        [SerializeField] float _spin;

        [Tooltip("If this is set, spawn the player here when starting the game.")]
        [SerializeField] bool _debugStartHere;

    #endregion

    /// <summary>
    /// Initially set to the origin, takes on the position of the last checkpoint to be touched.
    /// </summary>
    static public Vector3 LatestPosition { get; private set; }

#if UNITY_EDITOR
    void Awake()
    {
        if (_debugStartHere) {
            LatestPosition = transform.position;
        }
    }
#endif

    void OnTriggerEnter(Collider collider)
    {
        GetComponent<Rigidbody>().angularVelocity = Vector3.up * _spin;
        GetComponent<Renderer>().material.color = _checkedColor;

        LatestPosition = transform.position;
    }
}