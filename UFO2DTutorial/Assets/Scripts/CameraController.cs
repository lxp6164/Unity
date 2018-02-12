using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作用：摄影机跟随玩家

public class CameraController : MonoBehaviour {

    [Header("玩家物件")]
    public GameObject player;

    [Header("相对位移")]
    public Vector3 offset; //宣告成public可以即时修改数值
    
	// Use this for initialization
	void Start () {
        //相对位移 = 摄影机坐标 - 玩家坐标；
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //摄影机的坐标 = 玩家坐标 + 相对位移
        transform.position = player.transform.position + offset;
	}
}
