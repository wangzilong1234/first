using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnrealM;

public class test2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Log.Info(Time.realtimeSinceStartup.ToString());
        this.Delayer(5,() => {
            Log.Info("{0}:{1}_____","你好","CODE");
            Log.Info(Time.realtimeSinceStartup.ToString());
        });
<<<<<<< HEAD
<<<<<<< HEAD

        MessengerCenter.EventCenter.Broadcast(1,1111,2222,3333);
        ActionSequenceSystem.Delayer(10,() => {
            Log.Info("10 later");
        });
    }

    private void OnEnable() {
       
    }

    private void OnDestroy() {
        MessengerCenter.EventCenter.RemoveListener(1,(data) => {
            foreach (var item in data) {
                Log.Info(item.ToString());
            }
        });
        Log.Info("remove");
=======
>>>>>>> parent of 66d6ec3... Update test2.cs
=======
>>>>>>> parent of 66d6ec3... Update test2.cs
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
