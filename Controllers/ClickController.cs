using UnityEngine;

using GeekBrains.lesson3;

namespace GeekBrains.Controllers.lesson3
{
    public class ClickController : MonoBehaviour
    {
        [SerializeField] public LayerMask ClickMask;
        [SerializeField] public Material MaterialLine;

        #region Hashing Data
        private PlayerMover _player = null;
        private Camera _camera;
        private LineRenderer _lineRender;
        private int _maxMovePointsWay;
        #endregion

        #region Statics values
        private static readonly Vector3 _BAN_POINT = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        private static int _MOUSE_BUTTON_LEFT = (int)MouseButton.LeftButton;
        private const int   _LINE_RENDER_NUMER_SIGMENTS = 2;
        private const float _LINE_RENDER_WIDTHS = 0.1f;
        private const float _GRADIENT_MIN = 0.0f;
        private const float _GRADIENT_MAX = 1.0f;
        private const int _COUNT_COLOR_KEYS = 2;
        private const float _OFFSET_LINE_MOUSE = 0.2f;
        #endregion

        private RaycastHit _hitInfo;

        private Vector3 _lastPoint;
        private Vector3 _drawToPoint = _BAN_POINT;

        private void Awake()
        {
            _hitInfo = new RaycastHit();
        }

        private void Start()
        {
            _player = FindObjectOfType<PlayerMover>();
            if (_player == null)
                Debug.Log(_ERR_NOT_EXIST_PLAYER);
            _maxMovePointsWay = _player.MaxCountMovePoint;
            _lastPoint = _player.Transform.position;

            _camera = Camera.main;

            var colorKeys = new GradientColorKey[_COUNT_COLOR_KEYS];
            colorKeys[0].color = Color.blue;
            colorKeys[0].time = _GRADIENT_MIN;
            colorKeys[1].color = new Color(0.3f, 0.7f, 0.5f);
            colorKeys[1].time = _GRADIENT_MAX;

            var gradient = new Gradient();
            gradient.colorKeys = colorKeys;
            gradient.mode = GradientMode.Fixed;
            
            var temp = new GameObject("LineRender");
            _lineRender = temp.AddComponent<LineRenderer>();
            _lineRender.material = MaterialLine;
            _lineRender.colorGradient = gradient;
            _lineRender.startWidth = _LINE_RENDER_WIDTHS;
            _lineRender.endWidth = _LINE_RENDER_WIDTHS;
            _lineRender.positionCount = _LINE_RENDER_NUMER_SIGMENTS;
        }


        private void Update()
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out _hitInfo, ClickMask))
            {
                _drawToPoint = _hitInfo.point;
            }
            else return;

            if (Input.GetMouseButtonDown(_MOUSE_BUTTON_LEFT))
            {
                var resultPlayer = _player.AskAddPointPath(_drawToPoint);

                if (resultPlayer.TypeMessage == TypeMessagePlayer.AllIsOk)
                {
                    _lastPoint = _drawToPoint;
                    _lastPoint.y += _OFFSET_LINE_MOUSE;
                }

                SendMessageToUi(resultPlayer.Message);
            }

            _drawToPoint.y += _OFFSET_LINE_MOUSE;
            if (_player.IsMove())
            {                
                ShowLastPointToMouse(_drawToPoint);
            }
            else
            {
                _lastPoint = _player.Transform.position;
                ShowLastPointToMouse(_drawToPoint);
            }
        }


        private void SendMessageToUi(string message)
        {
            var instance = Main.Instance;
            if (instance != null)
            {
                instance.UiController.SetMessage(message);
            }
        }

        private void ShowLastPointToMouse(Vector3 targetPoint)
        {
            if (_lastPoint != _BAN_POINT)
            {
                _lineRender.SetPosition(0, targetPoint);
                _lineRender.SetPosition(1, _lastPoint);
            }
        }


        #region Errors values
        private const string _ERR_NOT_EXIST_PLAYER = "Error. ClickController not exis PlayerMover class";
        #endregion
    }
}