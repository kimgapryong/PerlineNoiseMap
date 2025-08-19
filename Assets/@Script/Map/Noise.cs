using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
   public static float[,] GetNoise(int maxWidth, int maxHeight, int seed, float scale, int octaves, float wave, float reqance, Vector2 vec)
    {
        float[,] noiseList = new float[maxHeight, maxWidth];

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        Vector2[] offsetVec = new Vector2[octaves];
        System.Random rand = new System.Random(seed);
        for(int i = 0; i < octaves; i++)
        {
            float xPos = rand.Next(-10000, 10000) + vec.x;
            float yPos = rand.Next(-10000, 10000) + vec.y;
            offsetVec[i] = new Vector2(xPos, yPos);
        }

        if (scale <= 0)
            scale = 0.00001f;

        for(int y = 0; y < maxHeight; y++)
        {
            for(int x = 0; x < maxWidth; x++)
            {
                float ampltude = 1;
                float wavetude = 1;
                float heightPoint = 0;

                for(int i =0; i < octaves; i++)
                {
                    float xValue = x / scale * wavetude + offsetVec[i].x;
                    float yValue = y / scale * wavetude + offsetVec[i].y;
                    float noiseValue = Mathf.PerlinNoise(xValue, yValue) * 2 - 1;
                    
                    heightPoint += noiseValue * ampltude;
                    ampltude *= wave;
                    wavetude *= reqance;
                }
                
                if(heightPoint > maxValue)
                    maxValue = heightPoint;
                else if(heightPoint < minValue)
                    minValue = heightPoint;

                noiseList[y,x] = heightPoint;
            }
        }

        for (int y = 0; y < maxHeight; y++)
            for (int x = 0; x < maxWidth; x++)
                noiseList[y, x] = Mathf.InverseLerp(minValue, maxValue, noiseList[y,x]);

        return noiseList;
    }
}
