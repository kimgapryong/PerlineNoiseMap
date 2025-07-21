using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    private bool _check = false;

    public virtual bool Init()
    {
        if (_check)
            return false;


        return true;
    }
    private void Start()
    {
        Init();
    }


}
