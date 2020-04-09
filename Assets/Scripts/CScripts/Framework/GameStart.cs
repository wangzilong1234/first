using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {
	void Start () {
        Log.Info("GameStart");
        DontDestroyOnLoad(this.gameObject);
        //var view = UIManager.Instance.Open<UILogin>();
    }
	
	
}
