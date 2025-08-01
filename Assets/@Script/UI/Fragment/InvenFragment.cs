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

    public void SetBagItem(Define.TileType type)
    {
        Debug.Log(type);
        GetText((int)Texts.ItemText).gameObject.SetActive(true);
        GetText((int)Texts.ItemNumText).gameObject.SetActive(true);
        
        _type = type;
        _item = Manager.Bag.GetItem(type);

        GetText((int)Texts.ItemText).text = _item.itemName;
        GetText((int)Texts.ItemNumText).text = _item.count.ToString();

        if(_item.count <= 0)
            ResetItem();

    }
    public bool CheckItem(Define.TileType type)
    {
        if(_type == type ) 
            return true;


        return false;
    }

    public bool CheckNoneItem(Define.TileType type)
    {
        if (_type == Define.TileType.None )
            return true;

        return false;
    }
    public void UseItem(int count = 1)//Action callback)
    {
        if(_type == Define.TileType.None )
            return;

        //callback?.Invoke();

        if (!Manager.Bag.UseBagItem(_type, count))
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
