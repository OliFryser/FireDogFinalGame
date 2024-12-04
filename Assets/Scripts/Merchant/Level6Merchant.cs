using System.Collections.Generic;
using SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Merchant
{
    public class Level6Merchant : MerchantBase
    {
        [Header("Music Controller")]
        [SerializeField]
        private MusicController musicController;
        
        [FormerlySerializedAs("lampOffSprites")]
        [Header("Lamp Sprites and Enemies")]
        [SerializeField]
        private List<GameObject> _lampOffSprites;

        [FormerlySerializedAs("lampEnemies")] [SerializeField]
        private List<GameObject> _lampEnemies;

        [SerializeField] private GameObject _decoyEnemy;
        
        private EnemyTracker _enemyTracker;

        protected override void Start()
        {
            base.Start();
            _enemyTracker = FindAnyObjectByType<EnemyTracker>();
        }
        
        public override void Interact()
        {
            base.Interact();
            ActivateLamps();
        }
        
        private void ActivateLamps()
        {
            for (int i = 0; i < _lampOffSprites.Count; i++)
            {
                if (i < _lampOffSprites.Count && _lampOffSprites[i] != null)
                {
                    _lampOffSprites[i].SetActive(false);
                }

                if (i < _lampEnemies.Count && _lampEnemies[i] != null)
                {
                    _lampEnemies[i].SetActive(true);
                }
            }
            
            Destroy(_decoyEnemy);
            _enemyTracker.UnregisterEnemy();

            if (musicController != null)
            {
                musicController.SetBattleState(0.0f);
            }
        }
    }
}