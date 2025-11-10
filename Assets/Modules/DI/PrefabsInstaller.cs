using Modules.Input;
using Modules.SceneController;
using UnityEngine;
using Zenject;

namespace Modules.DI
{
    public class PrefabsInstaller : MonoInstaller
    {
        [SerializeField]
        private Bootstrap.Bootstrap bootstrapPrefab;

        public override void InstallBindings()
        {
            InstallInputSystem();
            InstallSceneLoader();
            InstallBootstrap();
        }

        private void InstallInputSystem()
        {
            InputSystem input = new();
            Container.Bind<InputSystem>().FromInstance(input).AsSingle();
        }

        private void InstallSceneLoader()
        {
            SceneLoader loader = new();
            Container.Bind<SceneLoader>().FromInstance(loader).AsSingle();
        }

        private void InstallBootstrap()
        {
            Container.InstantiatePrefab(bootstrapPrefab);
        }
    }
}
