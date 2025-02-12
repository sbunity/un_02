using System;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Models.Menu
{
    public static class MissionsModel
    {
        private const string MissionsDataKey = "MissionsModel.MissionsData";
        
        public static MissionState GetMissionState(MissionType missionID)
        {
            DailyMissionsData missionsData = GetMissionsData();
            DailyMission mission = missionsData.missions.Find(m => m.missionID == missionID);
            if (mission == null) return MissionState.Default;

            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            
            if (mission.isCompleted && !mission.rewardClaimed)
                return MissionState.Completed;
            
            if (mission.rewardClaimed && mission.nextAvailableTime > currentTime)
                return MissionState.Time;

            return MissionState.Default;
        }
        
        public static void CompleteMission(MissionType missionID)
        {
            DailyMissionsData missionsData = GetMissionsData();
            
            DailyMission mission = missionsData.missions.Find(m => m.missionID == missionID);
            if (mission == null) return;

            mission.isCompleted = true;
            mission.nextAvailableTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 86400;
            SaveMissions(missionsData);
        }
        
        public static void ClaimReward(MissionType missionID)
        {
            DailyMissionsData missionsData = GetMissionsData();
            DailyMission mission = missionsData.missions.Find(m => m.missionID == missionID);
    
            if (mission == null || !mission.isCompleted || mission.rewardClaimed) return;

            mission.rewardClaimed = true;
            mission.nextAvailableTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 86399;

            SaveMissions(missionsData);
        }
        
        public static TimeSpan GetTimeUntilMissionAvailable(MissionType missionID)
        {
            DailyMissionsData missionsData = GetMissionsData();
            DailyMission mission = missionsData.missions.Find(m => m.missionID == missionID);

            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            long remainingTime = mission.nextAvailableTime - currentTime;

            return TimeSpan.FromSeconds(remainingTime);
        }
        
        private static DailyMissionsData GetMissionsData()
        {
            if (PlayerPrefs.HasKey(MissionsDataKey))
            {
                string json = PlayerPrefs.GetString(MissionsDataKey);
                return JsonUtility.FromJson<DailyMissionsData>(json);
            }
            else
            {
                return CreateDefaultMissions();
            }
        }
        
        private static void SaveMissions(DailyMissionsData missionsData)
        {
            string json = JsonUtility.ToJson(missionsData);
            PlayerPrefs.SetString(MissionsDataKey, json);
            PlayerPrefs.Save();
        }
        
        private static DailyMissionsData CreateDefaultMissions()
        {
            DailyMissionsData missionsData = new DailyMissionsData();
            
            missionsData.missions = new List<DailyMission>
            {
                new DailyMission { missionID = MissionType.First, isCompleted = false, nextAvailableTime = 0, rewardClaimed = false },
                new DailyMission { missionID = MissionType.Second, isCompleted = false, nextAvailableTime = 0, rewardClaimed = false },
                new DailyMission { missionID = MissionType.Third, isCompleted = false, nextAvailableTime = 0, rewardClaimed = false }
            };
            
            return missionsData;
        }
    }
}