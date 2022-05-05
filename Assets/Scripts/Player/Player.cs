using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class Player : Character
    {
        protected override void Awake()
        {
            base.Awake();
            Team = ETeam.player;
        }
    }
}
