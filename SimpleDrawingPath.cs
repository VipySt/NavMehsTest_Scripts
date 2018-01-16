using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBrains.lesson3
{
    public class SimpleDrawingPath
    {
        #region Static values
        private const int _FIRST_POSITION = 0;
        private const int _SECOUND_POSITION = 1;
        private const int _COUNT_POSITIONS = 2;
        private const float _START_WIDHT = 0.15f;
        private const float _END_WIDTH = 0.1f;
        #endregion

        private Vector3 _startOffset;
        private Vector3 _endOffset;
        private LineRenderer _line;

        public SimpleDrawingPath(string name, Material material)
        {
            if (!string.IsNullOrEmpty(name)) { }
            else
            {
                Debug.LogWarning(_WARNING_EMPTY_NAME);
                name = string.Empty;
            }

            if (material != null) { }
            else Debug.LogError(_WARNING_MATERIAL_NULL);

            var folder = new GameObject(name);
            _line = folder.AddComponent<LineRenderer>();
            _line.material = material;
            _line.startWidth = _START_WIDHT;
            _line.endWidth = _END_WIDTH;
            _line.positionCount = _COUNT_POSITIONS;

            _startOffset = Vector3.zero;
            _endOffset = _startOffset;
        }

        public void SetOffserts(Vector3 offsetOfStart, Vector3 offsetOfEnd)
        {
            _startOffset = offsetOfStart;
            _endOffset = offsetOfEnd;
        }


        public void SetStartPoint(Vector3 start)
        {
            _line.SetPosition(_FIRST_POSITION, start + _startOffset);
        }

        public void SetEndPoint(Vector3 end)
        {
            _line.SetPosition(_SECOUND_POSITION, end + _endOffset);
        }

        #region Error and warnings messages
        private const string _WARNING_EMPTY_NAME = "Input name is empty or null";
        private const string _WARNING_MATERIAL_NULL = "Input material is null";
        #endregion
    }
}
