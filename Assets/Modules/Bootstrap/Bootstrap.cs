using System;
using Modules.Input;
using Modules.SceneController;
using UnityEngine;
using Zenject;

namespace Modules.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [Inject]
        private SceneLoader _sceneLoader;

        [Inject]
        private InputSystem _inputSystem;

        private async void Start()
        {
            try
            {
                await _sceneLoader.LoadSceneAsync("_Main");
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString()); // TODO handle exception
            }
        }

        private void OnDestroy()
        {
            _inputSystem?.Dispose();
            _inputSystem = null;
        }
    }
}
