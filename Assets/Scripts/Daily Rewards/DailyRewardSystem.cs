using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

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
        [Header("Reward UI")]
        [SerializeField] GameObject rewardsObject;

        [SerializeField] TMP_Text rewardAmountText;
        [SerializeField] Button claimButton;
        [SerializeField] TMP_Text noMoreRewardsText;
        [SerializeField] GameObject rewardsPopup;

        [SerializeField] GameObject Day1Overlay;
        [SerializeField] GameObject Day2Overlay;
        [SerializeField] GameObject Day3Overlay;
        [SerializeField] GameObject Day4Overlay;
        [SerializeField] GameObject Day5Overlay;

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
        [SerializeField] double nextRewardDelay = 10f;
        [SerializeField] int consecutiveDays = 0;

        private int nextRewardIndex;
        public bool noMoreRewards = false;

        void Start()
        {
            Initalize();
            CheckForConsecutiveDays();
            CheckForRewards();

            rewardsPopup.SetActive(false);
        }

        private void Update()
        {
            if (noMoreRewards)
            {
                noMoreRewardsText.gameObject.SetActive(true);
                claimButton.gameObject.SetActive(false);
            }
        }

        void CheckForConsecutiveDays()
        {
            //check for day number (0-4) and set active (true/false)

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

            UpdateCoinsTextUI();
            UpdateGemsTextUI();

            //Add Click Events
            claimButton.onClick.RemoveAllListeners();
            claimButton.onClick.AddListener(OnClaimButtonClick);

            //Check if the game is opened for the first time then set Reward_Claim_Datetime to current datetime
            if (string.IsNullOrEmpty (PlayerPrefs.GetString("Reward_Claim_Datetime")))
            {
                PlayerPrefs.SetString("Reward_Claim_Datetime", DateTime.Now.ToString());
            }
        }

        void CheckForRewards()
        {
            DateTime currentDatetime = DateTime.Now;
            DateTime rewardClaimDatetime = DateTime.Parse(PlayerPrefs.GetString("Reward_Claim_Datetime", currentDatetime.ToString()));

            //get total between these 2 dates
            double elapsedHours = (currentDatetime - rewardClaimDatetime).TotalSeconds;

            if(elapsedHours >= nextRewardDelay)
            {
                ActivateReward();
            }
            else
            {
                DeactivateReward();
            }
        }

        void ActivateReward()
        {
            noMoreRewards = false;

            //Update reward UI
            Reward reward = rewardsDB.GetReward(nextRewardIndex);
            if(reward.Type == RewardType.Coins)
            {
                if (reward.Amount == 100)
                {
                    coinsSmall.SetActive(true);
                }

                else if (reward.Amount == 500)
                {
                    coinsMedium.SetActive(true);
                }

                else if (reward.Amount == 2000)
                {
                    coinsLarge.SetActive(true);
                }
     
            }
            else if (reward.Type == RewardType.Gems)
            {
                if (reward.Amount == 2)
                {
                    gemsSmall.SetActive(true);
                }

                else if (reward.Amount == 5)
                {
                    gemsLarge.SetActive(true);
                }
            }

            rewardAmountText.text = string.Format("+{0}", reward.Amount);

            if (consecutiveDays == 5)
            {
                consecutiveDays = 0; //reset day count after 5 days
            }     
        }

        void DeactivateReward()
        {
            noMoreRewards = true;
            rewardsPopup.SetActive(true);
        }

        void OnClaimButtonClick()
        {
            consecutiveDays += 1;
            PlayerPrefs.SetInt("Consecutive_Days", consecutiveDays);

            CheckForConsecutiveDays();

            Reward reward = rewardsDB.GetReward(nextRewardIndex);

            //Check reward type
            if (reward.Type == RewardType.Coins)
            {
                Debug.Log("<color=yellow>" + reward.Type.ToString() + "Claimed : </color>+" + reward.Amount);
                CurrencyData.Coins += reward.Amount;

                //TODO
                UpdateCoinsTextUI();
            } 
            else if (reward.Type == RewardType.Gems)
            {
                Debug.Log("<color=purple>" + reward.Type.ToString() + "Claimed : </color>+" + reward.Amount);
                CurrencyData.Gems += reward.Amount;

                //TODO
                UpdateGemsTextUI();
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
        }

        //Update UI (coins, gems)
        void UpdateCoinsTextUI()
        {
            coinsText.text = CurrencyData.Coins.ToString();
        }

        void UpdateGemsTextUI()
        {
            gemsText.text = CurrencyData.Gems.ToString();
        }
    }
}


