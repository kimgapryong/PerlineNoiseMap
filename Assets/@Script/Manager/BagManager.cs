using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager 
{
    Dictionary<Define.TileType, BagItem> itemDic = new Dictionary<Define.TileType, BagItem>();

    public void SetItem(Define.TileType type,string name, Color color)
    {
        //처음 아이템의 갯수을 보고 청소해줌
        //GarbegeItem(type);

        BagItem item;
        if(itemDic.TryGetValue(type, out item))
        {
            item.count++;
            itemDic[type] = item;
            return;
        }

        item = new BagItem() { name = name, count = 1, type = type, color = color };
        itemDic.Add(type, item);
    }
    public bool UseBagItem(Define.TileType type)
    {
        BagItem item;
        if (itemDic.TryGetValue(type, out item) == false)
            return false;

        item.count--;
        itemDic[type] = item;

        if(item.count <= 0)
        {
            GarbegeItem(type);
            return false;
        }
        return true;
    }
    public BagItem GetItem(Define.TileType type)
    {
        BagItem item;
        if (itemDic.TryGetValue(type, out item))
            return item;

        Debug.LogError("item이 존재하지 않음");
        return item;
    }
    public void GarbegeItem(Define.TileType type)
    {
        BagItem item;
        if (!itemDic.TryGetValue(type, out item))
            return;

        if(item.count <= 0)
            itemDic.Remove(type);

        //ui도 청소해야 한다
    }
}

public struct BagItem
{
    public string name;
    public int count;
    public Define.TileType type;
    public Color color;
}

