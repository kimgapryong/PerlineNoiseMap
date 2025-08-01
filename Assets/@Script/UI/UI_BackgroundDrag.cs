using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_BackgroundDrag : UI_Base, IDragHandler
{
    private Define.TileType _type;
    private BagItem _item;
    private CanvasGroup canvasGroup;
    enum Texts
    {
        Inventory_Txt,
    }

    public override bool Init()
    {
        if(base.Init() == false) 
            return false;

        BindText(typeof(Texts));
        _type = _item.type;
        canvasGroup = GetComponent<CanvasGroup>();

        return true;

    }
    public void OnDrag(PointerEventData eventData)
    {
        
    }
    public void SetInfo(Define.TileType type)
    {
        _type = type;
        _item = Manager.Bag.GetItem(type);
    }

    public void ChangeData()
    {
        _type = Define.TileType.None;
        
    }
}
