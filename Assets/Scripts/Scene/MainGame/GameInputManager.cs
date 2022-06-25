using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class GameInputManager : Singleton<GameInputManager>
    {
        public GameInput Input { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Input = new GameInput();
        }
    }
}
