using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager 
{
    Dictionary<Define.TileType, BagItem> itemDic = new Dictionary<Define.TileType, BagItem>();

    public void SetItem(Define.TileType type,string name)
    {
        //처음 아이템의 갯수을 보고 청소해줌
        GarbegeItem(type);

        BagItem item;
        if(itemDic.TryGetValue(type, out item))
        {
            item.count++;
            itemDic[type] = item;
            return;
        }

        item = new BagItem() { name = name, count = 1, type = type };
        itemDic.Add(type, item);
    }

    private void GarbegeItem(Define.TileType type)
    {
        BagItem item;
        if (!itemDic.TryGetValue(type, out item))
            return;

        if(item.count <= 0)
            itemDic.Remove(type);
    }
}

public struct BagItem
{
    public string name;
    public int count;
    public Define.TileType type;

}

