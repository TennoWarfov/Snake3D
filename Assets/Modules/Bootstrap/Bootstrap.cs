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
                await _sceneLoader.LoadSceneAsync("Main");
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString()); // TODO handle exception
            }
        }
    }
}
