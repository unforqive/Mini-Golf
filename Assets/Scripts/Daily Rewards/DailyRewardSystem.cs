using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;
using System.Threading;

namespace DailyRewardSystem
{
    public enum RewardType
    {
        Coins,
        Gems
    }

    [Serializable] public struct Reward
    {
        public RewardType Type;
        public int Amount;
    }

    public class DailyRewardSystem : MonoBehaviour
    {
        [Header("Main UI")]
        [SerializeField] TMP_Text coinsText;
        [SerializeField] TMP_Text gemsText;

        [Space]
        [Header("Leaderboard UI")]
        [SerializeField] TMP_Text coinsText2;
        [SerializeField] TMP_Text gemsText2;

        [Space]
        [Header("Reward UI")]
        [SerializeField] GameObject rewardsObject;

        [SerializeField] TMP_Text rewardAmountText;
        [SerializeField] Button claimButton;
        [SerializeField] TMP_Text noMoreRewardsText;
        [SerializeField] GameObject rewardsPopup;
        [SerializeField] GameObject rewardsPrompt;

        [SerializeField] GameObject Day1Overlay;
        [SerializeField] GameObject Day2Overlay;
        [SerializeField] GameObject Day3Overlay;
        [SerializeField] GameObject Day4Overlay;
        [SerializeField] GameObject Day5Overlay;

        [Space]
        [Header("Animation")]
        public Animator RewardAnim;
        public Animator RewardPopup;
        public Animator RewardNoti;

        [Space]
        [Header("Rewards Objects")]
        [SerializeField] GameObject coinsSmall;
        [SerializeField] GameObject coinsMedium;
        [SerializeField] GameObject coinsLarge;

        [SerializeField] GameObject gemsSmall;
        [SerializeField] GameObject gemsLarge;

        [Space]
        [Header("Rewards Database")]
        [SerializeField] RewardsDatabase rewardsDB;

        [Space]
        [Header("Timing")]
        //next reward wait delay
        [SerializeField] double nextRewardDelay = 24f;
        [SerializeField] float checkForRewardDelay = 5f;
        [SerializeField] int consecutiveDays = 0;

        private int nextRewardIndex;
        private bool isRewardReady;
        private bool noMoreRewards = false;

        private bool startTimer;
        public int timer;

        private string type;

        void Start()
        {
            Initalize();
            CheckForConsecutiveDays();

            StopAllCoroutines();
            StartCoroutine(CheckForRewards());

            rewardsPopup.SetActive(false);
        }

        private void Update()
        {
            if (noMoreRewards)
            {
                noMoreRewardsText.gameObject.SetActive(true);
                claimButton.gameObject.SetActive(false);
            }

            if (!noMoreRewards)
            {
                noMoreRewardsText.gameObject.SetActive(false);
                claimButton.gameObject.SetActive(true);
            }

            if (startTimer)
            {
                timer += 1;
            }

            if (timer == 400)
            {
                RewardPopup.SetBool("In", false);
                RewardPopup.SetBool("Out", true);
                timer = 0;
                startTimer = false;
            }
        }

        void CheckForConsecutiveDays()
        {
            //check for day number (0-4) and set active (true/false)

            if (consecutiveDays == 0)
            {
                Day1Overlay.SetActive(false);
                Day2Overlay.SetActive(false);
                Day3Overlay.SetActive(false);
                Day4Overlay.SetActive(false);
                Day5Overlay.SetActive(false);
            }

            if (consecutiveDays == 1)
            {
                Day1Overlay.SetActive(true);
                Day2Overlay.SetActive(false);
                Day3Overlay.SetActive(false);
                Day4Overlay.SetActive(false);
                Day5Overlay.SetActive(false);
            }

            if (consecutiveDays == 2)
            {
                Day1Overlay.SetActive(true);
                Day2Overlay.SetActive(true);
                Day3Overlay.SetActive(false);
                Day4Overlay.SetActive(false);
                Day5Overlay.SetActive(false);
            }

            if (consecutiveDays == 3)
            {
                Day1Overlay.SetActive(true);
                Day2Overlay.SetActive(true);
                Day3Overlay.SetActive(true);
                Day4Overlay.SetActive(false);
                Day5Overlay.SetActive(false);
            }

            if (consecutiveDays == 4)
            {
                Day1Overlay.SetActive(true);
                Day2Overlay.SetActive(true);
                Day3Overlay.SetActive(true);
                Day4Overlay.SetActive(true);
                Day5Overlay.SetActive(false);
            }

            if (consecutiveDays == 5)
            {
                Day1Overlay.SetActive(true);
                Day2Overlay.SetActive(true);
                Day3Overlay.SetActive(true);
                Day4Overlay.SetActive(true);
                Day5Overlay.SetActive(true);
            }
        }

        void Initalize()
        {
            consecutiveDays = PlayerPrefs.GetInt("Consecutive_Days");

            if (consecutiveDays == 5)
            {
                consecutiveDays = 0; //reset day count after 5 days
            }

            nextRewardIndex = PlayerPrefs.GetInt("Next_Reward_Index", 0);

            Game.Instance.UpdateAllCoinsUIText();
            Game.Instance.UpdateAllGemsUIText();

            //Add Click Events
            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(OnClaimButtonClick);

            //Check if the game is opened for the first time then set Reward_Claim_Datetime to current datetime
            if (string.IsNullOrEmpty (PlayerPrefs.GetString("Reward_Claim_Datetime")))
            {
                PlayerPrefs.SetString("Reward_Claim_Datetime", DateTime.Now.ToString());
            }
        }

