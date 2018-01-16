using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBrains.lesson3
{
    public class QueuePoolPaths<T> where T:Base3DScenceObject
    {
        #region Static values
        private const int _EMPTY_MASSIVE = 0;
        private const int _FIRST_INDEX = 0;
        #endregion

        private bool _poolIsInitialize = false;

        private readonly string _name;
        private readonly int _maxSize;

        private Transform _baseFolder;
        private T[] _pool;
        private Queue<T> _queue;

        public QueuePoolPaths(int maxSizePool, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _name = name;
            }
            else
            {
                Debug.LogWarning(name);
                _name = string.Empty;
            }

            if (maxSizePool > _EMPTY_MASSIVE)
            {
                _maxSize = maxSizePool;
            }
            else
            {
                if (maxSizePool == _EMPTY_MASSIVE)
                    Debug.LogError(_ERR_MAX_SIZE_IS_ZERO);
                else
                    Debug.LogError(_ERR_NEGATIVE_MAX_SIZE);
            }
        }

        public int MaxSizePool
        {
            get { return _maxSize; }
        }


        public void LoadPool(T instatiateObject)
        {
            if (instatiateObject != null) { }
            else
            {
                Debug.LogError(_ERR_NULL_INSTATIATE_OBJECT);
                return;
            }

            if (!_poolIsInitialize) { }
            else
            {
                Debug.LogWarning(_WARNING_RETRY_LOAD_POOL);
                return;
            }

            _queue = new Queue<T>();
            _pool = new T[_maxSize];

            _baseFolder = new GameObject(_name).transform;
            for (int i = _FIRST_INDEX; i < _pool.Length; ++i)
            {
                var obj = MonoBehaviour.Instantiate(instatiateObject, _baseFolder);
                obj.SetVisible(false);
                _pool[i] = obj;
            }

            _poolIsInitialize = true;
        }

        public bool IsFull
        {
            get
            {
                if (_poolIsInitialize)
                    return _queue.Count >= _maxSize;

                throw new System.Exception(_ERR_NOT_LOAD_POOL);
            }
        }

        public bool IsEmpty
        {
            get
            {
                if (_poolIsInitialize)
                    return _queue.Count <= _EMPTY_MASSIVE;

                throw new System.Exception(_ERR_NOT_LOAD_POOL);
            }
        }

        public int Count
        {
            get
            {
                if (_poolIsInitialize)
                    return _queue.Count;

                throw new System.Exception(_ERR_NOT_LOAD_POOL);
            }
        }


        public void Enqueue(Vector3 position, Quaternion rotation)
        {
            if (_poolIsInitialize) { }
            else throw new System.Exception(_ERR_NOT_LOAD_POOL);

            if (!IsFull) { }
            else
            {
                Debug.LogWarning(_WARNING_POOL_IS_OVERFULL);
                return;
            }

            T seachedObject = null; //Класс можно улучшить используя специфику очереди
            for (int i = _FIRST_INDEX; i < _pool.Length; ++i)
            {
                seachedObject = _pool[i];
                if (seachedObject.IsVisible()) { }
                else break;
            }

            seachedObject.Transform.position = position;
            seachedObject.Transform.rotation = rotation;
            seachedObject.SetVisible(true);
            _queue.Enqueue(seachedObject);
        }

        public void Dequeue()
        {
            if (_poolIsInitialize) { }
            else throw new System.Exception(_ERR_NOT_LOAD_POOL);

            if (IsEmpty) return;

            var deq = _queue.Dequeue();
            deq.SetVisible(false);
        }

        public T Peek()
        {
            if (_poolIsInitialize) { }
            else throw new System.Exception(_ERR_NOT_LOAD_POOL);

            if (!IsEmpty)
                return _queue.Peek();
            else
                return null;
        }

        public Vector3 PeekPoint()
        {
            if (_poolIsInitialize) { }
            else throw new System.Exception(_ERR_NOT_LOAD_POOL);

            if (!IsEmpty)
                return _queue.Peek().Transform.position;
            else
                return Vector3.zero;
        }


        public Vector3[] ArrayPaths()
        {
            if (_poolIsInitialize) { }
            else throw new System.Exception(_ERR_NOT_LOAD_POOL);

            var result = new Vector3[_queue.Count];
            int indexResult = _FIRST_INDEX;
            foreach (var element in _queue)
            {
                result[indexResult] = element.Transform.position;
                ++indexResult;
            }

            return result;
        }


        #region Error and warnings messages
        private const string _WARNING_INPUNT_NAME = "Input name is null or empty";
        private const string _WARNING_POOL_IS_OVERFULL = "Wargning. Can't enqueue at pool. Pool is overfull";
        private const string _WARNING_RETRY_LOAD_POOL = "Trying to reload pool. Pool is already loaded";
        private const string _ERR_NULL_INSTATIATE_OBJECT = "Input instatiate object is null";
        private const string _ERR_NEGATIVE_MAX_SIZE = "Input max size value is neagetive";
        private const string _ERR_MAX_SIZE_IS_ZERO = "Input max size value is zero";
        private const string _ERR_NOT_LOAD_POOL = "Pool is not loaded. Function can't be sucesfull";
        #endregion
    }
}
