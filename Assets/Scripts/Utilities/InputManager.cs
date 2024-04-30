using System;
using UnityEngine;

namespace Utilities
{
    [DefaultExecutionOrder(-1)]
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        
        private InputActions _input;

        public event Action OnGenerate;
        
        public Vector2 ScrollInput { get; private set; }

        private void Awake()
        {
            Instance = this;
            _input = new InputActions();
        }

        private void Start()
        {
            _input.Terrain.Generate.performed += _ => OnGenerate?.Invoke();
        }

        private void Update()
        {
            ScrollInput = _input.Camera.Zoom.ReadValue<Vector2>();
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