using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public interface IView
    {
        void OnStart();
        void OnClose();
        void OnBack();

    }
}


