using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : ViewBehavior {

    public event Action beforeShowView;
    private void Start()
    {
        if (beforeShowView != null)
        {
            beforeShowView();
        }
        OnStart();
    }
    
    public virtual void OnStart()
    {

    }



}
