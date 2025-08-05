using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BackgroundDrag : UI_Base, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Define.TileType _type;
    private BagItem _item;
    private CanvasGroup canvasGroup;

    private Transform curParent;
    enum Texts
    {
        Inventory_Txt,
        ItemNum_Txt,
    }

    public override bool Init()
    {
        if(base.Init() == false) 
            return false;

        
        _type = _item.type;
        canvasGroup = GetComponent<CanvasGroup>();

        GetText((int)Texts.Inventory_Txt).text = _item.itemName;
        GetText((int)Texts.ItemNum_Txt).text = _item.count.ToString();

        return true;

    }
    
    public void Refresh(Define.TileType type)
    {
        _item = Manager.Bag.GetItem(type);

        Debug.Log(GetText((int)Texts.Inventory_Txt));
        GetText((int)Texts.Inventory_Txt).text = _item.itemName;
        GetText((int)Texts.ItemNum_Txt).text = _item.count.ToString();
    }
    public void SetInfo(Define.TileType type)
    {
        _type = type;
        _item = Manager.Bag.GetItem(type);
        BindText(typeof(Texts));
        GetComponent<Image>().color = Color.gray;
    }

    public void ChangeData()
    {
        _type = Define.TileType.None;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if(curParent == transform.parent)
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        curParent = transform.parent;
    }
}
