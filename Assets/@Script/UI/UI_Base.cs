using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> typeDic = new Dictionary<Type, UnityEngine.Object[]>();

    protected bool _init = false;

    public virtual bool Init()
    {
        if (_init)
            return false;

        Manager.Init();

        _init = true;
        return true;
    }

    private void Start()
    {
        Init();
    }
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length];
        typeDic.Add(typeof(T), objs);

        for(int i = 0; i < names.Length; i++)
            objs[i] = gameObject.FindChild<T>(names[i]);
            
    }

    protected void 
        BindObject(Type type) { Bind<GameObject>(type); }
    protected void BindImage(Type type) { Bind<Image>(type); }
    protected void BindTextPro(Type type) { Bind<TextMeshProUGUI>(type); }
    protected void BindText(Type type) { Bind<Text>(type); }
    protected void BindButton(Type type) { Bind<Button>(type); }
    protected void BindSlider(Type type) { Bind<Slider>(type); }
    protected void BindInput(Type type) { Bind<InputField>(type); }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (typeDic.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected TextMeshProUGUI GetTextPro(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected Slider GetSlider(int idx) { return Get<Slider>(idx); }
    protected InputField GetInput(int idx) { return Get<InputField>(idx); }

    public static void BindEvent(GameObject go, Action action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Utils.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;

            case Define.UIEvent.Press:
                evt.OnPressHandler -= action;
                evt.OnPressHandler += action;
                break;
        }
    }
}
