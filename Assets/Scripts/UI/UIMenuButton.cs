using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NSS
{
    public class UIMenuButton : Button
    {
        public UIMenu OwnerMenu { get; set; }

        protected override void Awake()
        {
            onClick.AddListener(OnPressed);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (OwnerMenu)
            {
                OwnerMenu.OnButtonHovered(this);
            }
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            if (OwnerMenu)
            {
                OwnerMenu.OnButtonSelected(this);
            }
        }

        private void OnPressed()
        {
            if (OwnerMenu)
            {
                OwnerMenu.OnButtonPressed(this);
            }
        }
    }
}
