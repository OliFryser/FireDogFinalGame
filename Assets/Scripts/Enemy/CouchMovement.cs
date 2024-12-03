using Lib;
using UnityEngine;

namespace Enemy
{ 
    public class CouchMovement : EnemyMovement
    {
        [SerializeField]
        private GameObject _verticalShadow;

        [SerializeField]
        private GameObject _horizontalShadow;

        private bool _isHorizontal;
        
        protected override void Update()
        {
            base.Update();
            var isHorizontal = Utils.IsHorizontal(_lastMovedDirection);
            if (isHorizontal == _isHorizontal) return;
            _isHorizontal = isHorizontal;
            if (_isHorizontal)
            {
                _horizontalShadow.SetActive(true);
                _verticalShadow.SetActive(false);
            }
            else
            {
                _horizontalShadow.SetActive(false);
                _verticalShadow.SetActive(true);
            }
        }
    }
}
