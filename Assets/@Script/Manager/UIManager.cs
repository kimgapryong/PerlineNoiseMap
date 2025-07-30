using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 20;
    Stack<UI_Pop> _popStack = new Stack<UI_Pop>();

    public UI_Scene SceneUI { get; set; }   

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };

            return root;
        }
    }
    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public void MakeSubItem<T>(Transform parent = null, string key = null, Action<T> callback = null) where T : UI_Base
    {
        if(string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        Manager.Resources.Instantiate($"UI/Fragment/{key}", parent, (go) =>
        {
            T subItem = go.GetOrAddComponent<T>();
            callback?.Invoke(subItem);
        });
    }

    public void ShowSceneUI<T>(string key = null, Action<T> callback = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        Manager.Resources.Instantiate($"UI/Scene/{key}", Root.transform, (go) =>
        {
            T sceneUI = go.GetOrAddComponent<T>();
            SceneUI = sceneUI; 
            callback?.Invoke(sceneUI);
        });
    }
    public void ShowPopUI<T>(string key = null, Transform parent = null, Action<T> callback = null) where T : UI_Pop
    {
        if (string.IsNullOrEmpty(key))
            key = typeof(T).Name;

        Manager.Resources.Instantiate($"UI/Popup/{key}", null, (go) =>
        {
            T pop = go.GetOrAddComponent<T>();
            _popStack.Push(pop);

            if (parent != null)
                go.transform.SetParent(parent);
            else
                go.transform.SetParent(Root.transform);

            callback?.Invoke(pop);
        });
    }

   
    public void ClosePopUI(UI_Pop pop)
    {
        Debug.Log("������ ����");
        if (_popStack.Count == 0)
            return;

        Debug.Log(_popStack.Peek().name); 
        
        if (_popStack.Peek() != pop)
            return;

        Debug.Log("������ ����");
        ClosePopUI();
    }

    public void ClosePopUI()
    {
        Debug.Log("����");
        if (_popStack.Count == 0)
            return;

        Debug.Log("���2��");
        UI_Pop pop = _popStack.Pop();
        UnityEngine.Object.Destroy(pop.gameObject);
        pop = null;
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popStack.Count > 0)
            ClosePopUI();
    }
    public void Clear()
    {
        CloseAllPopupUI();
        SceneUI = null;
    }
}
