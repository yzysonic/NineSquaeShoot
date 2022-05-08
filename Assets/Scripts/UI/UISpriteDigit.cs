using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class UIDigitSprite : PooledMonoBehavior
    {
        private Sprite sprite;

        private void Awake()
        {
            sprite = GetComponent<Sprite>();
        }

        public void SetValue(char value)
        {

        }
    }
}
