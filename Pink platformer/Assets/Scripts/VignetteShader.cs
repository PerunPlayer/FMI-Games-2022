 using UnityEngine;

[ExecuteInEditMode]
public class VignetteShader : MonoBehaviour
{
    public Material material;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
