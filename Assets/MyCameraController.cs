using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour {
	//Unitychanのオブジェクト
	private GameObject unitychan;
	//Unitychanとカメラの距離
	private float difference;

	// Use this for initialization
	void Start () {
		//Unitychanのオブジェクトを取得
		this.unitychan = GameObject.Find ("unitychan");
		//Unitchanとカメラの位置（ｚ座標）の差を求める
		this.difference = unitychan.transform.position.z - this.transform.position.z;
		
	}
	
	// Update is called once per frame
	void Update () {
		//unitychanの位置に合わせてカメラの位置を移動
		this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z-difference);
		
	}
}
