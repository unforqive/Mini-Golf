using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;

public class ConfirmPurchase : MonoBehaviour
{
    [Header("Confirm UI")]
    [SerializeField] GameObject ConfirmHolder;
    [SerializeField] GameObject CoinConfirm;
    [SerializeField] GameObject GemConfirm;
    [SerializeField] GameObject Background;

    [Space]
    [Header("Animation")]
    public Animator ConfirmAnimation;

    [Space]
    [Header("Shop Script")]
    [SerializeField] Shop shop;

    void Start()
    {
        Background.SetActive(false);
        ConfirmHolder.SetActive(false);
        CoinConfirm.SetActive(false);
        GemConfirm.SetActive(false);
    }

    public void OnCoinSelection()
    {
        ConfirmHolder.SetActive(true);

        Background.SetActive(true);

        ConfirmAnimation.SetBool("Out", true);
        ConfirmAnimation.SetBool("In", false);

        CoinConfirm.SetActive(true);
        GemConfirm.SetActive(false);
    }

    public void OnGemSelection()
    {
        ConfirmHolder.SetActive(true);

        Background.SetActive(true);

        ConfirmAnimation.SetBool("Out", true);
        ConfirmAnimation.SetBool("In", false);

        CoinConfirm.SetActive(false);
        GemConfirm.SetActive(true);
    }

    public void OnCancel()
    {
        Background.SetActive(false);

        shop.InvalidCoins.SetActive(false);
        shop.InvalidGems.SetActive(false);
        shop.PurchaseSuccessful.SetActive(false);
        shop.AlreadyBought.SetActive(false);

        ConfirmAnimation.SetBool("Out", false);
        ConfirmAnimation.SetBool("In", true);
    }
}
