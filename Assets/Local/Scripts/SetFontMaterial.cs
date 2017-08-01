using UnityEngine;

/// <summary>
/// This component exists as a bit of a hack in order to use a custom material with Unity's default
/// 3D font. The texture it generates isn't exposed easily, so this component steals the texture and
/// assigns it to a provided custom material before putting it back on the renderer.
/// </summary>
public class SetFontMaterial : MonoBehaviour 
{
    #region Component Configuration

        [Tooltip("The new material to use with the default font renderer.")]
        [SerializeField] Material _overrideMaterial;

    #endregion

    void Awake() 
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        _overrideMaterial.SetTexture("_MainTex", meshRenderer.sharedMaterial.GetTexture("_MainTex"));
        meshRenderer.sharedMaterial = _overrideMaterial;
    }
}