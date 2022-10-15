using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class UIItem : MonoBehaviour
    {
        [SerializeField]
        private UISpriteNumber number;

        private int count = 0;

        public void SetCount(int num)
        {
            bool shouldDisplay = num > 0;
            if (shouldDisplay != gameObject.activeSelf)
            {
                gameObject.SetActive(shouldDisplay);
            }

            if (number)
            {
                number.Value = num;
            }

            count = num;
        }

        public void Countup()
        {
            if (number)
            {
                SetCount(count + 1);
            }
        }
    }
}
