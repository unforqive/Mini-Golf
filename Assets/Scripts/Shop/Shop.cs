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

    [SerializeField] TMP_Text ConfirmItemName;
    [SerializeField] GameObject ConfirmItemQuality;
    [SerializeField] GameObject ConfirmItemImage;


    [SerializeField] Animator ConfirmationAnimation;
    [SerializeField] Animator InvalidCoinAnimation;
    [SerializeField] Animator InvalidGemAnimation;

    public GameObject InvalidCoins;
    public GameObject InvalidGems;
    public GameObject PurchaseSuccessful;

    Button CoinConfirmButton;
    Button GemConfirmButton;

    private bool showGemValue;

    public ConfirmPurchase confirmPurchase;

    public int itemInt;

    private bool startInTimer;
    public int inTimer;

    void Start()
    {
        InvalidCoins.SetActive(false);
        InvalidGems.SetActive(false);
        PurchaseSuccessful.SetActive(false);

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

            if (ShopItemsList[i].IsPurchased && ShopItemsList[0].ItemName == "Ball Chest")
            {
                if (ShopItemsList[0].ItemName == "Ball Chest")
                {
                    return;
                }

                DisableBuy();
            }

            PreviewButton.AddEventListener(i, PreviewShopItem);
        }

        Destroy(ItemTemplate);
    }

    private void Update()
    {
        if (startInTimer)
        {
            inTimer += 1;
        }

        if (inTimer == 200)
        {
            ConfirmationAnimation.SetBool("In", true);
            InvalidCoinAnimation.SetBool("In", true);
            InvalidGemAnimation.SetBool("In", true);

            ConfirmationAnimation.SetBool("Out", false);
            InvalidCoinAnimation.SetBool("Out", false);
            InvalidGemAnimation.SetBool("Out", false);

            inTimer = 0;
            startInTimer = false;
        }
    }

    void PreviewShopItem(int itemIndex)
    {
        PreviewHolder.SetActive(true);

        confirmPurchase.ConfirmAnimation.SetBool("In", true);

        PreviewItemName.text = ShopItemsList[itemIndex].ItemName.ToString();
        PreviewItemType.text = ShopItemsList[itemIndex].ItemType.ToString();

        ConfirmItemName.text = ShopItemsList[itemIndex].ItemName.ToString();
        ConfirmItemName.color = ShopItemsList[itemIndex].itemColor;
        ConfirmItemQuality.GetComponent<Image>().sprite = ShopItemsList[itemIndex].QualityImage;

        ConfirmItemImage.GetComponent<Image>().sprite = ShopItemsList[itemIndex].ItemImage;

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

            InvalidCoinAnimation.SetBool("In", true);
            InvalidGemAnimation.SetBool("In", true);

            InvalidCoinAnimation.SetBool("Out", false);
            InvalidGemAnimation.SetBool("Out", false);

            if (!ShopItemsList[itemInt].IsPurchased)
            {
                PurchaseSuccessful.SetActive(true);

                ConfirmationAnimation.SetBool("Out", true);
                ConfirmationAnimation.SetBool("In", false);

                startInTimer = true;
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
        else
        {
            Debug.Log("You don't have enough coins");

            InvalidCoins.SetActive(true);
            InvalidGems.SetActive(false);
            PurchaseSuccessful.SetActive(false);

            InvalidCoinAnimation.SetBool("Out", true);
            InvalidCoinAnimation.SetBool("In", false);

            startInTimer = true;
        }
    }

    public void OnGemButtonClicked()
    {
        if (Game.Instance.HasEnoughGems(ShopItemsList[itemInt].GemValue) && !ShopItemsList[itemInt].IsPurchased)
        {
            InvalidCoins.SetActive(false);
            InvalidGems.SetActive(false);

            InvalidCoinAnimation.SetBool("In", true);
            InvalidGemAnimation.SetBool("In", true);

            InvalidCoinAnimation.SetBool("Out", false);
            InvalidGemAnimation.SetBool("Out", false);

            if (!ShopItemsList[itemInt].IsPurchased)
            {
                PurchaseSuccessful.SetActive(true);

                ConfirmationAnimation.SetBool("Out", true);
                ConfirmationAnimation.SetBool("In", false);

                startInTimer = true;
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
        else
        {
            Debug.Log("You don't have enough gems");

            InvalidCoins.SetActive(false);
            InvalidGems.SetActive(true);
            PurchaseSuccessful.SetActive(false);

            InvalidGemAnimation.SetBool("Out", true);
            InvalidGemAnimation.SetBool("In", false);

            startInTimer = true;
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

    public void CloseConfirmation()
    {
        confirmPurchase.ConfirmHolder.SetActive(false);
    }
}
