using UnityEngine;
using UnityEngine.AI;

namespace GeekBrains.lesson3
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PlayerMover : BaseGameObject
    {
        [SerializeField] public int MaxCountMovePoint = 10;
        [SerializeField] public MovePointsPlayer PrefubViewPointsWay;
        [SerializeField] public Material MaterialCurrentWayDraw;
        [SerializeField] public Material MaterialWaitWayDraw;

        #region Static values
        private static readonly Vector3 _VECTOR3_ZERO = Vector3.zero;
        private static readonly Vector3 _VECTOR3_DOWN = Vector3.down;
        private static readonly Vector3 _OFFSET_DRAW = new Vector3(0.0f, 0.1f, 0.0f);
        private const float _NO_AXIS_DIRECTION = 0.0f;
        private const string _NAME_POINTS_MOVE = "Move Points";
        private const string _NAME_DRAW_TO_PLAYER = "Player draw Way";
        private const string _NAME_DRAW_WAIT = "Wait way draw";
        private const int _FIRST_INDEX = 0;
        private const int _SECOUND_INDEX = 1;
        private const int _EMPTY_MASSIVE = 0;
        private const int _MIN_COUNT_WAY = 1;
        #endregion

        #region Hashing Messages Player
        private static readonly PlayerMessages _MESSAGE_GOOD = new PlayerMessages(TypeMessagePlayer.AllIsOk, _MSG_ANDERSTOOD);
        private static readonly PlayerMessages _MESSAGE_LONG = new PlayerMessages(TypeMessagePlayer.TooLongWay, _MSG_LONG_WAY);
        private static readonly PlayerMessages _MESSAGE_NO_WAY = new PlayerMessages(TypeMessagePlayer.WayNotExist, _MSG_NO_WAY);
        private const string _MSG_ANDERSTOOD = "Вас поянл";
        private const string _MSG_LONG_WAY = "Не, столько я запоминать не буду";
        private const string _MSG_NO_WAY = "А вот туда я не пройду";
        #endregion

        #region Hashing datas
        private NavMeshAgent _agent;
        private NavMeshPath _teoreticPath;
        private NavMeshPath _currentPath;
        #endregion

        private QueuePoolPaths<MovePointsPlayer> _paths;
        private SimpleDrawingPath _drawingPathToPlayer;
        private DrawingPath _drawWaitPath;
        private Vector3 _lastPoint;

        protected override void Awake()
        {
            base.Awake();

            _agent = GetComponent<NavMeshAgent>();
            _teoreticPath = new NavMeshPath();
            _currentPath = new NavMeshPath();

            _paths = new QueuePoolPaths<MovePointsPlayer>(MaxCountMovePoint, _NAME_POINTS_MOVE);
            _paths.LoadPool(PrefubViewPointsWay);

            _drawingPathToPlayer = new SimpleDrawingPath(_NAME_DRAW_TO_PLAYER, MaterialCurrentWayDraw);
            _drawingPathToPlayer.SetOffserts(Vector3.zero, _OFFSET_DRAW);
            _drawWaitPath = new DrawingPath(_NAME_DRAW_WAIT, MaterialWaitWayDraw);
            
            _lastPoint = Transform.position;
        }

        public bool IsMove()
        {
            return !_paths.IsEmpty;
        }


        public PlayerMessages AskAddPointPath(Vector3 point)
        {
            if (!_paths.IsFull) { }
            else return _MESSAGE_LONG;

            bool havePath = NavMesh.CalculatePath(_lastPoint, point, NavMesh.AllAreas, _teoreticPath);
            havePath &= _teoreticPath.status != NavMeshPathStatus.PathPartial;

            if (havePath) { }
            else return _MESSAGE_NO_WAY;

            AddPath(point);
            return _MESSAGE_GOOD;
        }

        public void PointWayAchieved(MovePointsPlayer collision)
        {
            //Быть готовым к проблемам...
            if (collision == _paths.Peek())
            {
                _paths.Dequeue();
                _drawWaitPath.Redraw(_paths.ArrayPaths(), _OFFSET_DRAW);

                if (_paths.IsEmpty) return;

                var target = _paths.PeekPoint();
                _drawingPathToPlayer.SetEndPoint(target);

                NavMesh.CalculatePath(Transform.position, target, NavMesh.AllAreas, _teoreticPath);
                _agent.SetPath(_teoreticPath);
            }
        }

        private void AddPath(Vector3 endPoint)
        {
            if (_paths.IsEmpty)
            {
                _agent.SetPath(_teoreticPath);
                _drawingPathToPlayer.SetStartPoint(Transform.position);
                _drawingPathToPlayer.SetEndPoint(endPoint);
            }

            _paths.Enqueue(endPoint, Quaternion.identity);
            _drawWaitPath.Redraw(_paths.ArrayPaths(), _OFFSET_DRAW);
            _lastPoint = endPoint;
        }
 

        private void Update()
        {
            if (IsMove())
            {
                _drawingPathToPlayer.SetStartPoint(Transform.position);
            }
        }
        
        #region Errors values
        #endregion
    }
}