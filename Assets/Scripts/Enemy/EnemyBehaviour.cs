using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NSS
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float startIdleTime = 0.5f;

        public bool IsWaitEntryPerformance { get; set; } = false;

        private Enemy enemy;
        private Weapon weapon;
        private CharacterMovement movement;
        private Timer moveTimer;
        private Timer startIdleTimer;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
            weapon = GetComponent<Weapon>();
            movement = GetComponent<CharacterMovement>();
            moveTimer = new Timer(enemy ? enemy.MoveInterval : 1);
            startIdleTimer = new Timer(startIdleTime);
        }

        private void Update()
        {
            if (IsWaitEntryPerformance)
            {
                return;
            }

            if (movement && movement.IsMoving)
            {
                // Don't allow any action when moving
                return;
            }

            // Move action
            moveTimer.Step();
            if (moveTimer.IsComplete)
            {
                moveTimer.Reset();
                if (TryMoveRandomly())
                {
                    // If we move successfully then we don't do anything further
                    return;
                }
            }

            // Fire action
            if (startIdleTimer.IsComplete)
            {
                if (weapon && weapon.IsCoolDownComplete)
                {
                    weapon.TryFireOnce();
                }
            }
            else
            {
                startIdleTimer.Step();
            }
            
        }

        private bool TryMoveRandomly()
        {
            if (!movement)
            {
                return false;
            }

            // Make move direction lottery
            List<EMoveDirection> moveLottery = new((int)EMoveDirection.Count);
            for (int i = 0; i < (int)EMoveDirection.Count; i++)
            {
                moveLottery.Add((EMoveDirection)i);
            }

            moveLottery = moveLottery.OrderBy(_ => Random.value).ToList();

            foreach(var dir in moveLottery)
            {
                if(movement.TryMove(dir))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
