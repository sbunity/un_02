using System;
using Models.Menu;
using Enums;
using Values;

namespace Models.Scenes
{
    public class MenuSceneModel
    {
        private const int FirstRewardCount = 500;
        private const int SecondRewardCount = 800;
        private const int ThirdRewardCount = 1000;
        
        public MissionState FirstMissionState => MissionsModel.GetMissionState(MissionType.First);
        public MissionState SecondMissionState => MissionsModel.GetMissionState(MissionType.Second);
        public MissionState ThirdMissionState => MissionsModel.GetMissionState(MissionType.Third);

        public TimeSpan FirstMissionTimeSpan => MissionsModel.GetTimeUntilMissionAvailable(MissionType.First);
        public TimeSpan SecondMissionTimeSpan => MissionsModel.GetTimeUntilMissionAvailable(MissionType.Second);
        public TimeSpan ThirdMissionTimeSpan => MissionsModel.GetTimeUntilMissionAvailable(MissionType.Third);

        public void ChangeMissionStateToTime(MissionType type)
        {
            MissionsModel.ClaimReward(type);

            switch (type)
            {
                case MissionType.First:
                    Wallet.AddMoney(FirstRewardCount);
                    break;
                case MissionType.Second:
                    Wallet.AddMoney(SecondRewardCount);
                    break;
                case MissionType.Third:
                    Wallet.AddMoney(ThirdRewardCount);
                    break;
            }
        }

        public int GetRewardMission(MissionType type)
        {
            switch (type)
            {
                case MissionType.First:
                    return FirstRewardCount;
                case MissionType.Second:
                    return SecondRewardCount;
                case MissionType.Third:
                    return ThirdRewardCount;
            }

            return 0;
        }
    }
}