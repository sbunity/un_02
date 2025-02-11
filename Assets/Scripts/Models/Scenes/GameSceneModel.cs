using Values;

namespace Models.Scenes
{
    public class GameSceneModel
    {
        private int _bet;
        private int _winBalance;

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
        }

        public void SubtractBetFromBalance()
        {
            Wallet.TryPurchase(_bet);
        }

        public void AddPoints(int value)
        {
            _winBalance += value;
        }
        
        public void TryAddRewardToWallet()
        {
            if (IsWin)
            {
                Wallet.AddMoney(_winBalance);
            }
        }

        public void ChangeBet(int direction)
        {
            _bet += direction;
        }
    }
}