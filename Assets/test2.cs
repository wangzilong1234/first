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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
