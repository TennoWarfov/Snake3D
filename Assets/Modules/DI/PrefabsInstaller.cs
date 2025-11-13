using Modules.Input;
using Modules.Player;
using Modules.SceneController;
using UnityEngine;
using Zenject;

namespace Modules.DI
{
    public class PrefabsInstaller : MonoInstaller
    {
        [Header("Bootstrap")]
        [SerializeField]
        private Bootstrap.Bootstrap bootstrapPrefab;

        [Header("Player")]
        [SerializeField]
        private PlayerController playerPrefab;

        public override void InstallBindings()
        {
            InstallInputSystem();
            InstallSceneLoader();
            InstallBootstrap();
            InstallPlayer();
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

        private void InstallPlayer()
        {
            var player = Container.InstantiatePrefab(playerPrefab);
            player.transform.position = new Vector3(0f, 1f, 0f);
            Container
                .Bind<PlayerController>()
                .FromInstance(player.GetComponent<PlayerController>())
                .AsSingle();
        }
    }
}
