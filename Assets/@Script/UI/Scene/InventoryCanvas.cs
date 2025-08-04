using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCanvas: UI_Scene
{
    private const int INVEN_COUNT = 32;
    private List<UI_InventroyFragment> _inventroyList = new List<UI_InventroyFragment>();
    enum Objects
    {
        InventoryContent,
    }
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));

        Manager.Bag.SetitemAction -= GetBackground;
        Manager.Bag.SetitemAction += GetBackground;

        for(int i = 0; i < INVEN_COUNT; i++)
        {
            Manager.UI.MakeSubItem<UI_InventroyFragment>(GetObject((int)Objects.InventoryContent).transform, "UI_InventroyFramgent", callback: (fragment) =>
            {
                Debug.Log(fragment);
                _inventroyList.Add(fragment);
            });
        }

        int value = 0;
        foreach(var item in Manager.Bag.itemDic.Keys)
        {
            UI_InventroyFragment curInven = _inventroyList[value];
            Manager.UI.MakeSubItem<UI_BackgroundDrag>(curInven.transform, callback: (fragment) =>
            {
                fragment.SetInfo(item);
                curInven.SetItem(item);
            });
            value++;
        }
        

        return true;
    }

    public UI_BackgroundDrag GetBackground(Define.TileType type)
    {
        foreach(var back in _inventroyList)
        {
            UI_BackgroundDrag ui = back.gameObject.GetComponentInChildren<UI_BackgroundDrag>();

            if(ui == null)
                continue;

            if(ui._type == type)
                return ui;
        }

        foreach(var back in _inventroyList)
        {
            UI_BackgroundDrag ui = back.gameObject.GetComponentInChildren<UI_BackgroundDrag>();

            if(ui != null)
                continue ;

            Manager.UI.MakeSubItem<UI_BackgroundDrag>(back.transform, callback: (fragment) =>
            {
                fragment.SetInfo(type);
                ui = fragment;
            });

            Debug.Log(ui);
            return ui;
        }

        return null;
    }
}
