using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NSS
{
    public class UIInput : MonoBehaviour
    {
        private GameInput.UIActions uiInput;

        private void Awake()
        {
            uiInput = GameInputManager.Instance.Input.UI;
            uiInput.Pause.started += _ => GameUIManager.Instance.TogglePause();
        }

        private void OnEnable()
        {
            uiInput.Enable();
        }

        private void OnDisable()
        {
            uiInput.Disable();
        }
    }
}
