using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeekBrains.Controllers.lesson3
{
    public class ViewcameraController : MonoBehaviour
    {
        [SerializeField] public float ScrollSpeed = 2.0f;

        #region Static values
        private const float _NO_SCROLL = 0.0f;
        private readonly int _CENTER_MOUSE_BUTTON = (int)MouseButton.CenterButton;
        #endregion

        private Transform _camera;
        private Vector3 _directionMove;
        private Vector3 _toRight;
        private Vector3 _toUp;

        private void Awake()
        {
            _camera = Camera.main.transform;

            _directionMove = _camera.forward;
            _toRight = _camera.right;
            _toUp = _camera.up;
        }

        private void Update()
        {
            var pos = _camera.position;

            float axis = Input.GetAxis("Mouse ScrollWheel");
            if (axis == _NO_SCROLL) { }
            else
            {
                pos += axis * _directionMove * ScrollSpeed;
            }

            if (Input.GetMouseButton(_CENTER_MOUSE_BUTTON))
            {
                Debug.Log("center mouse buttom droped");
                float horisontal = Input.GetAxis("Mouse X");
                float vertical = Input.GetAxis("Mouse Y");

                pos += -_toRight * horisontal - _toUp * vertical;
            }

            _camera.position = pos;
        }
    }
}