        IEnumerator CheckForRewards()
        {
            while (true)
            {
                if (!isRewardReady)
                {
                    DateTime currentDatetime = DateTime.Now;
                    DateTime rewardClaimDatetime = DateTime.Parse(PlayerPrefs.GetString("Reward_Claim_Datetime", currentDatetime.ToString()));

                    //get total between these 2 dates
                    double elapsedHours = (currentDatetime - rewardClaimDatetime).TotalHours;

                    if (elapsedHours >= nextRewardDelay)
                    {
                        ActivateReward();
                    }
                    else
                    {
                        DeactivateReward();
                    }
                }
                yield return new WaitForSeconds(checkForRewardDelay);
            } 
        }

        void ActivateReward()
        {
            isRewardReady = true;
            noMoreRewards = false;

            //Update reward UI
            Reward reward = rewardsDB.GetReward(nextRewardIndex);
            if(reward.Type == RewardType.Coins)
            {
                if (reward.Amount == 100)
                {
                    coinsSmall.SetActive(true);
                    coinsMedium.SetActive(false);
                    coinsLarge.SetActive(false);

                    gemsSmall.SetActive(false);
                    gemsLarge.SetActive(false);
                }

                if (reward.Amount == 500)
                {
                    coinsSmall.SetActive(false);
                    coinsMedium.SetActive(true);
                    coinsLarge.SetActive(false);

                    gemsSmall.SetActive(false);
                    gemsLarge.SetActive(false);
                }

                if (reward.Amount == 2000)
                {
                    coinsSmall.SetActive(false);
                    coinsMedium.SetActive(false);
                    coinsLarge.SetActive(true);

                    gemsSmall.SetActive(false);
                    gemsLarge.SetActive(false);
                }
     
            }
            else if (reward.Type == RewardType.Gems)
            {
                if (reward.Amount == 2)
                {
                    coinsSmall.SetActive(false);
                    coinsMedium.SetActive(false);
                    coinsLarge.SetActive(false);

                    gemsSmall.SetActive(true);
                    gemsLarge.SetActive(false);
                }

                if (reward.Amount == 5)
                {
                    coinsSmall.SetActive(false);
                    coinsMedium.SetActive(false);
                    coinsLarge.SetActive(false);

                    gemsSmall.SetActive(false);
                    gemsLarge.SetActive(true);
                }
            }

            rewardAmountText.text = reward.Amount + " " + type;

            if (consecutiveDays == 5)
            {
                consecutiveDays = 0; //reset day count after 5 days
            }     
        }

        void DeactivateReward()
        {
            isRewardReady = false;
            noMoreRewards = true;
        }

        void OnClaimButtonClick()
        {
            rewardsPrompt.SetActive(true);
            RewardPopup.SetBool("In", true);
            RewardPopup.SetBool("Out", false);

            consecutiveDays += 1;
            PlayerPrefs.SetInt("Consecutive_Days", consecutiveDays);

            CheckForConsecutiveDays();

            rewardsPopup.SetActive(true);

            startTimer = true;

            Reward reward = rewardsDB.GetReward(nextRewardIndex);

            //Check reward type
            if (reward.Type == RewardType.Coins)
            {
                type = "Coins";
                Debug.Log("<color=yellow>" + reward.Type.ToString() + "Claimed : </color>+" + reward.Amount);
                CurrencyData.Coins += reward.Amount;

                Game.Instance.UpdateAllCoinsUIText();
            } 
            else if (reward.Type == RewardType.Gems)
            {
                type = "Gems";
                Debug.Log("<color=purple>" + reward.Type.ToString() + "Claimed : </color>+" + reward.Amount);
                CurrencyData.Gems += reward.Amount;
                Game.Instance.UpdateAllGemsUIText();

                isRewardReady = false;
            }

            //Save next reward index
            nextRewardIndex++;
            if(nextRewardIndex >= rewardsDB.RewardsCount)
            {
                nextRewardIndex = 0;
            }

            PlayerPrefs.SetInt("Next_Reward_Index", nextRewardIndex);

            //Save DateTime of the last claimed click
            PlayerPrefs.SetString("Reward_Claim_Datetime", DateTime.Now.ToString());

            DeactivateReward();

            RewardAnim.SetBool("In", false);
            RewardAnim.SetBool("Out", true);

            RewardNoti.SetBool("In", true);
            RewardNoti.SetBool("Out", false);
        }

        public void CloseButton()
        {
            rewardsPrompt.SetActive(true);

            RewardAnim.SetBool("In", false);
            RewardAnim.SetBool("Out", true);

            RewardNoti.SetBool("In", true);
            RewardNoti.SetBool("Out", false);
        }

        public void ShowRewards()
        {
            RewardAnim.SetBool("In", true);
            RewardAnim.SetBool("Out", false);

            RewardNoti.SetBool("In", false);
            RewardNoti.SetBool("Out", true);
        }
    }
}


