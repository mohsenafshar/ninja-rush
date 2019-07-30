using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public OptionMenu optionMenu;
	public AudioManager audioManager;
	public GameObject playerPrefab;
	public Toast toast;
	public Text continueText;
	public Text scoreText;

	public int level;
	public float timeElapsed = 0f;
	public float bestTime = 0f;
	private float blinkTime;

	public bool isStarted;

	private bool hasNewBest;
	private bool blink;
	private bool gameStarted ;

	private PauseGame pauseGame;
	private SaveData saveData;
	private InputState inputState;
	private GameStats gameStats;
	private TimeManager timeManager;
 	private GameObject player;
	private Spawner spawner;
	private GameObject floor;

	#region IPointerDownHandler implementation
	public void OnPointerDown(PointerEventData eventData){
		if (GameObject.FindWithTag ("Player") != null) {
			inputState = GameObject.FindWithTag ("Player").GetComponent<InputState> ();
			inputState.activeButton = true;
		}

		OnClickResetGame ();
	}
	#endregion
	
	#region IPointerUpHandler implementation
	public void OnPointerUp(PointerEventData eventData){
		if (GameObject.FindWithTag ("Player") == null)
			return;
		
		inputState = GameObject.FindWithTag("Player").GetComponent<InputState>();
		inputState.activeButton = false;
	}
	#endregion

	public void OnClickResetGame ()
	{
		if (!gameStarted && Time.timeScale == 0 && gameStats.HeartsCount > 0) {
			isStarted = true;
			timeManager.ManipulateTime (1f, 1f);
			ResetGame ();
		}
	}
	

	void Awake(){
		
		pauseGame = GameObject.FindWithTag ("Canvas").GetComponent<PauseGame> ();
		saveData = GetComponent<SaveData> ();
		gameStats = gameObject.GetComponent<GameStats> ();
		floor = GameObject.FindGameObjectWithTag ("foreground");
		spawner = GameObject.Find ("spawner").GetComponent<Spawner>();
		timeManager = GetComponent<TimeManager>();

	}

	void Start () {

		isStarted = false;

		var pos = new Vector3 (0, -(Screen.height / perfectPixelsCamera.pixelsToUnits) / 2 + floor.transform.localScale.y / 2, 0);
		floor.transform.position = pos;

		spawner.active = false;

		Time.timeScale = 0;

		continueText.text = "TAP ANYWHERE TO START";

		bestTime = PlayerPrefs.GetFloat ("BestTime");

		gameObjectUtill.theTime = 0;
		gameObjectUtill.level = level;

		optionMenu.PreLoadVolumes ();
	}

	void Update () {
		if ((int)timeElapsed == 1) {
			ChangeGravity();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			pauseGame.GetTime ();
		}

		if (!gameStarted) {
			continueText.enabled = true;
			blinkTime++;
			if (blinkTime % 60 == 0) {
				blink = !blink;
			}

			continueText.canvasRenderer.SetAlpha (blink ? 0 : 1);

//			var textColor = hasNewBest ? "#FF0" : "#FFF";

			scoreText.text = "TIME: " + FormatTime (timeElapsed);// + "\n<color="+textColor+">BEST: " + FormatTime (bestTime)+"</color>";
		} else {
			timeElapsed += Time.deltaTime;
			scoreText.text = "TIME: " + FormatTime (timeElapsed);
		}

		switch ( ((int)timeElapsed).ToString()){
			case "20":
				spawner.range = new Vector2(2, 3);
				gameObjectUtill.theTime = 20;
				break;
			case "40":
				gameObjectUtill.theTime = 40;
				break;
			case "60":
				gameObjectUtill.theTime = 60;
				break;
			case "80":
				gameObjectUtill.theTime = 80;
				break;
			case "100":
				gameObjectUtill.theTime = 100;
				break;
		case "120":
				spawner.range = new Vector2(1.5f, 2.5f);
				gameObjectUtill.theTime = 120;
			break;
		case "150":
				spawner.range = new Vector2(1.3f, 2);
				gameObjectUtill.theTime = 150;
				break;
			default :
				break;
		}

	}
	
	void ChangeGravity(){
		player.GetComponent<Rigidbody2D> ().gravityScale = 60;
	}

	public void OnPlayerKilled(){

		gameStats.DecreaseHeartsCounts ();
		spawner.active = false;

		var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
		playerDestroyScript.DestroyCallback -= OnPlayerKilled;

		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

		if (gameStats.HeartsCount == 0) {
			gameStats.DisplayStats (hasNewBest, timeElapsed);
			timeManager.ManipulateTime (0f, 1f);
			return;
		}

		timeManager.ManipulateTime (0f, 5.5f);

		continueText.text = "TAP ANYWHERE TO RESTART";
		gameStarted = false;
	}

	public void ResetGame(){
//		gameStats.coinScore.text = gameStats.CoinsCount.ToString ();
		spawner.active = true;

		player = gameObjectUtill.Instantiate(playerPrefab, new Vector3(0, ((Screen.height/perfectPixelsCamera.pixelsToUnits)/2) + 100, 0));
	
		var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
		playerDestroyScript.DestroyCallback += OnPlayerKilled;

		gameStarted = true;
		continueText.canvasRenderer.SetAlpha(0);
		continueText.enabled = false;

		hasNewBest = false;
	}

	public void EndGame(){
		continueText.gameObject.SetActive (false);

		//Set PlayerPrefs
		if (timeElapsed > bestTime) {
			hasNewBest = true;
			bestTime = timeElapsed;
			PlayerPrefs.SetFloat("BestTime", bestTime);
		}

		saveData.Save("somthing");
			
		gameStats.DisplayStats (hasNewBest, timeElapsed);

	}

	public void OnClicKResumeGame(GameObject go){

		if (gameStats.HeartsCount < 1) {
			toast.ToastMessage ();
			return;
		}

		//reset the gameplay
		Time.timeScale = 1;

		gameStats.coinScore.text = gameStats.CoinsCount.ToString();
		gameStats.diamondScore.text = gameStats.DiamondsCount.ToString();

		//SaveBonusData ();
		saveData.Save("");

		continueText.gameObject.SetActive (true);
		continueText.text = "TAP ANYWHERE TO START";
		go.SetActive (false);
		gameStats.ChangeAlpha (gameStats.HeartsCount, 255);

		timeManager.ManipulateTime(1f,1f);
		ResetGame ();

	}

	public void OnClicKPlayAgain(GameObject go){

		if (gameStats.HeartsCount > 0) {

			FlushMemeory ();

			gameStats.HeartsCount = gameStats.HeartsCount + 3;
			if (gameStats.HeartsCount >= 5)
				gameStats.HeartsCount = 5;
			
			gameStats.coinScore.text = gameStats.CoinsCount.ToString ();
			gameStats.diamondScore.text = gameStats.DiamondsCount.ToString ();
			gameStats.ChangeAlpha (gameStats.HeartsCount, 255);
			saveData.Save ("");
			go.SetActive (false);

			continueText.gameObject.SetActive (true);
			continueText.text = "TAP ANYWHERE TO START";
			timeElapsed = 0;
			gameObjectUtill.theTime = 0;

			gameStarted = false;
			Time.timeScale = 0;

		} else {
			OnClicKRestart ();
		}
	}

	public void OnClicKRestart(){

		FlushMemeory ();
		
		gameStats.HeartsCount = 3;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		
	}
	
	//reset the gameplay
	public void FlushMemeory(){
		Time.timeScale = 1;
		DestroyGameObjects ();
		gameObjectUtill.pools.Clear ();
	}

	void DestroyGameObjects(){

		GameObject[] objects = spawner.prefabs;
		foreach (GameObject go in objects) {
			string name = go.name + "ObjectPool";
			GameObject destroyable = GameObject.Find(name);
			DestroyImmediate(destroyable);
		}

		DestroyObject (GameObject.Find (playerPrefab.name+ "ObjectPool"));
		DestroyObject (GameObject.Find ("heart puffyObjectPool"));
		DestroyObject (GameObject.Find ("diamond_blueObjectPool"));
		DestroyObject (GameObject.Find ("spinning coin long timeObjectPool"));
	}

	public void SaveBonusData (){
		PlayerPrefs.SetInt (gameStats.COINS_COUNT, gameStats.CoinsCount);
		PlayerPrefs.SetInt (gameStats.DIAMONDS_COUNT, gameStats.DiamondsCount);
	}

	public string FormatTime(float value){
		TimeSpan t = TimeSpan.FromSeconds (value);
		return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
	}

}
