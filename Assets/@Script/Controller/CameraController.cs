using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BaseController
{
    private Transform target;
    public void SetInfo(Transform target)
    {
        this.target = target;
    }
    private void Update()
    {
        transform.position = target.position;
    }
    
    
}
