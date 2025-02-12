using System;
using Enums;
using Models.Menu;
using UnityEngine;
using Values;

namespace Models.Scenes
{
    public class GameSceneModel
    {
        private int _bet;
        private int _winBalance;
        private int _ballsCount;
        private DateTime _startTime;

        private const int MIN_BET = 1;
        private const int MAX_BET = 15;

        public bool IsPlusBtnActive => _bet < MAX_BET && _bet +1 <= Wallet.Money;
        public bool IsMinusBtnActive => _bet > MIN_BET;

        public int Bet => _bet;
        public int Points => _winBalance;
        public bool IsWin => _winBalance > 0;
        public int Reward => _winBalance > 0 ?  _winBalance * _bet : 0;

        public GameSceneModel()
        {
            _bet = MIN_BET;
            _winBalance = 0;
            _ballsCount = 0;
        }

        public void SubtractBetFromBalance()
        {
            Wallet.TryPurchase(_bet);
            
            _startTime = DateTime.UtcNow;
        }

        public void AddPoints(int value)
        {
            _winBalance += value;
            _ballsCount++;
        }
        
        public void TryAddRewardToWallet()
        {
            if (IsWin)
            {
                Wallet.AddMoney(_winBalance);
            }
        }

        public void TryCompletedMissions()
        {
            if (Reward >= 1000 && MissionsModel.GetMissionState(MissionType.First) == MissionState.Default)
            {
                MissionsModel.CompleteMission(MissionType.First);
            }

            if (_ballsCount >= 50 && MissionsModel.GetMissionState(MissionType.Second) == MissionState.Default)
            {
                MissionsModel.CompleteMission(MissionType.Second);
            }
            
            Debug.Log(DateTime.UtcNow.Subtract(_startTime).Minutes);

            if (DateTime.UtcNow.Subtract(_startTime).Minutes >= 3 &&
                MissionsModel.GetMissionState(MissionType.Third) == MissionState.Default)
            {
                MissionsModel.CompleteMission(MissionType.Third);
            }
        }

        public void ChangeBet(int direction)
        {
            _bet += direction;
        }
    }
}