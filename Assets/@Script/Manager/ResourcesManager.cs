using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourcesManager
{
    Dictionary<string, Object> _resources = new Dictionary<string, Object>();
    public T Load<T>(string name) where T : UnityEngine.Object
    {
        UnityEngine.Object obj = null;
        if(_resources.TryGetValue(name, out obj))
            return obj as T;

        obj = Resources.Load($"Prefabs/{name}");
        return obj as T;
    }

    public GameObject Instantiate(string name, Transform parent = null)
    {
        GameObject go = Load<GameObject>(name);
        GameObject obj = Object.Instantiate(go, parent);
        obj.name = name;

        return obj;
    }
    public GameObject Instantiate(string name, Vector3 pos, Quaternion rot, Transform parent = null)
    {
        GameObject obj = Instantiate(name, parent);
        obj.transform.localPosition = pos;
        obj.transform.rotation = rot;

        return obj;
    }
  
}
