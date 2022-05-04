using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Enemy : Character
    {
        [SerializeField]
        private float moveInterval = 1;

        public float MoveInterval => moveInterval;
    }
}
