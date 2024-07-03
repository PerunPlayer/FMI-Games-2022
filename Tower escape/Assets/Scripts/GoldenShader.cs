using UnityEngine;

[ExecuteInEditMode]

public class GoldenShader : MonoBehaviour
{
    public Material material;
    public Shader shader;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material = new Material(shader);
        Graphics.Blit(source, destination, material);
    }
}