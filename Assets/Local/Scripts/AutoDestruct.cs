using System.Collections;
using UnityEngine;

/// <summary>
/// This component simply causes its GameObject to be destroyed a configurable
/// amount of time after coming in to existence. Useful for one-off FX objects.
/// </summary>
public class AutoDestruct : MonoBehaviour 
{
    #region Component Configuration

        [Tooltip("Time in seconds to wait before destroying the GameObject.")]
        [SerializeField] float _delay;

    #endregion

    IEnumerator Start() 
    {
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }
}