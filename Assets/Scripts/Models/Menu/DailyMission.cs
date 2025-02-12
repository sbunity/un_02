using Enums;

namespace Models.Menu
{
    [System.Serializable]
    public class DailyMission
    {
        public MissionType missionID;    
        public bool isCompleted;         
        public long nextAvailableTime;
        public bool rewardClaimed;
    }
}