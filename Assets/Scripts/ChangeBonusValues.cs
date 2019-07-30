using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeBonusValues : MonoBehaviour {

	public int coinToHeart = 1000;
	public int diamondToCoin = 100;

	[SerializeField]
	private int maxHeartValue = 5;
	public int MaxHeartValue {get;set;}

	public Button arrow_enable;
	public Button arrow_disable;
	public Button plus_enable;
	public Button plus_disable;

	private bool isArrowEnable;
	private bool isAddEnable;

	private GameStats gameStats;

	void Awake(){
		gameStats = GameObject.FindWithTag ("GameManager").GetComponent<GameStats> ();

		DefaultButtonsCondition ();
	}
	
	void Start(){
		ChangeButtonView ();
	}

	public void OnClickArrow(){

		if (isArrowEnable) {

			gameStats.DiamondsCount -= 1;
			gameStats.CoinsCount += diamondToCoin;

			gameStats.coinText.text = gameStats.CoinsCount.ToString();
			gameStats.diamondText.text = gameStats.DiamondsCount.ToString();

		}

		ChangeButtonView ();

	}

	public void OnClickPlus(){

		if (isAddEnable) {
			
			gameStats.CoinsCount -= coinToHeart;
			gameStats.HeartsCount += 1;

			gameStats.coinText.text = gameStats.CoinsCount.ToString();
			gameStats.heartText.text = gameStats.HeartsCount.ToString();
		}

		gameStats.ChangeAlpha (gameStats.HeartsCount, 255);

		print (gameStats.HeartsCount.ToString());

		ChangeButtonView ();

		if (gameStats.HeartsCount >= 5) {
			plus_enable.gameObject.SetActive (false);
			plus_disable.gameObject.SetActive (true);
		}

	}

	public void ChangeButtonView(){
		if (gameStats.statsMenu.gameObject.activeSelf) {
			if(gameStats.DiamondsCount > 0){
				arrow_enable.gameObject.SetActive(true);
				arrow_disable.gameObject.SetActive(false);
				isArrowEnable = true;
			}
			else {
				arrow_enable.gameObject.SetActive(false);
				arrow_disable.gameObject.SetActive(true);
				isArrowEnable = false;
			}

			if(gameStats.CoinsCount >= coinToHeart && gameStats.HeartsCount < maxHeartValue){
				plus_enable.gameObject.SetActive(true);
				plus_disable.gameObject.SetActive(false);
				isAddEnable = true;
			}
			else {
				plus_enable.gameObject.SetActive(false);
				plus_disable.gameObject.SetActive(true);
				isAddEnable = false;
			}
		}
	}

	private void DefaultButtonsCondition(){

		arrow_enable.gameObject.SetActive(false);
		arrow_disable.gameObject.SetActive(true);
		isArrowEnable = false;
		
		plus_enable.gameObject.SetActive(false);
		plus_disable.gameObject.SetActive(true);
		isAddEnable = false;

	}


}
