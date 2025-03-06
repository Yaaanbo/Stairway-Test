using StairwayTest.Utilities;
using StairwayTest.Utilities.Interface;
using UnityEngine.InputSystem;

namespace StairwayTest.Manager
{
    public class ControlManager : SingletonPersistent<ControlManager>, ILoadExternalClasses
    {
        private const string GAMEPAD_CONTROL_SCHEME_NAME = "Gamepad";

        private PlayerInput playerInput;
        private UIManager uiManager;

        private void Start()
        {
            LoadExternalClassInstance();

            playerInput = this.GetComponent<PlayerInput>();
            playerInput.onControlsChanged += OnControlSchemeChanged;
        }


        private void OnDisable()
        {
            playerInput.onControlsChanged -= OnControlSchemeChanged;
        }

        public void LoadExternalClassInstance()
        {
            uiManager = UIManager.Instance;
        }

        private void OnControlSchemeChanged(PlayerInput _playerInput)
        {
            if(_playerInput.currentControlScheme == GAMEPAD_CONTROL_SCHEME_NAME)
            {
                uiManager.SelectLastSelectedButton();
            }
        }
    }
}
