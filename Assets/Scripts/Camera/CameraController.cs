using System;
using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        private InputActions _input;
        
        public Transform pivotPoint;
        public float rotationSpeed = 10.0f;
        public float zoomSpeed = 2.0f;
        public float minDistance = 2.0f;
        public float maxDistance = 20.0f;

        private float _currentDistance;
        private void Awake()
        {
            _input = new InputActions();
        }

        private void Start()
        {
            _currentDistance = Vector3.Distance(transform.position, pivotPoint.position);
        }
        
        private void Update()
        {
            // Rotation
            transform.RotateAround(pivotPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);

            // Zoom
            float scrollInput = _input.Camera.Zoom.ReadValue<Vector2>().y;
            _currentDistance -= scrollInput * zoomSpeed;
            _currentDistance = Mathf.Clamp(_currentDistance, minDistance, maxDistance);

            Vector3 direction = (transform.position - pivotPoint.position).normalized;
            transform.position = pivotPoint.position + direction * _currentDistance;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }
    }
}