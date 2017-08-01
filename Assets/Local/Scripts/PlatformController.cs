using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component controls the behaviour of the TNT platforms that the player launches from their gun.
/// </summary>
public class PlatformController : MonoBehaviour 
{
    #region Component Configuration
    
        [Tooltip("Time in seconds that the platform takes to unfold when first shot.")]
        [SerializeField] float _delayToStart = 0.5f;

        [Tooltip("Speed of the platform when exiting the gun.")]
        [SerializeField] float _launchSpeed = 20f;
        
        [Tooltip("Impulse transferred to player when the platform explodes.")]
        [SerializeField] float _explosionPower = 10f;
        
        [Tooltip("Y-position in world space where the platform is destroyed if it falls below.")]
        [SerializeField] float _deathElevation = -5f;
        
        [Tooltip("Offset of the explosion force. Does not affect spawned explosion FX.")]
        [SerializeField] Vector3 _explosionSourceOffset = Vector3.down;
        
        [Tooltip("Prefab to spawn at the platform's position when it explodes.")]
        [SerializeField] GameObject _explosionPrefab;

        [Tooltip("Name of the layer the platform moves to once it's fully unfurled.")]
        [SerializeField] string _unfurledLayer;

        [Tooltip("The platform material tints from this color while unfurling.")]
        [SerializeField] Color _preUnfurlColor; 

        [Tooltip("The platform material is set to this color when the platform is ready.")]
        [SerializeField] Color _finalColor; 

    #endregion

    PlayerController _player;
    Rigidbody _rigidbody;
    Vector3 _savedVelocity;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    IEnumerator Start()
    {
        var renderer = GetComponentInChildren<Renderer>();
        var initialScale = renderer.transform.localScale;
        var startOrientation = Quaternion.Euler(180f, Random.value * 360f, 0f);
        var material = renderer.material;

        for (var t = 0f; t < 1f; ) {
            if (!_rigidbody.isKinematic) {
                var t2 = t * t;
                renderer.transform.rotation = Quaternion.Slerp(startOrientation, Quaternion.identity, t2);
                renderer.transform.localScale = Vector3.Lerp(0.001f * Vector3.one, initialScale, Mathf.Clamp01(3f * t2));
                material.color = Color.Lerp(_preUnfurlColor, _finalColor, t / 2f);
                t += Time.deltaTime / _delayToStart;
            }

            yield return new WaitForEndOfFrame();
        }

        gameObject.layer = LayerMask.NameToLayer(_unfurledLayer);
        material.color = _finalColor; 
        renderer.transform.localScale = initialScale;
        renderer.transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Initializes the platform state, should be called right after creating an instance of one.
    /// </summary>
    /// <param name="player">The PlayerController that launched the platform.</param>
    /// <param name="launchDirection">Normalized vector pointing in the direction to launch.</param>
    public void Init(PlayerController player, Vector3 launchDirection)
    {
        _player = player;
        _rigidbody.velocity = _launchSpeed * launchDirection;
    }

    /// <summary>
    /// Pauses or unpauses the platform in time.
    /// </summary>
    /// <param name="paused">Whether the platform should be paused or not.</param>
    public void SetPaused(bool paused)
    {
        if (paused == _rigidbody.isKinematic) return;

        if (paused) {
            _savedVelocity = _rigidbody.velocity;
            _rigidbody.isKinematic = true;
        } else {
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = _savedVelocity;
        }
    }

    void FixedUpdate()
    {
        if (_rigidbody.position.y < _deathElevation) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        _player.RocketJump(transform.position + _explosionSourceOffset, _explosionPower);
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}