using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : UI_Scene
{
    enum Objects
    {
        InvenContent,
    }

    private int invenCount = 8;
    public List<InvenFragment> invenList= new List<InvenFragment>();
    public ItemCombinScriptable[] combinDatas;

    public void SetInfo(ItemCombinScriptable[] datas)
    {
        combinDatas = datas;
    }
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        BindObject(typeof(Objects));

        for(int i = 0; i < invenCount; i++)
        {
            Manager.UI.MakeSubItem<InvenFragment>(GetObject((int)Objects.InvenContent).transform, callback:(inven) =>
            {
                invenList.Add(inven);
            });
        }

        return true;
    }

    public InvenFragment InvenType(Define.TileType type)
    {
        foreach(InvenFragment inven in invenList)
        {
            if(inven._type == type)
                return inven;
        }

        Debug.LogWarning("이상발생");
        return null;
    }
    public InvenFragment CheckItem(Define.TileType type)
    {
        foreach(InvenFragment fragment in invenList)
        {
            if (fragment.CheckItem(type))
                return fragment;
            
        }
        foreach (InvenFragment fragment in invenList)
        {
            if (fragment.CheckNoneItem(type))
                return fragment;
        }
        Debug.LogError("가득하다");
        return null;
    }
}
