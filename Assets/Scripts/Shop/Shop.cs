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
        public string ItemType;
        public string ItemName;
        public Sprite ItemImage;
        public Sprite currency;
        public Color itemColor;
        public int Price;
        public int GemValue;
        public bool IsPurchased = false;
    }

    public List<ShopItem> ShopItemsList;

    [Header("Shop UI")]
    [SerializeField] GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button PreviewButton;

    [Space]
    [Header("Preview UI")]
    [SerializeField] GameObject PreviewHolder;
    [SerializeField] GameObject PreviewItemQuality;
    [SerializeField] TMP_Text PreviewItemName;

    [SerializeField] TMP_Text PreviewItemType;
    [SerializeField] GameObject PreviewItem;

    [SerializeField] GameObject PreviewValueCoins;
    [SerializeField] GameObject PreviewValueGems;
    [SerializeField] TMP_Text PreviewValue;
    [SerializeField] TMP_Text PreviewGemsValue;
    [SerializeField] GameObject ChestContents;
    [SerializeField] GameObject ContentsHolder;

    [Space]
    [Header("Confirm UI")]
    [SerializeField] GameObject CoinConfirm;
    [SerializeField] GameObject GemConfirm;

    [SerializeField] TMP_Text ConfirmValue;
    [SerializeField] TMP_Text ConfirmGemValue;

    public GameObject InvalidCoins;
    public GameObject InvalidGems;
    public GameObject PurchaseSuccessful;
    public GameObject AlreadyBought;

    Button CoinConfirmButton;
    Button GemConfirmButton;

    private bool showGemValue;

    public ConfirmPurchase confirmPurchase;

    public int itemInt;

    void Start()
    {
        InvalidCoins.SetActive(false);
        InvalidGems.SetActive(false);
        PurchaseSuccessful.SetActive(false);
        AlreadyBought.SetActive(false);

        PreviewValueGems.SetActive(false);
        showGemValue = false;
        ChestContents.SetActive(false);
        ContentsHolder.SetActive(false);

        int len = ShopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);

            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].QualityImage;
            g.transform.GetChild(1).GetComponent<Image>().sprite = ShopItemsList[i].QualityPlace;
            g.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].currency;
            g.transform.GetChild(2).GetComponent<Image>().sprite = ShopItemsList[i].ItemImage;
            g.transform.GetChild(3).GetComponent<TMPro.TMP_Text>().text = ShopItemsList[i].Price.ToString("#,##0");
            g.transform.GetChild(6).GetComponent<Image>().color = ShopItemsList[i].itemColor;

            PreviewButton = g.transform.GetComponent<Button>();
            CoinConfirmButton = CoinConfirm.transform.GetComponent<Button>();
            GemConfirmButton = GemConfirm.transform.GetComponent<Button>();

            if (ShopItemsList[i].IsPurchased)
            {
                DisableBuy();
            }
            PreviewButton.AddEventListener(i, PreviewShopItem);
        }

        Destroy(ItemTemplate);
    }

    void PreviewShopItem(int itemIndex)
    {
        PreviewHolder.SetActive(true);

        confirmPurchase.ConfirmAnimation.SetBool("Out", false);
        confirmPurchase.ConfirmAnimation.SetBool("In", true);

        PreviewItemName.text = ShopItemsList[itemIndex].ItemName.ToString();
        PreviewItemType.text = ShopItemsList[itemIndex].ItemType.ToString();
        
        if (ShopItemsList[itemIndex].ItemName == "Ball Chest")
        {
            PreviewValueGems.SetActive(true);
            PreviewGemsValue.text = ShopItemsList[itemIndex].GemValue.ToString("#,##0");
            ConfirmGemValue.text = ShopItemsList[itemIndex].GemValue.ToString("#,##0");
            ChestContents.SetActive(true);
        }
        else
        {
            PreviewValueGems.SetActive(false);
            ChestContents.SetActive(false);
            ContentsHolder.SetActive(false);
        }

        PreviewItem.GetComponent<Image>().sprite = ShopItemsList[itemIndex].ItemImage;
        PreviewItemName.color = ShopItemsList[itemIndex].itemColor;

        PreviewItemQuality.GetComponent<Image>().sprite = ShopItemsList[itemIndex].QualityImage;
        PreviewValueCoins.transform.GetChild(0).GetComponent<TMP_Text>().text = ShopItemsList[itemIndex].Price.ToString("#,##0");
        ConfirmValue.text = ShopItemsList[itemIndex].Price.ToString("#,##0");

        itemInt = itemIndex;
    }

    public void ClosePreivew(int itemIndex)
    {
        PreviewHolder.SetActive(false);
    }

    public void OnCoinButtonClicked()
    {
        if (Game.Instance.HasEnoughCoins(ShopItemsList[itemInt].Price) && !ShopItemsList[itemInt].IsPurchased)
        {
            InvalidCoins.SetActive(false);
            InvalidGems.SetActive(false);
            AlreadyBought.SetActive(false);

            if (!ShopItemsList[itemInt].IsPurchased)
            {
                PurchaseSuccessful.SetActive(true);
            }

            Game.Instance.UseCoins(ShopItemsList[itemInt].Price);

            //Purchase item
            ShopItemsList[itemInt].IsPurchased = true;

            //Disable button
            PreviewButton = ShopScrollView.GetChild(itemInt).GetComponent<Button>();
            DisableBuy();

            //Change UI text: coins
            Game.Instance.UpdateAllCoinsUIText();
        }

        if (ShopItemsList[itemInt].IsPurchased)
        {
            AlreadyBought.SetActive(true);
            AlreadyBought.SetActive(true);
            InvalidCoins.SetActive(false);
            InvalidGems.SetActive(false);
            PurchaseSuccessful.SetActive(false);
        }
        else
        {
            Debug.Log("You don't have enough coins");

            InvalidCoins.SetActive(true);
            InvalidGems.SetActive(false);
            PurchaseSuccessful.SetActive(false);
            AlreadyBought.SetActive(false);
        }
    }

    public void OnGemButtonClicked()
    {
        if (Game.Instance.HasEnoughGems(ShopItemsList[itemInt].GemValue) && !ShopItemsList[itemInt].IsPurchased)
        {
            InvalidCoins.SetActive(false);
            InvalidGems.SetActive(false);
            AlreadyBought.SetActive(false);

            if (!ShopItemsList[itemInt].IsPurchased)
            {
                PurchaseSuccessful.SetActive(true);
            }

            Game.Instance.UseGems(ShopItemsList[itemInt].GemValue);

            //Purchase item
            ShopItemsList[itemInt].IsPurchased = true;

            //Disable button
            PreviewButton = ShopScrollView.GetChild(itemInt).GetComponent<Button>();
            DisableBuy();

            //Change UI text: gems
            Game.Instance.UpdateAllGemsUIText();
        }

        if(ShopItemsList[itemInt].IsPurchased)
        {
            AlreadyBought.SetActive(true);
            InvalidCoins.SetActive(false);
            InvalidGems.SetActive(false);
            PurchaseSuccessful.SetActive(false);
        }
        else
        {
            Debug.Log("You don't have enough gems");

            InvalidCoins.SetActive(false);
            InvalidGems.SetActive(true);
            PurchaseSuccessful.SetActive(false);
            AlreadyBought.SetActive(false);
        }
    }

    void DisableBuy()
    {
        PreviewButton.interactable = false;
        PreviewButton.transform.GetChild(4).gameObject.SetActive(true);
    }

    public void ViewContent()
    {
        ContentsHolder.SetActive(true);
        PreviewHolder.SetActive(false);
    }

    public void ViewPreview()
    {
        PreviewHolder.SetActive(true);
        ContentsHolder.SetActive(false);
    }
}
