using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    Dictionary<Vector3Int, Tile> _tileDic = new Dictionary<Vector3Int, Tile>();
    public enum ChooiseMap {NoiseMap, ColorMap }
    public ChooiseMap chooise;

    public int maxWidth;
    public int maxHeight;
    public float noiseScale;

    public int octaves;

    [Range(0f, 1f)]
    public float wave;
    public float cycle;

    public int seed;
    public Vector2 vec;

    public bool update;

    public MapStruct[] regions;

    [Range(10f,100f)]
    public int heightPoint;

    [Range(1, 10)]
    public int groundPoint;
    //오브젝트가 한번에 이동해야 하는 위치
    private int objTrans = 4;

    
    

    private void Start()
    {
        CraeteMapGenerator();
    }
    public void CraeteMapGenerator()
    {
        float[,] noiseMap = Noise.GetNoise(maxWidth, maxHeight, seed, noiseScale, octaves, wave, cycle, vec);

        for(int y = 0; y < maxHeight; y++)
        {
            for(int x = 0; x < maxWidth; x++)
            {
                Vector3Int dicVec = new Vector3Int(x, 0, y);
                GameObject obj = Manager.Resources.Instantiate("Water", (Vector3)dicVec * objTrans, Quaternion.identity);
                Debug.Log(obj);
                Tile tile = obj.AddComponent<Tile>().GetSetTile("물",Define.TileType.Water, 0, 100);
                _tileDic.Add(dicVec, tile);
            }
        }

        for (int y = 0; y < maxHeight; y++)
            for (int x = 0; x < maxWidth; x++)
                for (int i = 0; i < regions.Length; i++)
                    if (noiseMap[y, x] <= regions[i].height)
                    {
                        for(int value = 0; value < noiseMap[y,x] * heightPoint; value++)
                        {
                            Vector3Int dicVec = new Vector3Int(x, value, y);
                            GameObject obj = Manager.Resources.Instantiate("Rock", (Vector3)dicVec * objTrans, Quaternion.identity);
                            Tile tile = obj.AddComponent<Tile>().GetSetTile("돌", Define.TileType.Rock, value, 100);
                            if (_tileDic.TryGetValue(dicVec, out Tile curtile))
                            {
                                Destroy(curtile.gameObject);

                                _tileDic[dicVec] = tile;
                                continue;
                            }
                            _tileDic.Add(dicVec, tile);
                        }
                        break;
                    }

        Vector3Int[] vecs = new Vector3Int[5] { Vector3Int.up, Vector3Int.forward, Vector3Int.left, Vector3Int.back, Vector3Int.right };
       
        for(int y  = 0; y < maxHeight; y++)
        {
            for(int x = 0; x < maxWidth; x++)
            {
                for(int i = 0; i < noiseMap[y, x] * heightPoint; i++)
                {
                    Vector3Int curVec = new Vector3Int(x, i, y);

                    if (!_tileDic.ContainsKey(curVec))
                        continue;
                    if (curVec.x < 0 || curVec.z < 0 || curVec.x >= maxWidth || curVec.z >= maxHeight)
                        continue;
                    if (_tileDic[curVec].tileType != Define.TileType.Rock)
                        continue;


                    for (int k = 0; k < vecs.Length; k++)
                    {
                        for(int g = 1; g <= groundPoint; g++)
                        {
                            Vector3Int vec = new Vector3Int(x, i, y) + vecs[k] * g;

                            if (_tileDic.ContainsKey(vec))
                                continue;
                            if (vec.x < 0 || vec.z < 0 || vec.x >= maxWidth || vec.z >= maxHeight)
                                continue;


                            GameObject obj = Manager.Resources.Instantiate("Iland", (Vector3)vec * objTrans, Quaternion.identity);
                            Tile tile = obj.AddComponent<Tile>().GetSetTile("흙", Define.TileType.Ground, i, 100);
                            _tileDic.Add(vec, tile);

                        }
                    }
                }
            }
        }

        Vector3Int[] posVec = new Vector3Int[4] { Vector3Int.forward, Vector3Int.left, Vector3Int.back, Vector3Int.right };
        for (int y = 0; y < maxHeight; y+=8)
        {
            for(int x = 0; x < maxWidth; x+=8)
            {
                Vector3Int treePos = new Vector3Int(x, heightPoint, y);

                for (int rand = 0; rand < 8; rand++)
                {
                    treePos += posVec[Random.Range(0, posVec.Length)];
                }
                    

                if (treePos.x < 0 || treePos.z < 0 || treePos.x >= maxWidth || treePos.z >= maxHeight)
                    continue;
                if (!_tileDic.ContainsKey(treePos))
                    treePos = CheckIslandDown(treePos);

                if (_tileDic[treePos].tileType == Define.TileType.Water)
                    continue;

                CreateTree(treePos);
                
            }
        }
    }
    public Vector3Int CheckIslandUp(Vector3Int vec)
    {
        Vector3Int vecPos = vec;
        if (_tileDic.ContainsKey(vec))
        {
            vecPos = CheckIslandUp(vec + Vector3Int.up); 
        }
        return vecPos;
    }
    public Vector3Int CheckIslandDown(Vector3Int vec)
    {
        Vector3Int vecPos = vec;
        if (!_tileDic.ContainsKey(vec))
        {
            vecPos = CheckIslandDown(vec + Vector3Int.down);
        }
        return vecPos;
    }
    public void CreateTree(Vector3Int vec)
    {
        Vector3Int[] leafArray = new Vector3Int[5] { Vector3Int.up, Vector3Int.forward, Vector3Int.left, Vector3Int.back, Vector3Int.right };
        Vector3Int curVec = vec;
        for (int i = 0; i < 6; i++)
        {
            curVec += Vector3Int.up;
            
            GameObject obj = Manager.Resources.Instantiate("Tree", (Vector3)curVec * objTrans, Quaternion.identity);
            Tile tile = obj.AddComponent<Tile>().GetSetTile("나무", Define.TileType.Tree, i, 100);
            if (_tileDic.ContainsKey(curVec))
                continue;

            
            _tileDic.Add(curVec, tile);

            if(i > 4)
            {
                for(int k = 0; k < leafArray.Length; k++)
                {
                    Vector3Int leafVec = curVec + leafArray[k];
                    Debug.Log(leafVec);
                    GameObject leaf = Manager.Resources.Instantiate("Leaf", (Vector3)leafVec * objTrans, Quaternion.identity);
                    Tile leafTile = leaf.AddComponent<Tile>().GetSetTile("나뭇잎", Define.TileType.Leaf, i, 100);
                    if (_tileDic.ContainsKey(leafVec))
                        continue;
                    _tileDic.Add(leafVec, leafTile);
                }
            }
        }

    }
    public void CreateMapTextureGenerator()
    {
        float[,] noiseMap = Noise.GetNoise(maxWidth, maxHeight, seed, noiseScale, octaves, wave, cycle, vec);
        Texture2D texture = new(0,0);

        Color[] colorMap = new Color[maxWidth * maxHeight];
        for(int y =0; y < maxHeight; y++)
            for(int x =0; x < maxWidth; x++)
                for(int i = 0; i < regions.Length; i++)
                    if (noiseMap[y,x] <= regions[i].height)
                    {
                        colorMap[y * maxWidth + x] = regions[i].color;
                        break;
                    }

        switch (chooise)
        {
            case ChooiseMap.NoiseMap:
                texture = TextureGenerator.NoiseTexture(noiseMap);
                break;

            case ChooiseMap.ColorMap:
                texture = TextureGenerator.ColorTexture(colorMap, maxWidth, maxHeight);
                break;
        }

        CreateNoiseTexture createNoise =  FindObjectOfType<CreateNoiseTexture>();
        createNoise.CreateTextureMap(texture);
    }

}

[System.Serializable]
public struct MapStruct
{
    public string name;
    public float height;
    public Color color;
    public GameObject obj;
}
