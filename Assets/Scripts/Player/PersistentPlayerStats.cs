using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PersistentPlayerStats : MonoBehaviour
    {
        private HubUpgradeHandler _hubUpgradeHandler;

        [SerializeField]
        private List<HubUpgradeInfo> _hubUpgradeInfos;

        [SerializeField]
        private List<HubUpgrade> _hubUpgrades;

        public List<HubUpgrade> HubUpgrades => _hubUpgrades;

        public int Coins { get; private set; }
        public int Deaths { get; private set; }

        public float FlashlightRadius { get; private set; } = 4.0f;
        public float CriticalAttackChance { get; private set; }
        public int CleaningReward { get; private set; } = 2;
        public bool PassiveHealing { get; set; }

        private void Awake()
        {
            _hubUpgradeHandler = new HubUpgradeHandler(this);
            foreach (var upgradeInfo in _hubUpgradeInfos)
            {
                _hubUpgrades.Add(new HubUpgrade(upgradeInfo, _hubUpgradeHandler));
            }
        }

        public void AddCoins(int amount)
        {
            Coins += amount;
        }

        public void SpendCoins(int amount)
        {
            Coins -= amount;
        }

        public void AddPlayerDeath()
        {
            Deaths++;
        }

        public void IncreaseFlashLightRadius()
        {
            FlashlightRadius += .5f;
        }

        public void IncreaseCriticalAttack()
        {
            CriticalAttackChance++;
        }

        public void IncreaseCleaningReward()
        {
            CleaningReward++;
        }
    
        public void ResetPlayerProgress()
        {
            Destroy(gameObject);
        }
    }
}
