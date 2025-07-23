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
    private List<InvenFragment> invenList= new List<InvenFragment>();
    
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

    public InvenFragment CheckItem(Define.TileType type)
    {
        foreach(InvenFragment fragment in invenList)
        {
            if (fragment.CheckItem(type))
                return fragment;
        }

        Debug.LogError("∞°µÊ«œ¥Ÿ");
        return null;
    }
}
