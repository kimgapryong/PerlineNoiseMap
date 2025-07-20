using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator
{
    public static Texture2D ColorTexture(Color[] colors, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);

        texture.filterMode = FilterMode.Point;  
        texture.wrapMode = TextureWrapMode.Clamp;

        texture.SetPixels(colors);
        texture.Apply();

        return texture;
    }

    public static Texture2D NoiseTexture(float[,] noiseList)
    {
        int xValue = noiseList.GetLength(0);
        int yValue = noiseList.GetLength(1);

        Color[] colorsList = new Color[xValue * yValue];
        for (int y = 0; y < yValue; y++)
            for (int x = 0; x < xValue; x++)
                colorsList[y * xValue + x] = Color.Lerp(Color.black, Color.white, noiseList[y, x]);

        return ColorTexture(colorsList, xValue, yValue);
    }
}
