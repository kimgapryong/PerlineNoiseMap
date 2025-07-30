using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientFragment : UI_Base
{
    BagItem _item;
    CombineData _combin;
    bool check;
    enum Texts
    {
        ItemName,
        ItemNumTxt,
    }

    public override bool Init()
    {
        if(base.Init() == false)    
            return false;

        BindText(typeof(Texts));

        
        GetText((int)Texts.ItemName).text = _item.itemName;
        Refresh();
       
        return true;
    }

    public void Refresh()
    {
        _item = Manager.Bag.GetItem(_item.type);

        GetText((int)Texts.ItemNumTxt).text = $"{_item.count}/{_combin.count}";

        if(_item.count < _combin.count)
            check = false;
        else 
            check = true;

        if (check)
            transform.Find("Image").GetComponent<Image>().color = Color.green;
        else
            transform.Find("Image").GetComponent<Image>().color = Color.red;
    }
    public void SetInfo(BagItem item,CombineData combin, bool check)
    {
        this.check = check;
        _combin = combin;
        _item = item;
    }
}
