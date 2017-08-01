using System.Collections;
using UnityEngine;

/// <summary>
/// This component manages the stuff that's rendered by the HUD camera, which is rendered on top
/// of the main first person camera and doesn't move.
/// </summary>
public class HUDController : MonoBehaviour
{
    #region Component Configuration

        [Tooltip("Reference to the child game game object that is the visible gun.")]
        [SerializeField] GameObject _outerGunChild;

        [Tooltip("Reference to the part of the gun that slides when fired.")]
        [SerializeField] GameObject _innerGunChild;

        [Tooltip("Multiplier on the amount to sway the gun while walking.")]
        [SerializeField] float _gunBobMultiplier;

        [Tooltip("Time in seconds to play the recoil animation when shot.")]
        [SerializeField] float _gunRecoilAnimationTime;

        [Tooltip("New local position to tween the entire gun to when recoiling.")]
        [SerializeField] Vector3 _outerGunRecoilPosition;

        [Tooltip("Y-position to slide the slidey part of the gun back to")]
        [SerializeField] float _innerGunRecoilPositionY;

    #endregion

    Vector3 _gunOrigin;
    float _innerGunStartY;
    Vector3 _curRecoilOffset;

    void Awake()
    {
        _innerGunStartY = _innerGunChild.transform.localPosition.y;
        _gunOrigin = _outerGunChild.transform.localPosition;
    }

    /// <summary>
    /// Sets the absolute bobbing position of the gun. This is expected to be called every frame.
    /// </summary>
    /// <param name="bob">The offset of the gun in a scaled screen space.</param>
    public void SetGunBob(Vector2 bob)
    {
        _outerGunChild.transform.localPosition = _gunOrigin - _gunBobMultiplier * bob.AsVector3WithZ(0f) + _curRecoilOffset;
    }

    /// <summary>
    /// Triggers the firing animation of the gun.
    /// </summary>
    public void AnimateGunShot()
    {
        StartCoroutine(animateGunShotRoutine());
    }

    IEnumerator animateGunShotRoutine()
    {
        for (var t = 0f; t < 1f; t += Time.deltaTime / _gunRecoilAnimationTime) {
            var easedT = -t*(t-2f);

            _innerGunChild.transform.localPosition = _innerGunChild.transform.localPosition.WithY(
                Mathf.Lerp(_innerGunRecoilPositionY, _innerGunStartY, easedT)
            );
            _curRecoilOffset = Vector3.Lerp(_outerGunRecoilPosition - _gunOrigin, Vector3.zero, easedT);

            yield return new WaitForEndOfFrame();
        }
    }
}