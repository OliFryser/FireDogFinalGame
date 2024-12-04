using System.Collections.Generic;
using Dialogue;
using Player;
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
        private DialoguePlayer _dialogPlayer;
        private PlayerStats _playerStats;
        
        [SerializeField]
        private DialogueSequence _firstSequence;
        [SerializeField]
        private DialogueSequence _shorterSequence;

        protected override void Start()
        {
            base.Start();
            _dialogPlayer = FindAnyObjectByType<DialoguePlayer>();
            _enemyTracker = FindAnyObjectByType<EnemyTracker>();
            _playerStats = FindAnyObjectByType<PlayerStats>();
        }
        
        public override void Interact()
        {
            base.Interact();
            DialogueSequence sequence = 
                _playerStats.HasStartedLevel6 ? _shorterSequence: _firstSequence;
            _playerStats.StartLevel6();
            _dialogPlayer.StartDialog(sequence, ActivateLamps);
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
            
            Destroy(gameObject);
        }
    }
}