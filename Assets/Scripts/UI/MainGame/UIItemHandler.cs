using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class UIItemHandler : MonoBehaviour
    {
        [SerializeField]
        private UIItem attackItemUI;

        [SerializeField]
        private UIItem speedItemUI;

        public void CountupAttackItem()
        {
            if (attackItemUI)
            {
                attackItemUI.Countup();
            }
        }

        public void CountupSpeedItem()
        {
            if (speedItemUI)
            {
                speedItemUI.Countup();
            }
        }

        public void ResetItemCount()
        {
            if (attackItemUI)
            {
                attackItemUI.SetCount(0);
            }

            if (speedItemUI)
            {
                speedItemUI.SetCount(0);
            }
        }
    }
}
