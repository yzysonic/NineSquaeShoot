using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UIPauseButton : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            if (button)
            {
                button.onClick.AddListener(OnButtonPressed);
            }
        }

        private void OnButtonPressed()
        {
            GameUIManager.Instance.SetPauseActive(true);
        }
    }
}
