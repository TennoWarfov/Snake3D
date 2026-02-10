using System;
using System.Collections.Generic;
using Modules.AppSignals;
using Supyrb;
using UnityEngine;

namespace Modules.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed = 5;

        [SerializeField]
        private float rotationSpeed = 180;

        [SerializeField]
        private float bodySpeed = 5;

        [SerializeField]
        private int gap = 10;

        [SerializeField]
        private Transform root;

        [SerializeField]
        private GameObject bodyPartPrefab;

        private GlobalSignals.AppInitializedSignal _appInitializedSignal;
        private List<Transform> _bodyParts;
        private List<Vector3> _positionsHistory;
        private float _positionsHistoryClearTime;
        private bool _initialized;
        private Vector3 _point;

        private void Awake()
        {
            Signals.Get(out _appInitializedSignal);
            _appInitializedSignal.AddListener(Initialize);
        }

        private void Initialize()
        {
            _bodyParts = new List<Transform>();
            _positionsHistory = new List<Vector3>();

            root.transform.position = new Vector3(0f, 1f, 0f);

            for (var i = 0; i < 5; i++)
                GrowSnake();

            _initialized = true;
        }

        private void Update()
        {
            if (!_initialized)
                return;

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                GrowSnake();
            }

            root.position += root.forward * (movementSpeed * Time.deltaTime);
            var direction = UnityEngine.Input.GetAxis("Horizontal");
            root.Rotate(Vector3.up, direction * rotationSpeed * Time.deltaTime);

            _positionsHistory.Insert(0, root.position);
            var index = 0;
            var bodyPositionIndex = 0;
            foreach (var bodyPart in _bodyParts)
            {
                bodyPositionIndex = Mathf.Clamp(index * gap, 0, _positionsHistory.Count - 1);
                _point = _positionsHistory[bodyPositionIndex];
                var moveDirection = _point - bodyPart.position;
                bodyPart.position += moveDirection * (bodySpeed * Time.deltaTime);
                bodyPart.LookAt(_point);
                index++;
            }

            _positionsHistoryClearTime += Time.deltaTime;
            var difference = _positionsHistory.Count - bodyPositionIndex;
            if (difference >= gap * 2 && _positionsHistoryClearTime >= 1)
            {
                var countToRemove = difference - gap * 2;
                _positionsHistory.RemoveRange(bodyPositionIndex + gap, countToRemove);
                _positionsHistoryClearTime = 0;
                Debug.LogError(
                    $"Removed starting from index: {bodyPositionIndex + gap}, count {countToRemove}, now {_positionsHistory.Count}"
                );
            }
        }

        private void GrowSnake()
        {
            var part = Instantiate(bodyPartPrefab).transform;
            _bodyParts.Add(part);
        }

        private void OnDestroy()
        {
            _appInitializedSignal.RemoveListener(Initialize);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(_point, .5f);
        }
    }
}
