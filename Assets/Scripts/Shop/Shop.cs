using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Singleton:Shop

    public static Shop Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [System.Serializable]
    public class ShopItem
    {
        public Sprite QualityImage;
        public Sprite QualityPlace;
        public Sprite ItemImage;
        public Sprite currency;
        public int Price;
        public bool IsPurchased = false;
    }

    public List<ShopItem> ShopItemsList;

    [Header("Shop UI")]
    [SerializeField] GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button BuyButton;

    [Space]
    [Header("Preview UI")]
    [SerializeField] GameObject PreviewItemQuality;
    [SerializeField] TMP_Text PreviewItemName;

    [SerializeField] TMP_Text PreviewItemType;
    [SerializeField] GameObject PreviewItem;

    [SerializeField] GameObject PreviewValueCoins;
    [SerializeField] GameObject PreviewValueGems;
    [SerializeField] TMP_Text PreviewValue;

    void Start()
    {
        int len = ShopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);

            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].QualityImage;
            g.transform.GetChild(1).GetComponent<Image>().sprite = ShopItemsList[i].QualityPlace;
            g.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].currency;
            g.transform.GetChild(2).GetComponent<Image>().sprite = ShopItemsList[i].ItemImage;
            g.transform.GetChild(3).GetComponent<TMPro.TMP_Text>().text = ShopItemsList[i].Price.ToString("#,##0");
            BuyButton = g.transform.GetComponent<Button>();

            if (ShopItemsList[i].IsPurchased)
            {
                DisableBuy();
            }
            BuyButton.AddEventListener(i, OnShopItemButtonClicked);
        }

        Destroy(ItemTemplate);
    }

    void OnShopItemButtonClicked(int itemIndex)
    {
        if(Game.Instance.HasEnoughCoins (ShopItemsList[itemIndex].Price))
        {
            Game.Instance.UseCoins(ShopItemsList[itemIndex].Price);

            //Purchase item
            ShopItemsList[itemIndex].IsPurchased = true;

            //Disable button
            BuyButton = ShopScrollView.GetChild(itemIndex).GetComponent<Button>();
            DisableBuy();

            //Change UI text: coins
            Game.Instance.UpdateAllCoinsUIText();
        }
        else
        {
            Debug.Log("You don't have enough coins");
        }

        if (Game.Instance.HasEnoughGems(ShopItemsList[itemIndex].Price))
        {
            Game.Instance.UseGems(ShopItemsList[itemIndex].Price);

            //Purchase item
            ShopItemsList[itemIndex].IsPurchased = true;

            //Disable button
            BuyButton = ShopScrollView.GetChild(itemIndex).GetComponent<Button>();
            DisableBuy();

            //Change UI text: gems
            Game.Instance.UpdateAllGemsUIText();
        }
        else
        {
            Debug.Log("You don't have enough gems");
        }
    }

    void DisableBuy()
    {
        BuyButton.interactable = false;
        BuyButton.transform.GetChild(4).gameObject.SetActive(true);
    }
}
