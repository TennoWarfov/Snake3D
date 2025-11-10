using Modules.Input;
using Zenject;

namespace Modules.DI
{
    public class PrefabsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallInputSystem();
        }

        private void InstallInputSystem()
        {
            InputSystem input = new();
            Container.Bind<InputSystem>().FromInstance(input).AsSingle();
        }
    }
}
