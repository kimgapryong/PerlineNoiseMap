using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager 
{
    public Dictionary<Define.TileType, BagItem> itemDic = new Dictionary<Define.TileType, BagItem>();
    public Func<Define.TileType, UI_BackgroundDrag> SetitemAction;
    public void SetItem(Define.TileType type, string name, int count = 1)
    {
        UI_BackgroundDrag back = SetitemAction?.Invoke(type);

        if (itemDic.TryGetValue(type, out BagItem item))
        {
            item.count += count;
            itemDic[type] = item;  // struct는 반드시 재할당 필요
        }
        else
        {
            item = new BagItem { itemName = name, count = count, type = type };
            itemDic.Add(type, item);
        }

        back?.Refresh(type);
    }
    public bool UseBagItem(Define.TileType type, int count = 1)
    {
        BagItem item;
        if (itemDic.TryGetValue(type, out item) == false)
            return false;

        item.count -= count;
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
    public string itemName;
    public int count;
    public Define.TileType type;
    
}

