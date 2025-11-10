using System;
using Modules.SceneController;
using UnityEngine;
using Zenject;

namespace Modules.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [Inject]
        private SceneLoader _sceneLoader;

        private async void Start()
        {
            try
            {
                Debug.Log("Starting bootstrap");
                await _sceneLoader.LoadSceneAsync("_Main");
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString()); // TODO handle exception
            }
        }
    }
}
