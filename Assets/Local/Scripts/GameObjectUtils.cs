using UnityEngine;

/// <summary>
/// Some useful functions for working with GameObjects.
/// </summary>
static public class GameObjectUtils
{
    /// <summary>
    /// Call GameObject.Destroy on all GameObjects which have the provided component attached.
    /// </summary>
    /// <typeparam name="T"> Type of the component to find. </typeparam>
    static public void DestroyAll<T>() where T : MonoBehaviour
    {
        foreach (var c in Object.FindObjectsOfType<T>()) {
            GameObject.Destroy(c.gameObject);
        }
    }
}