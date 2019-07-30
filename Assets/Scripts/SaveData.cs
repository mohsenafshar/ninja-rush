using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveData : MonoBehaviour {

	private GameStats gameStats;

	private const string CoinsCount = "coinsCount";
	private const string DiamondsCount = "diamondsCount";

	void Awake(){
		gameStats = gameObject.GetComponent<GameStats> ();
	}

	public void Save(){
		PlayerPrefs.SetInt (CoinsCount, gameStats.CoinsCount);
		PlayerPrefs.SetInt (DiamondsCount, gameStats.DiamondsCount);
	}

//	public void Load(){
//		gameStats.CoinsCount =  PlayerPrefs.GetInt (CoinsCount);
//		gameStats.DiamondsCount =  PlayerPrefs.GetInt (DiamondsCount);
//	}

	public void Save(string name){
		BinaryFormatter bf = new BinaryFormatter ();
//		if(File.Exists(Application.persistentDataPath + "/PlayerInfo.dat"))
		FileStream file = File.Create (Application.persistentDataPath + "/PlayerInfo.dat");

		PlayerData data = new PlayerData ();
		data.Coins = gameStats.CoinsCount;
		data.diamonds = gameStats.DiamondsCount;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/PlayerInfo.dat")) {

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/PlayerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close ();

			if (gameStats == null) {
				gameStats = gameObject.GetComponent<GameStats> ();
				print ("gameStats not initilized");
			}
			if (gameStats == null) {
				print ("gameStats still null");
				return;
			}
			gameStats.CoinsCount = data.Coins;
			gameStats.DiamondsCount = data.diamonds;
		}
	}

}

[Serializable]
class PlayerData{
	public int Coins;
	public int diamonds;
}
