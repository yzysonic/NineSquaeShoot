using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class FieldBlock : MonoBehaviour, IDamageSender, IDamageReceiver
    {
        [SerializeField]
        private SpriteRenderer groundLightRenderer;

        public int Index { get; set; } = 0;
        public ETeam Team { get; set; } = ETeam.none;
        public uint RowIndex { get; set; } = 0;
        public Character StayingCharacter { get; private set; }
        public bool CanTransferDamage => StayingCharacter != null;

        private readonly Dictionary<Object, DamageInfo> damageReservations = new();

        public void ReceiveDamage(DamageInfo damageInfo)
        {
            if (StayingCharacter)
            {
                (this as IDamageSender).SendDamage(StayingCharacter.gameObject, damageInfo, StayingCharacter);
            }
        }

        public bool TryCharacterEnter(Character character)
        {
            if (character == null || StayingCharacter != null)
            {
                return false;
            }

            StayingCharacter = character;
            OnCharacterEntered(character);
            return true;
        }

        public void CharacterExit()
        {
            StayingCharacter = null;
            FieldManager.Instance.OnCharacterExitedBlock(this);
        }

        public void ReserveDamageTransferring(Object owner, DamageInfo damageInfo)
        {
            if (damageReservations.Count == 0)
            {
                if (groundLightRenderer)
                {
                    groundLightRenderer.enabled = true;
                }
            }
            damageReservations.Add(owner, damageInfo);
        }

        public void CancelDamageTransferring(Object owner)
        {
            damageReservations.Remove(owner);
            if (damageReservations.Count == 0)
            {
                if (groundLightRenderer)
                {
                    groundLightRenderer.enabled = false;
                }
            }
        }

        private void OnCharacterEntered(Character character)
        {
            foreach (var item in damageReservations)
            {
                (this as IDamageSender).SendDamage(character.gameObject, item.Value, character);
            }

            damageReservations.Clear();
            if (groundLightRenderer)
            {
                groundLightRenderer.enabled = false;
            }

            FieldManager.Instance.OnCharacterEnteredBlock(this);
        }
    }
}
