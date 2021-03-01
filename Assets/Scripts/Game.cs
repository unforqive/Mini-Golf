using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	#region Singleton:Game

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			CurrencyData.Coins += 1000;
			UpdateAllCoinsUIText();
		}
	}

	public static Game Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	#endregion

	[SerializeField] TMP_Text[] allCoinsUIText;
	[SerializeField] TMP_Text[] allGemsUIText;

	void Start()
	{
		UpdateAllCoinsUIText();
		UpdateAllGemsUIText();
	}

	public void UseCoins(int amount)
	{
		CurrencyData.Coins -= amount;
	}

	public bool HasEnoughCoins(int amount)
	{
		return (CurrencyData.Coins >= amount);
	}

	public void UseGems(int amount)
	{
		CurrencyData.Gems -= amount;
	}

	public bool HasEnoughGems(int amount)
	{
		return (CurrencyData.Gems >= amount);
	}

	//Handles all coins and gem text updates !!!!!IMPORTANT!!!!!\\

	public void UpdateAllCoinsUIText()
	{
		for (int i = 0; i < allCoinsUIText.Length; i++)
		{
			allCoinsUIText[i].text = CurrencyData.Coins.ToString("#,##0");
		}
	}

	public void UpdateAllGemsUIText()
	{
		for (int i = 0; i < allGemsUIText.Length; i++)
		{
			allGemsUIText[i].text = CurrencyData.Gems.ToString("#,##0");
		}
	}
}
