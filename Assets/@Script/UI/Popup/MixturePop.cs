using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixturePop : UI_Pop
{
    ItemCombinScriptable _items;
    List<IngredientFragment> _ingredient = new List<IngredientFragment>();
    enum Texts
    {
        ItemTxt,
    }
    enum Buttons
    {
        CombinBtn,
    }
    enum Objects
    {
        IngredientContent,
    }
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindObject(typeof(Objects));

        GetText((int)Texts.ItemTxt).text = _items.itemName;

        Debug.Log(GetText((int)Texts.ItemTxt).transform);
        Debug.Log(GetButton((int)Buttons.CombinBtn).transform);
        Debug.Log(GetObject((int)Objects.IngredientContent));

        for(int i = 0; i < _items.datas.Length; i++)
        {
            int index = i;
            Manager.UI.MakeSubItem<IngredientFragment>(GetObject((int)Objects.IngredientContent).transform,
                callback: (fragment) =>
            {
                CombineData curData = _items.datas[index];
                BagItem curBag = Manager.Bag.GetItem(curData.type);

                bool check = true;
                if(curBag.count < curData.count)
                    check = false;

                fragment.SetInfo(curBag, curData, check);
                _ingredient.Add(fragment);
            });

        }

        BindEvent(GetButton((int)Buttons.CombinBtn).gameObject, MakeItem);
        return true;
    }

    public void SetInfo(ItemCombinScriptable items)
    {
        _items = items;
    }

    private void MakeItem()
    {
        if(!CheckItem())
            return;

        MainCanvas canvas = Manager.UI.SceneUI as MainCanvas;

        foreach (CombineData item in _items.datas)
        {
            InvenFragment inventory = canvas.InvenType(item.type);
            inventory.UseItem(item.count);
            inventory.SetBagItem(item.type);
        }
            

        foreach (IngredientFragment fragment in _ingredient)
            fragment.Refresh();

        Manager.Bag.SetItem(_items.type, _items.name, _items.count);

       
        InvenFragment inven = canvas.CheckItem(_items.type);
        inven.SetBagItem(_items.type);
       
    }
    private bool CheckItem()
    {
        foreach(CombineData item in _items.datas)
        {
            if(Manager.Bag.GetItem(item.type).count < item.count)
                return false;
        }

        return true;
    }
}
