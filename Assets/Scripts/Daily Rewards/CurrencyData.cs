using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrencyData
{
    private static int _coins = 0;
    private static int _gems = 0;

    //Static Constructor to load data from playerPrefs
    static CurrencyData()
    {
        _coins = PlayerPrefs.GetInt("Coins", 0);
        _gems = PlayerPrefs.GetInt("Gems", 0);
    }
    public static int Coins
    {
        get { return _coins; }
        set { PlayerPrefs.SetInt("Coins", (_coins = value)); }
    }

    public static int Gems
    {
        get { return _gems; }
        set { PlayerPrefs.SetInt("Gems", (_gems = value)); }
    }
}
