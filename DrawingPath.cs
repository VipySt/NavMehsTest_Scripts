using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeekBrains.lesson3
{
    public class DrawingPath
    {
        #region Consts
        private const string _BASE_NAME = "Draw Path";
        private const int _FIRST_INDEX = 0;
        private const int _SECOUND_INDEX = 1;
        private const int _EMPTY_POSITIONS_COUNT = 0;
        private const float _WIDTH_DRAW_LINE_START = 0.15f;
        private const float _WIDTH_DRAW_LINE_END = 0.1f;
        private const float _COLOR_TIME_KEY_1 = 0.0f;
        private const float _COLOR_TIME_KEY_2 = 1.0f;
        #endregion

        private Transform _baseObject;
        private LineRenderer _line;

        private int _countPositions = _EMPTY_POSITIONS_COUNT;


        public DrawingPath(string name, Material material)
        {
            string nameBase = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                nameBase = string.Format("{0} {1}", _BASE_NAME, name);
            }
            else
            {
                Debug.LogError(_ERR_NULL_OR_EMPTY_NAME);
            }

            if (material) { }
            else Debug.LogError(_ERR_MATERIAL_NULL);

            var keysColor = new GradientColorKey[2];
            keysColor[0].color = Color.gray;
            keysColor[0].time = _COLOR_TIME_KEY_1;
            keysColor[1].color = Color.green;
            keysColor[1].time = _COLOR_TIME_KEY_2;

            var gradient = new Gradient();
            gradient.colorKeys = keysColor;
            gradient.mode = GradientMode.Fixed;

            _baseObject = new GameObject(nameBase).GetComponent<Transform>();
            _line = _baseObject.gameObject.AddComponent<LineRenderer>();
            _line.material = material;
            _line.startColor = Color.gray;
            _line.endColor = Color.green;
            _line.startWidth = _WIDTH_DRAW_LINE_START;
            _line.endWidth = _WIDTH_DRAW_LINE_END;
            _line.positionCount = _EMPTY_POSITIONS_COUNT;
        }

        
        public void MoveStart(Vector3 position)
        {
            if (_countPositions > _EMPTY_POSITIONS_COUNT) { }
            else
            {
                Debug.LogError(_ERR_EMPTY_POSITIONS_COUNT);
                return;
            }

            _line.SetPosition(_FIRST_INDEX, position);
        }

        public void Redraw(Vector3[] positions, Vector3 offset)
        {
            if (positions != null) { }
            else throw new System.ArgumentNullException(_ERR_EMPTY_IN_POSITIONS);

            _countPositions = positions.Length;
            _line.positionCount = _countPositions;

            for (int i = _FIRST_INDEX; i < positions.Length; ++i)
                _line.SetPosition(i, positions[i] + offset);
        }

        public void Redraw(Vector3 startPosition, Vector3[] positions, Vector3 offset)
        {
            if (positions != null) { }
            else throw new System.ArgumentNullException(_ERR_EMPTY_IN_POSITIONS);

            _countPositions = positions.Length + 1;
            _line.positionCount = _countPositions;

            _line.SetPosition(_FIRST_INDEX, startPosition);
            for (int i = _FIRST_INDEX; i < positions.Length; ++i)
                _line.SetPosition(i + 1, positions[i] + offset);
        }


        #region Error values
        private const string _ERR_MATERIAL_NULL = "Error. Input material not exist";
        private const string _ERR_NULL_OR_EMPTY_NAME = "Error. Input name is null or empty";
        private const string _ERR_EMPTY_POSITIONS_COUNT = "Error. Positions count is equel 0";
        private const string _ERR_EMPTY_IN_POSITIONS = "Error. Input positions at redraw is null";
        #endregion
    }
}
