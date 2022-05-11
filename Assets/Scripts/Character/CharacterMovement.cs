using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField, Range(0, 5)]
        private float moveDuration = 0.1f;

        [SerializeField]
        private AnimationCurve moveCurve = AnimationCurve.Linear(0, 0, 1, 1);

        public bool IsMoving { get; private set; } = false;

        private Character character;
        private Timer timer;
        private Transform startPoint;
        private Transform endPoint;

        private void Awake()
        {
            character = GetComponent<Character>();
            timer = new Timer();
        }

        private void Update()
        {
            UpdateMove();
        }

        public bool TryMove(EMoveDirection direction)
        {
            if(IsMoving)
            {
                return false;
            }

            // Just move instantaneously
            if (moveDuration <= 0)
            {
                return TryMove_Implementation(direction);
            }

            // Setup to move smoothly
            startPoint = character.StayingBlock.transform;

            // When the move fails
            if (!TryMove_Implementation(direction))
            {
                startPoint = null;
                return false;
            }

            endPoint = character.StayingBlock.transform;
            timer.Reset(moveDuration);
            IsMoving = true;

            return true;
        }

        protected virtual bool TryMove_Implementation(EMoveDirection direction)
        {
            FieldBlock targetBlock = FieldManager.Instance.GetAdjacentBlock(character.StayingBlock, direction);
            if (targetBlock == null)
            {
                return false;
            }

            bool shouldSetPosition = moveDuration <= 0;
            return TryEnterBlock(targetBlock, shouldSetPosition);
        }

        public bool TryEnterBlock(FieldBlock block, bool setCharacterPosition = true)
        {
            if (block == null)
            {
                return false;
            }

            if (block.TryCharacterEnter(character))
            {
                character.StayingBlock = block;
                if (setCharacterPosition)
                {
                    transform.position = block.transform.position;
                }
                return true;
            }

            return false;
        }

        private void UpdateMove()
        {
            if(!IsMoving
                || startPoint == null 
                || endPoint == null
                || Time.timeScale == 0)
            {
                return;
            }

            timer.Step();
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, moveCurve.Evaluate(timer.Progress));

            if (timer.IsComplete)
            {
                IsMoving = false;
                startPoint = null;
                endPoint = null;
            }
        }

        public void ResetStatus()
        {
            IsMoving = false;
            startPoint = null;
            endPoint = null;
            timer.Reset(moveDuration);
        }
    }
}
