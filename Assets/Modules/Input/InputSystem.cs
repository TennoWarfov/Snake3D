namespace Modules.Input
{
    public class InputSystem
    {
        public InputSystem_Actions InputActions;

        public InputSystem()
        {
            InputActions = new InputSystem_Actions();
            InputActions.Enable();
        }

        public void Dispose()
        {
            InputActions.Disable();
            InputActions.Dispose();
            InputActions = null;
        }
    }
}
