using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Components
{
    public class PointsGrid : MonoBehaviour, IEnumerable<Vector3>
    {
        [SerializeField, Min(1)] private int _rows = 1;
        [SerializeField, Min(1)] private int _columns = 1;
        [SerializeField] private Vector2 _spacing = Vector2.one;

        public int Rows => _rows;
        public int Columns => _columns;

        public Vector3 this[int row, int column] => GetWorldPointPosition(row, column);

        private Vector2 GetWorldPointPosition(int row, int column)
        {
            if (row < 0 || row >= _rows || column < 0 || column >= _columns)
            {
                throw new ArgumentOutOfRangeException();
            }

            var topLeftCorner = new Vector2(-((_columns - 1) * _spacing.x * 0.5f), 0f);
            var pointPosition = (Vector2)transform.position + topLeftCorner + Vector2.Scale(new Vector2(column, -row), _spacing);

            return pointPosition;
        }

        public IEnumerator<Vector3> GetEnumerator()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    yield return GetWorldPointPosition(i, j);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            DrawPoints();
        }

        private void DrawPoints()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    Gizmos.DrawSphere(GetWorldPointPosition(i, j), 0.1f);
                }
            }
        }
#endif
    }
}