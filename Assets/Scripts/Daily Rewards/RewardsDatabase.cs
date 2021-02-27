using DailyRewardSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardsDB", menuName = "Daily Rewards System/Rewards Database")]
public class RewardsDatabase : ScriptableObject
{
    public Reward[] rewards;

    public int RewardsCount
    {
        get { return rewards.Length; }
    }

    public Reward GetReward(int index)
    {
        return rewards[index];
    }
}