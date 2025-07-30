using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Combin Data", menuName = "Combin Data")]
public class ItemCombinScriptable : ScriptableObject
{
    public string itemName;
    public int count;
    public Define.TileType type;
    public CombineData[] datas;
}

[Serializable]
public struct CombineData
{
    public Define.TileType type;
    public int count;
}
