using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {
	//アニメーションするためのコンポーネントを入れる
	private Animator myAnimator;
	//Unityちゃんを移動させるコンポーネントを入れる
	private Rigidbody myRigidbody;
	//前進するための力
	private float forwardForce = 800.0f;
	//左右に移動するための力
	private float turnForce = 500.0f;
	//ジャンプするための力
	private float upForce = 500.0f;
	//左右の移動できる範囲
	private float movableRange = 3.4f;
	//動きを減速させる係数
	private float coefficient = 0.95f;
	//ゲーム終了の判定
	private bool isEnd = false;
	//ゲーム終了時に表示するテキスト
	private GameObject stateText;
	//スコアを表示するテキスト
	private GameObject scoreText;
	//得点
	private int score = 0;
	//左ボタンの判定
	private bool isLButtonDown = false;
	//右ボタンお判定
	private bool isRButtonDown = false;

	// Use this for initialization
	void Start () {

		//Animatorコンポーネントを取得
		this.myAnimator = GetComponent<Animator>();

		//走るアニメーションを開始
		this.myAnimator.SetFloat ("Speed" ,1.0f);

		//Rigidbody コンポーネントを収録
		this.myRigidbody = GetComponent<Rigidbody>();

		//シーン中のstateTextオブジェクトを取得
		this.stateText = GameObject.Find("GameResultText");

		//シーン中のscoreTextオブジェクトを取得
		this.scoreText = GameObject.Find("ScoreText");
		
	}
	
	// Update is called once per frame
	void Update () {

		//ゲーム終了ならUnitychanの動きを減衰する
		if (this.isEnd) {
			this.forwardForce *= this.coefficient;
			this.turnForce *= this.coefficient;
			this.upForce *= this.coefficient;
			this.myAnimator.speed *= this.coefficient;
		}

		//Unitychanに前方向の力を加える
		this.myRigidbody.AddForce (this.transform.forward * this.forwardForce);

		//Unitychanを矢印キーまたはボタンに応じて左右に移動させる
		if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x) {
			//左に移動
			this.myRigidbody.AddForce (-this.turnForce, 0, 0);
		} else if ((Input.GetKey (KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange) {
			//右に移動
			this.myRigidbody.AddForce (this.turnForce, 0, 0);
		}

		//Jumpステートの場合はJumpにfalseをセットする
		if (this.myAnimator.GetCurrentAnimatorStateInfo (0).IsName ("Jump")) {
			this.myAnimator.SetBool ("Jump", false);
		}
		
		//ジャンプしていない時にスペースが押されたらジャンプする
		if (Input.GetKeyDown (KeyCode.Space) && this.transform.position.y < 0.5f) {
			//ジャンプアニメを再生
			this.myAnimator.SetBool ("Jump", true);
			//Unitychanに上方向の力を加える
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
		
		//現在のUnitychanのZ座標を取得
		float UnitychanZ = GameObject.Find("unitychan").transform.position.z;

		//CarTagゲームオブジェクトを取得（List）
		GameObject[] Caritems = GameObject.FindGameObjectsWithTag ("CarTag");
			//
			for (int i = 0; i < Caritems.Length; i++) {
				//該当ゲームオブジェクトのZ座標取得
				Vector3 tmp = (Caritems[i]).transform.position; 
				float itemPosZ = tmp.z;
				//現在のフレームおけるUnitychanより2m後方にある該当オブジェクトをDestroyする
				if ((UnitychanZ - itemPosZ) > 2) {
					Destroy(Caritems[i]); 
				}
			}
		//CoinTagゲームオブジェクトを取得
		GameObject[] Coinitems = GameObject.FindGameObjectsWithTag ("CoinTag");
			//削除
			for (int i = 0; i < Coinitems.Length; i++) {
				//該当ゲームオブジェクトのZ座標取得
				Vector3 tmp = (Coinitems[i]).transform.position;
				float itemPosZ = tmp.z;
				//Unity
				if ((UnitychanZ - itemPosZ) > 2) {
					Destroy(Coinitems[i]);
				}
			}
		//TrafficTagゲームオブジェクトを取得
		GameObject[] TrafficConeitems = GameObject.FindGameObjectsWithTag ("TrafficConeTag");
			//削除
			for (int i = 0; i < TrafficConeitems.Length; i++) {
				//該当ゲームオブジェクトのZ座標取得
				Vector3 tmp = (TrafficConeitems [i]).transform.position;
				float itemPosZ = tmp.z;
				//Unity
				if ((UnitychanZ - itemPosZ) > 2) {
					Destroy(TrafficConeitems[i]);
				}
			}
	}

	//トリガーモードで他のオブジェクトと接触した場合の処理
	void OnTriggerEnter(Collider other) {

		//障害物に衝突した場合
		if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
			this.isEnd = true;
			//stateTextにGAME OVERを表示
			this.stateText.GetComponent<Text>().text = "GAME OVER!!";
		}

		//ゴール地点に到達した場合
		if (other.gameObject.tag == "GoalTag") {
			this.isEnd = true;
			//stateTextにGAME CLEARを表示
			this.stateText.GetComponent<Text>().text = "CLEAR!!";
		}
		//コインに衝突した場合
		if (other.gameObject.tag == "CoinTag") {

			//スコアを火山
			this.score += 10;

			//ScoreText獲得した点数を表示
			this.scoreText.GetComponent<Text> ().text = "Score " + this.score + "pt";

			//パーティクルを再生
			GetComponent<ParticleSystem> ().Play ();

			//接触したコインのオブジェクトを破棄
			Destroy (other.gameObject);
		}
	}

	//ジャンプボタンを押した場合の処理
	public void GetMyJumpButtonDown() {
		if (this.transform.position.y < 0.5f) {
			this.myAnimator.SetBool ("Jump", true);
			this.myRigidbody.AddForce (this.transform.up * this.upForce);
		}
	}

	//左ボタンを押し続けた場合の処理
	public void GetMyLeftButtonDown() {
		this.isLButtonDown = true;
	}

	//左ボタンを話した場合の処理
	public void GetMyLeftButtonUp() {
		this.isLButtonDown = false;
	}

	//右ボタンを押し続けた場合の処理
	public void GetMyRightButtonDown() {
		this.isRButtonDown = true;
	}

	//右ボタンを話した場合の処理
	public void GetMyRightButtonUp() {
		this.isRButtonDown = false;
	}
}
