using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPop : UI_Scene
{
    private const int INVEN_COUNT = 32;
    private List<InvenFragment> _invenList;
    private List<UI_InventroyFragment> _inventroyList;
    enum Objects
    {
        InventoryContent,
    }
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));

        for(int i = 0; i < INVEN_COUNT; i++)
        {
            Manager.UI.MakeSubItem<UI_InventroyFragment>(parent: GetObject((int)Objects.InventoryContent).transform, callback: (fragment) =>
            {
                _inventroyList.Add(fragment);
            });
        }

        int value = 0;
        foreach(var item in Manager.Bag.itemDic.Keys)
        {
            Manager.UI.MakeSubItem<UI_BackgroundDrag>(_inventroyList[value].transform, callback: (fragment) =>
            {
                fragment.SetInfo(item);
            });
            value++;
        }
        

        return true;
    }

    public void SetInfo(List<InvenFragment> list)
    {
        _invenList = list;
    }
}
