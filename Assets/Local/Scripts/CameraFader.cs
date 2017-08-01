using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component modifies the material on an attached renderer, fading it in or out by modifying
/// the alpha value of the renderer's primary material's color. It's meant to be placed in front 
/// of a camera to fade the entire view.
/// </summary>
public class CameraFader : MonoBehaviour 
{
    #region Component Configuration

        [Tooltip("Time in seconds to fade from fully opaque to fully transparent.")]
        [SerializeField] float _fadeInTime;

        [Tooltip("Time in seconds to fade from fully transparent to fully opaque.")]
        [SerializeField] float _fadeOutTime;

    #endregion

    Renderer _renderer;
    Material _material;
    Color _color;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _color = _material.GetColor("_Color");

        FadeIn();
    }

    /// <summary>
    /// Sets the color to fully opaque, and then slowly fades to transparent. 
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine(fadeInRoutine());
    }

    /// <summary>
    /// Sets the color to fully transparent, and then slowly fades to opaque. Optionally, you 
    /// can provide a callback to execute once the fade-out has completed. 
    /// </summary>
    /// <param name="then"> Callback to invoke once the fade has completed. </param>
    public void FadeOut(Action then = null)
    {
        StartCoroutine(fadeOutRoutine(then));
    }

    IEnumerator fadeInRoutine()
    {
        for (var t = 0f; t <= 1f; t += Time.deltaTime / _fadeInTime) {
            var easedT = -t*(t-2f);
            _color.a = 1f - easedT;
            _material.SetColor("_Color", _color);
            yield return new WaitForEndOfFrame();
        }

        _renderer.enabled = false;
    }

    IEnumerator fadeOutRoutine(Action then)
    {
        _renderer.enabled = true;

        for (var t = 0f; t <= 1f; t += Time.deltaTime / _fadeOutTime) {
            _color.a = t;
            _material.SetColor("_Color", _color);
            yield return new WaitForEndOfFrame();
        }

        if (then != null) {
            then();
        }
    }
}