using System;
using Camera;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject genSettingsPanel;
        [SerializeField] private GameObject camSettingsPanel;

        [Header("Terrain Gen")] 
        [SerializeField] private TextMeshProUGUI filterAlphaText;
        [SerializeField] private TextMeshProUGUI meshSizeText;
        [SerializeField] private TextMeshProUGUI meshScaleText;
        
        [Header("Cam Settings")]
        [SerializeField] private TextMeshProUGUI rotSpeedText;
        [SerializeField] private TextMeshProUGUI zoomSpeedText;

        [Header("Scripts")]
        [SerializeField] private TerrainGenerator terrainGenerator;
        [SerializeField] private CameraController cameraController;

        [Space, SerializeField] private TextMeshProUGUI versionText;

        private void Start()
        {
            versionText.text = $"v. {Application.version}";
        }
        
        public void GenSettingsB()
        {
            genSettingsPanel.SetActive(!genSettingsPanel.activeSelf);
            camSettingsPanel.SetActive(false);
        }

        public void CamSettingsB()
        {
            camSettingsPanel.SetActive(!camSettingsPanel.activeSelf);
            genSettingsPanel.SetActive(false);
        }

        public void GenerateB()
        {
            terrainGenerator.GenerateTerrain();
        }

        public void MeanInput(string value)
        {
            var number = Convert.ToDouble(value);
            if (number <= 0)
            {
                Debug.Log("Error Value must be greater than zero");
            }
            else
            {
                terrainGenerator.mean = number;
            }
        }
        
        public void StdDevInput(string value)
        {
            var number = Convert.ToDouble(value);
            if (number <= 0)
            {
                Debug.Log("Error Value must be greater than zero");
            }
            else
            {
                terrainGenerator.stdDev = number;
            }
        }

        public void AlphaSliderInput(float value)
        {
            terrainGenerator.alpha = value;
            filterAlphaText.text = $"{value:0.000}";
        }

        public void MeshSizeInput(float value)
        {
            switch (value)
            {
                case 0:
                    terrainGenerator.mapSize = 16;
                    meshSizeText.text = "16 x 16";
                    break;
                case 1:
                    terrainGenerator.mapSize = 32;
                    meshSizeText.text = "32 x 32";
                    break;
                case 2:
                    terrainGenerator.mapSize = 64;
                    meshSizeText.text = "64 x 64";
                    break;
                case 3:
                    terrainGenerator.mapSize = 128;
                    meshSizeText.text = "128 x 128";
                    break;
                case 4:
                    terrainGenerator.mapSize = 256;
                    meshSizeText.text = "256 x 256";
                    break;
                case 5:
                    terrainGenerator.mapSize = 512;
                    meshSizeText.text = "512 x 512";
                    break;
                case 6:
                    terrainGenerator.mapSize = 1024;
                    meshSizeText.text = "1024 x 1024";
                    break;
            }
        }
        
        public void MeshScaleInput(float value)
        {
            switch (value)
            {
                case 0:
                    terrainGenerator.scale = 0.125f;
                    meshScaleText.text = "0.125";
                    break;
                case 1:
                    terrainGenerator.scale = 0.25f;
                    meshScaleText.text = "0.25";
                    break;
                case 2:
                    terrainGenerator.scale = 0.5f;
                    meshScaleText.text = "0.5";
                    break;
                case 3:
                    terrainGenerator.scale = 1;
                    meshScaleText.text = "1";
                    break;
            }
        }

        public void CamRotSpeedInput(float value)
        {
            cameraController.rotationSpeed = value;
            rotSpeedText.text = $"{value:0.000}";
        }
        
        public void CamZoomSpeedInput(float value)
        {
            cameraController.zoomSpeed = value;
            zoomSpeedText.text = $"{value:0.000}";
        }

        public void WireframeToggle(bool value)
        {
            terrainGenerator.ToggleWireframe(value);
        }
        
    }
}