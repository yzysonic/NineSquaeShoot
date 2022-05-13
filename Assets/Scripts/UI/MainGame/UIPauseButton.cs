using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NSS
{
    public class UIPauseButton : MonoBehaviour
    {
        [SerializeField]
        private UIPause pauseUI;

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
            if (pauseUI)
            {
                pauseUI.gameObject.SetActive(true);
            }
        }
    }
}
