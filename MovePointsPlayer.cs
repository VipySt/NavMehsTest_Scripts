using System;
using UnityEngine;

namespace GeekBrains.lesson3
{
    [RequireComponent(typeof(Collider))]    
    public class MovePointsPlayer : Base3DScenceObject
    {
        private Collider _collider;

        protected override void Awake()
        {
            base.Awake();

            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        public int IndexFormPlayer { get; set; }

        public override void SetVisible(bool visible)
        {
            if (visible == IsVisible()) return;

            base.SetVisible(visible);
            _collider.enabled = visible;
        }

        private void OnTriggerEnter(Collider other)
        {
            var col = other.GetComponent<PlayerMover>();
            if (col != null)
            {
                col.PointWayAchieved(this);
            }
        }
    }
}
