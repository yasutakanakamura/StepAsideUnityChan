using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
	//carPrefabを入れる
	public GameObject carPrefab;
	//coinPrefabを入れる
	public GameObject coinPrefab;
	//cornPrefabを入れる
	public GameObject cornPrefab;
	//スタート地点
	private int startPos = -160;
	//ゴール地点
	private int goalPos = 120;
	//アイテムを出すx方向の範囲
	private float posRange = 3.4f;
	//今のフレームでアイテムをつくっていいかの判定
	private bool IsGenerate = true;
	private int i = -160;

	// Use this for initialization
	void Start () {
		/*
		//一定の距離ごとにアイテムを生成
		for (int i = startPos; i < goalPos; i += 15) {
			//どのアイテムお出すのかをランダムに設定
			int num = Random.Range (0,10);
			if (num <= 1) {
				//コーンをX軸方向に一直線に生成
				for (float j = -1; j <= 1; j += 0.4f) {
					GameObject cone = Instantiate (cornPrefab) as GameObject;
					cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, i);
				}
			}else {
					
				//レーンごとにアイテム生成
				for (int j = -1; j < 2; j++) {
					//アイテムの種類を決める
					int item = Random.Range (1,11);
					//アイテムを置くZ座標のオフセットをランダムに設定
					int offsetZ = Random.Range (-5,6);
					//60%コイン配置：30%車配置：10%何もなし
						if (1 <= item && item <= 6) {
							//コインを生成
							GameObject coin = Instantiate (coinPrefab) as GameObject;
							coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y, i + offsetZ);

						} else if (7 <= item && item <=9) {
							//車を生成
							GameObject car = Instantiate (carPrefab) as GameObject;
							car.transform.position = new Vector3 (posRange * j, car.transform.position.y, i + offsetZ);
						}
				}
			}
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
		
		//現在のUnitychanのZ座標を取得
		float UnitychanZ = GameObject.Find("unitychan").transform.position.z;
		//UnityChanから＋50mの位置
		float ItemGeneratePos = UnitychanZ + 50f;
		Debug.Log(ItemGeneratePos);
		//次のアイテムをつくる距離にUnitychanが入ったかの判定
		if (i < ItemGeneratePos){
			IsGenerate = true;
		}
		//動的にアイテムを生成（UnityChanからZ軸に＋50以内の距離に入った時）
		if (ItemGeneratePos >= startPos && ItemGeneratePos <= goalPos) {
			if (IsGenerate == true){
				Debug.Log(ItemGeneratePos);
				if (ItemGeneratePos >= i) {
						Debug.Log(i);
						//どのアイテムお出すのかをランダムに設定
						int num = Random.Range (0,10);
						if (num <= 1) {
							//コーンをX軸方向に一直線に生成
							for (float j = -1; j <= 1; j += 0.4f) {
								GameObject cone = Instantiate (cornPrefab) as GameObject;
								cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, i);
							}
						}else {
								
							//レーンごとにアイテム生成
							for (int j = -1; j < 2; j++) {
								//アイテムの種類を決める
								int item = Random.Range (1,11);
								//アイテムを置くZ座標のオフセットをランダムに設定
								int offsetZ = Random.Range (-5,6);
								//60%コイン配置：30%車配置：10%何もなし
									if (1 <= item && item <= 6) {
										//コインを生成
										GameObject coin = Instantiate (coinPrefab) as GameObject;
										coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y, i + offsetZ);

									} else if (7 <= item && item <=9) {
										//車を生成
										GameObject car = Instantiate (carPrefab) as GameObject;
										car.transform.position = new Vector3 (posRange * j, car.transform.position.y, i + offsetZ);
									}
							}
						}
						//iに15を足して、次のアイテム生成Z座標とする
						i += 15;
						Debug.Log(i);
						//毎フレーム実行しないように判定をfalseにする
						IsGenerate = false;
				}
				
			}
		}
	}
	
}
