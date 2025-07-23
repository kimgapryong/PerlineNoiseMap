using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNoiseTexture : MonoBehaviour
{
    public Renderer textureRenderer;
    
    public void CreateTextureMap(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
}
