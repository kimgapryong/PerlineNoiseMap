using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager 
{
    Dictionary<Define.TileType, BagItem> itemDic = new Dictionary<Define.TileType, BagItem>();

    public void SetItem(Define.TileType type,string name, int count = 1)
    {
        //ó�� �������� ������ ���� û������
        //GarbegeItem(type);

        BagItem item;
        if(itemDic.TryGetValue(type, out item))
        {
            item.count += count;
            itemDic[type] = item;
            return;
        }
        
        item = new BagItem() { itemName = name, count = count, type = type };
        itemDic.Add(type, item);
        Debug.Log($"{item.itemName} ���");
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

        Debug.LogError("item�� �������� ����");
        return item;
    }
    public void GarbegeItem(Define.TileType type)
    {
        BagItem item;
        if (!itemDic.TryGetValue(type, out item))
            return;

        if(item.count <= 0)
            itemDic.Remove(type);

        //ui�� û���ؾ� �Ѵ�
    }
}

public struct BagItem
{
    public string itemName;
    public int count;
    public Define.TileType type;
    
}

