using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuController : MonoBehaviour
{
    #region Public Variables

    public GameObject StartScreen;
    public GameObject SettingsMenu;
    public GameObject PlayMenu;
    public GameObject SkinsMenu;
    public GameObject ShopMenu;
    public GameObject GiftsMenu;
    public GameObject SplashScreen;

    public static string menu;
    public static string nextMenu;

    public Animator splashScreenButtonAnimation;
    public Animator StartScreenButtonsAnimation;
    public Animator SettingsMenuAnimation;
    public Animator PlayMenuAnimation;
    public Animator SkinsMenuAnimation;
    public Animator ShopMenuAnimation;
    public Animator GiftsMenuAnimation;

    public bool startScreenAnimationTimer;

    public bool returnToMenu;

    public AudioHandler audioHandler;

    public TMPro.TMP_Text qualityText;

    public int quality;

    public TMP_Dropdown qualityDropDown;

    public GameHandler gameHandler;

    public bool inGame;

    public Slider powerBar;

    #endregion

    #region Private Variables

    private int MenuAnimationTimer;

    #endregion

    void Awake()
    {
        quality = PlayerPrefs.GetInt("Quality");
        qualityDropDown.value = quality;

        Debug.Log("Player Preferences Loaded.");
    }

    void Start()
    {
        menu = "Splash Screen";

        startScreenAnimationTimer = false;
        MenuAnimationTimer = 0;

        returnToMenu = false;

        inGame = false;
    }

    void Update()
    {
        if (startScreenAnimationTimer)
        {
            MenuAnimationTimer += 1;
        }

        if (MenuAnimationTimer > 20)
        {
            if (nextMenu == "Start Menu")
            {
                menu = "Start Screen";
            }

            if (nextMenu == "Play Menu")
            {
                menu = "Play Screen";
            }

            if (nextMenu == "Settings Menu")
            {
                menu = "Settings Screen";
            }

            if (nextMenu == "Shop Menu")
            {
                menu = "Shop Screen";
            }

            if (nextMenu == "Skins Menu")
            {
                menu = "Skins Screen";
            }

            if (nextMenu == "Gifts Menu")
            {
                menu = "Gifts Screen";
            }

            startScreenAnimationTimer = false;
            MenuAnimationTimer = 0;
        }

        if (menu == "Splash Screen")
        {
            SplashScreen.SetActive(true);
            StartScreen.SetActive(false);
            SettingsMenu.SetActive(false);
            PlayMenu.SetActive(false);
            SkinsMenu.SetActive(false);
            ShopMenu.SetActive(false);
            GiftsMenu.SetActive(false);
        }

        if (menu == "Start Screen")
        {
            SplashScreen.SetActive(false);
            StartScreen.SetActive(true);
            SettingsMenu.SetActive(false);
            PlayMenu.SetActive(false);
            SkinsMenu.SetActive(false);
            ShopMenu.SetActive(false);
            GiftsMenu.SetActive(false);
        }

        if (menu == "Start Screen" && returnToMenu)
        {
            SplashScreen.SetActive(false);
            StartScreen.SetActive(true);
            SettingsMenu.SetActive(false);
            PlayMenu.SetActive(false);
            SkinsMenu.SetActive(false);
            ShopMenu.SetActive(false);
            GiftsMenu.SetActive(false);

            StartScreenButtonsAnimation.SetBool("Start Appear", true);
            StartScreenButtonsAnimation.SetBool("Start Disappear", false);

            returnToMenu = false;
        }

        if (menu == "Play Screen")
        {
            SplashScreen.SetActive(false);
            StartScreen.SetActive(false);
            SettingsMenu.SetActive(false);
            PlayMenu.SetActive(true);
            SkinsMenu.SetActive(false);
            ShopMenu.SetActive(false);
            GiftsMenu.SetActive(false);
        }

        if (menu == "Settings Screen")
        {
            SplashScreen.SetActive(false);
            StartScreen.SetActive(false);
            SettingsMenu.SetActive(true);
            PlayMenu.SetActive(false);
            SkinsMenu.SetActive(false);
            ShopMenu.SetActive(false);
            GiftsMenu.SetActive(false);
        }

        if (menu == "Shop Screen")
        {
            SplashScreen.SetActive(false);
            StartScreen.SetActive(false);
            SettingsMenu.SetActive(false);
            PlayMenu.SetActive(false);
            SkinsMenu.SetActive(false);
            ShopMenu.SetActive(true);
            GiftsMenu.SetActive(false);
        }

        if (menu == "Skins Screen")
        {
            SplashScreen.SetActive(false);
            StartScreen.SetActive(false);
            SettingsMenu.SetActive(false);
            PlayMenu.SetActive(false);
            SkinsMenu.SetActive(true);
            ShopMenu.SetActive(false);
            GiftsMenu.SetActive(false);
        }

        if (menu == "Gifts Screen")
        {
            SplashScreen.SetActive(false);
            StartScreen.SetActive(false);
            SettingsMenu.SetActive(false);
            PlayMenu.SetActive(false);
            SkinsMenu.SetActive(false);
            ShopMenu.SetActive(false);
            GiftsMenu.SetActive(true);
        }
    }

    #region Menu Buttons

    public void BeginningScreen()
    {
        startScreenAnimationTimer = true;

        nextMenu = "Start Menu";

        splashScreenButtonAnimation.SetBool("CameraDown", true);
    }

    public void Play()
    {
        startScreenAnimationTimer = true;

        nextMenu = "Play Menu";

        StartScreenButtonsAnimation.SetBool("Start Appear", false);
        StartScreenButtonsAnimation.SetBool("Start Disappear", true);

        audioHandler.sfx.PlayOneShot(audioHandler.longSwooshSFX);
    }

    public void Settings()
    {
        startScreenAnimationTimer = true;

        nextMenu = "Settings Menu";

        StartScreenButtonsAnimation.SetBool("Start Appear", false);
        StartScreenButtonsAnimation.SetBool("Start Disappear", true);

        audioHandler.sfx.PlayOneShot(audioHandler.longSwooshSFX);
    }

    public void Shop()
    {
        startScreenAnimationTimer = true;

        nextMenu = "Shop Menu";

        StartScreenButtonsAnimation.SetBool("Start Appear", false);
        StartScreenButtonsAnimation.SetBool("Start Disappear", true);

        audioHandler.sfx.PlayOneShot(audioHandler.longSwooshSFX);
    }

    public void Skins()
    {
        startScreenAnimationTimer = true;

        nextMenu = "Skins Menu";

        StartScreenButtonsAnimation.SetBool("Start Appear", false);
        StartScreenButtonsAnimation.SetBool("Start Disappear", true);

        audioHandler.sfx.PlayOneShot(audioHandler.longSwooshSFX);
    }

    public void Gifts()
    {
        startScreenAnimationTimer = true;

        nextMenu = "Gifts Menu";

        StartScreenButtonsAnimation.SetBool("Start Appear", false);
        StartScreenButtonsAnimation.SetBool("Start Disappear", true);

        audioHandler.sfx.PlayOneShot(audioHandler.longSwooshSFX);
    }

    #endregion

    #region Close Menu Buttons

    public void CloseMenu()
    {
        if (menu == "Play Screen")
        {
            PlayMenuAnimation.SetBool("Play Appear", false);
            PlayMenuAnimation.SetBool("Play Disappear", true);
            returnToMenu = true;

            audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
        }

        if (menu == "Settings Screen")
        {
            SettingsMenuAnimation.SetBool("Settings Appear", false);
            SettingsMenuAnimation.SetBool("Settings Disappear", true);
            returnToMenu = true;

            audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
        }

        if (menu == "Shop Screen")
        {
            ShopMenuAnimation.SetBool("Shop Appear", false);
            ShopMenuAnimation.SetBool("Shop Disappear", true);
            returnToMenu = true;

            audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
        }

        if (menu == "Skins Screen")
        {
            SkinsMenuAnimation.SetBool("Skins Appear", false);
            SkinsMenuAnimation.SetBool("Skins Disappear", true);
            returnToMenu = true;

            audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
        }

        if (menu == "Gifts Screen")
        {
            GiftsMenuAnimation.SetBool("Gifts Appear", false);
            GiftsMenuAnimation.SetBool("Gifts Disappear", true);
            returnToMenu = true;

            audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
        }

        startScreenAnimationTimer = true;
        nextMenu = "Start Menu";
    }

    #endregion

    public void StartMenu()
    {
        menu = "Start Screen";

        StartScreenButtonsAnimation.SetBool("Start Appear", true);
        StartScreenButtonsAnimation.SetBool("Start Disappear", false);

        audioHandler.sfx.PlayOneShot(audioHandler.swooshSFX);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        quality = qualityIndex;
    }

    public void SetQualityPref()
    {
        PlayerPrefs.SetInt("Quality", quality);

        Debug.Log("Player Preferences Saved.");
    }

    public void LaunchGame()
    {
        if (menu == "Play Screen")
        {
            CloseMenu();

            gameHandler.EnablePlayerCamera();
            inGame = true;
            powerBar.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}
