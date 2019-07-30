using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStats : MonoBehaviour {

	public AudioManager audioManager;

	public string COINS_COUNT = "coinsCount";
	public string DIAMONDS_COUNT = "diamondsCount";
	public string HEARTS_COUNT = "heartsCount";
	
	public Image statsMenu;

	public Text coinScore;
	public Text diamondScore;

	public Text coinText;
	public Text diamondText;
	public Text heartText;

	
	[SerializeField]
	private Text statsTime;
	
	private int coinsCount;
	private int diamondsCount;
	private int heartsCount;

	private GameManager gameManager;
	private SaveData saveData;
	private PowerUpManager powerUpManager;
	private ChangeBonusValues changeBonus;
	private GameObject[] hearts = new GameObject[5];


	void Awake(){
		
		gameManager = gameObject.GetComponent<GameManager> ();
		saveData = gameObject.GetComponent<SaveData> ();
		changeBonus = GameObject.Find ("Canvas").GetComponent<ChangeBonusValues> ();

		//put hearts in array hearts[]
		for (int i = 0; i < 5; i++) {
			int q = i+1;
			hearts[i] = GameObject.Find("Hearts "+q);
		}

//		if (PlayerPrefs.HasKey (COINS_COUNT) && PlayerPrefs.HasKey (DIAMONDS_COUNT)) {
//			CoinsCount = PlayerPrefs.GetInt (COINS_COUNT);
//			diamondsCount = PlayerPrefs.GetInt (DIAMONDS_COUNT);
//		}else coinsCount = diamondsCount = 0;

		saveData.Load ();

		coinScore.text = CoinsCount.ToString ();
		diamondScore.text = DiamondsCount.ToString ();

		heartsCount = 3;
		statsMenu.gameObject.SetActive (false);
	}
	
	public int CoinsCount{
		get {return coinsCount; }
		set { coinsCount = value;}
	}

	public int DiamondsCount{
		get {return diamondsCount; }
		set { diamondsCount = value;}
	}

	public int HeartsCount{
		get {return heartsCount; }
		set { heartsCount = value;}
	}

	public void DecreaseHeartsCounts(){
		heartsCount--;
		if (heartsCount >= 0) {
			ChangeAlpha (heartsCount, 0);
			
			if (heartsCount == 0){
				audioManager.AudioPlayer("loose");
				gameManager.EndGame();
			}
		}
	}

	public void ResetTextValues(){
		coinText.text = coinsCount.ToString ();
		diamondText.text = diamondsCount.ToString ();
		heartText.text = heartsCount.ToString ();
	}

	public void DisplayStats(bool hasNewBest, float timeElapsed){
		var textColor = hasNewBest ? "#FF0" : "#FFF";
		statsTime.text = "<color=" + textColor + ">TIME: " + gameManager.FormatTime (timeElapsed) + "</color>";// + "\n<color="+textColor+">BEST: " + gameManager.FormatTime (gameManager.bestTime)+"</color>";
		statsMenu.gameObject.SetActive (true);

		ResetTextValues ();

		changeBonus.ChangeButtonView ();
	}

	private void GetPowerUpManager(){
		powerUpManager = GameObject.FindWithTag ("Player").GetComponent<PowerUpManager> ();
	}

	public void IncrementBonusCount(string tag){
		GetPowerUpManager ();

		switch (tag) {
			
		case "coin":
			coinsCount++;
			coinScore.text = coinsCount.ToString();
			audioManager.AudioPlayer("coin");
			break;
		case "heart":
			heartsCount++;
			if( heartsCount >= 5 )
				heartsCount = 5;
			
			ChangeAlpha(heartsCount, 255);
			audioManager.AudioPlayer("heart");
			break;
		case "diamond":
			diamondsCount++;
			diamondScore.text = diamondsCount.ToString();
			audioManager.AudioPlayer("diamond");
			break;
		case "invis":
			if(!powerUpManager.isInvisible){
				audioManager.AudioPlayer("invis");
				StartCoroutine(powerUpManager.ChangeVisibility());
			}
			break;
		case "growUp":
			if(!powerUpManager.isGrowing){
				audioManager.AudioPlayer("growUp");
				StartCoroutine(powerUpManager.Grow());
			}
			break;
		case "timeWatch":
			if(!powerUpManager.isSlowed){
				audioManager.AudioPlayer("timeWatch");
				StartCoroutine(powerUpManager.ChangeTime(0.5f));
			}
			break;
		case "poison":
			if(!powerUpManager.isPoisoned){
				audioManager.AudioPlayer("poison");
				StartCoroutine(powerUpManager.Poisoned());
			}
			break;
		}
		
	}
	
	public void ChangeAlpha(int index, int alpha){
		bool increase;

		if (index >= 5)
			index = 5;

		if (alpha == 255)
			increase = true;
		else
			increase = false;


		if (increase) {

			index--;
			while (index >=0) {
				Color temp = hearts [index].GetComponent<SpriteRenderer> ().color;
				temp.a = alpha;
				hearts [index].GetComponent<SpriteRenderer> ().color = temp;
				index--;
			}

		}else {
			while (index < 5 ) {
				Color temp = hearts [index].GetComponent<SpriteRenderer> ().color;
				temp.a = alpha;
				hearts [index].GetComponent<SpriteRenderer> ().color = temp;
				index++;
			}
		}
	}
	

}