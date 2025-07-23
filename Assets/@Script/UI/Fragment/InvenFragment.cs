using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenFragment : UI_Base
{
    public Define.TileType _type = Define.TileType.None;
    BagItem _item;
   enum Texts
    {
        ItemText,
        ItemNumText,
    }
    public override bool Init()
    {
        if(base.Init() == false)    
            return false;

        BindText(typeof(Texts));

        GetText((int)Texts.ItemText).gameObject.SetActive(false);
        GetText((int)Texts.ItemNumText).gameObject.SetActive(false);
        return true;
    }

    public void SetBagItem(Define.TileType type,BagItem item)
    {
        GetText((int)Texts.ItemText).gameObject.SetActive(true);
        GetText((int)Texts.ItemNumText).gameObject.SetActive(true);
        
        _type = type;
        _item = item;
        Debug.Log(_type);

        GetText((int)Texts.ItemText).text = item.name;
        GetText((int)Texts.ItemNumText).text = item.count.ToString();
    }
    public bool CheckItem(Define.TileType type)
    {
        if(_type == type ) 
            return true;

        if (_type == Define.TileType.None)
            return true;

        return false;
    }
    private void UseItem()
    {
        if(_type == Define.TileType.None )
            return;

        if (!Manager.Bag.UseBagItem(_type))
            ResetItem();

        
    }

    private void ResetItem()
    {
        GetText((int)Texts.ItemText).gameObject.SetActive(false);
        GetText((int)Texts.ItemNumText).gameObject.SetActive(false);

        Manager.Bag.GarbegeItem(_type);
        _type = Define.TileType.None;
    }
    
}
