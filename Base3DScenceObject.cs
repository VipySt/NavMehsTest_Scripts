using UnityEngine;

namespace GeekBrains
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Base3DScenceObject : BaseGameObject
    {
        public MeshRenderer MeshRender { get; set; }

        private bool _meshRenderOn = true;

        protected override void Awake()
        {
            base.Awake();
            MeshRender = GetComponent<MeshRenderer>();

            _meshRenderOn = MeshRender.enabled;
        }

        public bool IsVisible()
        {
            return _meshRenderOn;
        }

        public virtual void SetVisible(bool visible)
        {
            if (visible == _meshRenderOn) return;
            MeshRender.enabled = visible;
            _meshRenderOn = visible;
        }
    }
}
