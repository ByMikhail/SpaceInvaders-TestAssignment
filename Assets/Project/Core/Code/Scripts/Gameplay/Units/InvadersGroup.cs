using System;
using SpaceInvaders.Foundation.TinyGameplayFramework;
using SpaceInvaders.Gameplay.Components;
using SpaceInvaders.Tools.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceInvaders.Gameplay.Units
{
    [RequireComponent(typeof(PointsGrid), typeof(Rigidbody2D))]
    public class InvadersGroup : GameplayUnitsGroup<Invader>
    {
        [SerializeField] private BoxCollider2D _movementConstrainingCollider;
        [SerializeField] private Invader[] _invadersPrefabs;
        [SerializeField] private float _horizontalMovementSpeed = 1f;
        [SerializeField] private float _verticalOffset = 0.5f;

        private Transform _transform;
        private Rigidbody2D _rigidbody;
        private PointsGrid _spawnPoints;
        private Invader[,] _invadersGrid;
        private RenderersBoundsComposer _invadersBoundsComposer;
        private float _horizontalMovementDirection = 1f;

        #region Unity lifecycle

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
            _spawnPoints = GetComponent<PointsGrid>();
            _invadersBoundsComposer = new RenderersBoundsComposer();
        }

        private void Start()
        {
            CreateInvadersOnGrid();
            UpdateMovementConstrainingCollider();
        }

        private void FixedUpdate()
        {
            var newPosition = _rigidbody.position + Vector2.right * (_horizontalMovementSpeed * _horizontalMovementDirection * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            var newPosition = _rigidbody.position + Vector2.down * _verticalOffset;
            _rigidbody.position = newPosition;

            _horizontalMovementDirection *= -1;
        }

        #endregion

        public void Fire()
        {
            var randomInvader = GetRandomInvaderFromBottom();

            if (randomInvader)
            {
                randomInvader.Fire();
            }
        }

        private void UpdateMovementConstrainingCollider()
        {
            var invadersBounds = _invadersBoundsComposer.Compose();

            Bounds groupBoundsInWorldSpace = invadersBounds.HasValue
                ? invadersBounds.Value
                : new Bounds(_movementConstrainingCollider.transform.position, Vector3.one);

            var groupBoundsInLocalSpace = groupBoundsInWorldSpace.ToLocalSpace(_movementConstrainingCollider.transform);

            _movementConstrainingCollider.size = groupBoundsInLocalSpace.size;
            _movementConstrainingCollider.offset = groupBoundsInLocalSpace.center;
        }

        private void CreateInvadersOnGrid()
        {
            _invadersGrid = new Invader[_spawnPoints.Rows, _spawnPoints.Columns];

            for (int i = 0, n = _spawnPoints.Rows; i < n; i++)
            {
                for (int j = 0, m = _spawnPoints.Columns; j < m; j++)
                {
                    var invader = CreateInvader(_spawnPoints[i, j]);

                    _invadersGrid[i, j] = invader;
                    AddUnit(invader);
                }
            }
        }

        private Invader CreateInvader(Vector3 position)
        {
            var invaderPrefabIndex = Random.Range(0, _invadersPrefabs.Length);
            var invaderPrefab = _invadersPrefabs[invaderPrefabIndex];

            return Instantiate(invaderPrefab, position, Quaternion.identity, _transform);
        }


        private Invader GetRandomInvaderFromBottom()
        {
            Invader result = null;

            int columnsCount = _invadersGrid.GetLength(1);
            int column = Random.Range(0, columnsCount);
            int step = Random.Range(1, columnsCount);

            for (int i = 0; i < columnsCount; i++)
            {
                var invader = FindBottomAliveInvader(column);

                if (invader)
                {
                    result = invader;
                    break;
                }

                column = (column + step) % columnsCount;
            }


            return result;
        }

        private Invader FindBottomAliveInvader(int column)
        {
            Invader result = null;

            for (int row = _invadersGrid.GetLength(0) - 1; row >= 0; row--)
            {
                var invader = _invadersGrid[row, column];

                if (invader && !invader.IsKilled)
                {
                    result = invader;
                    break;
                }
            }

            return result;
        }

        #region Event listeners

        protected override void HandleOnUnitAdded(Invader invader)
        {
            _invadersBoundsComposer.IncludeRenderersOf(invader.gameObject);
            invader.OnKilled += Invader_OnKilled;
        }

        protected override void HandleOnUnitRemoved(Invader invader)
        {
            _invadersBoundsComposer.ExcludeRenderersOf(invader.gameObject);
            invader.OnKilled -= Invader_OnKilled;
        }

        protected override void HandleOnLastUnitRemoved(Invader _)
        {
            DestroyItself();
        }

        private void Invader_OnKilled(object sender, EventArgs _)
        {
            var invader = (Invader)sender;

            _invadersBoundsComposer.ExcludeRenderersOf(invader.gameObject);
            invader.OnKilled -= Invader_OnKilled;
            UpdateMovementConstrainingCollider();
        }

        #endregion
    }
}