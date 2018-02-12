using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //每秒以z轴为中心旋转45度
        transform.Rotate(new Vector3(0,0,45) * Time.deltaTime);
	}
}
