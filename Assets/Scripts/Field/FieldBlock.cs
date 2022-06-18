using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class FieldBlock : MonoBehaviour, IDamageSender, IDamageReceiver
    {
        private enum EGroundLightState
        {
            None,
            Player,
            Enemy,
            Both
        }

        [SerializeField]
        private SpriteRenderer groundLightRenderer;

        [SerializeField]
        private Color playerGroundLightColor = Color.white;

        [SerializeField]
        private Color enemyGroundLightColor = Color.yellow;

        public int Index { get; set; } = 0;
        public ETeam Team { get; set; } = ETeam.none;
        public uint RowIndex { get; set; } = 0;
        public Character StayingCharacter { get; private set; }
        public bool CanTransferDamage => StayingCharacter && !StayingCharacter.IsInvincible;

        private readonly Dictionary<Object, DamageInfo> damageReservations = new();

        private readonly HashSet<Projectile> stayingSelfProjectiles = new();

        private EGroundLightState groundLightState = EGroundLightState.None;

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

        public void OnCharacterEnterFinished(Character character)
        {
            if (character && character == StayingCharacter)
            {
                ApplyDamageFormReservations(character);
            }
        }

        public void CharacterExit()
        {
            StayingCharacter = null;
            if (FieldManager.IsCreated)
            {
                FieldManager.Instance.OnCharacterExitedBlock(this);
            }
        }

        public void ReserveDamageTransferring(Object owner, DamageInfo damageInfo)
        {
            if (damageReservations.Count == 0)
            {
                SetGroundLight(Team == ETeam.enemy, true);
            }
            damageReservations.Add(owner, damageInfo);
        }

        public void CancelDamageTransferring(Object owner)
        {
            damageReservations.Remove(owner);
            if (damageReservations.Count == 0)
            {
                SetGroundLight(Team == ETeam.enemy, false);
            }
        }

        private void OnCharacterEntered(Character character)
        {
            if (character.Movement && !character.Movement.IsMovingVertically)
            {
                ApplyDamageFormReservations(character);
            }
            FieldManager.Instance.OnCharacterEnteredBlock(this);
        }

        private void ApplyDamageFormReservations(Character character)
        {
            foreach (var item in damageReservations)
            {
                (this as IDamageSender).SendDamage(character.gameObject, item.Value, character);
            }

            damageReservations.Clear();
            SetGroundLight(Team == ETeam.enemy, false);
        }

        public void OnSelfProjectileEntered(Projectile projectile)
        {
            if (!projectile)
            {
                return;
            }
            if (stayingSelfProjectiles.Count == 0)
            {
                SetGroundLight(projectile.Team == ETeam.player, true);
            }
            stayingSelfProjectiles.Add(projectile);
        }

        public void OnSelfProjectileExited(Projectile projectile)
        {
            if (!projectile)
            {
                return;
            }
            stayingSelfProjectiles.Remove(projectile);
            if (stayingSelfProjectiles.Count == 0)
            {
                SetGroundLight(projectile.Team == ETeam.player, false);
            }
        }

        private void SetGroundLight(bool isPlayer, bool enabled)
        {
            EGroundLightState settingState = isPlayer ? EGroundLightState.Player : EGroundLightState.Enemy;
            if(enabled)
            {
                groundLightState |= settingState;
            }
            else
            {
                groundLightState &= ~settingState;
            }

            if (groundLightRenderer)
            {
                switch (groundLightState)
                {
                    case EGroundLightState.None:
                        groundLightRenderer.color = Color.clear;
                        break;

                    case EGroundLightState.Player:
                        groundLightRenderer.color = playerGroundLightColor;
                        break;

                    case EGroundLightState.Enemy:
                        groundLightRenderer.color = enemyGroundLightColor;
                        break;

                    case EGroundLightState.Both:
                        groundLightRenderer.color = playerGroundLightColor * enemyGroundLightColor;
                        break;
                }

                groundLightRenderer.enabled = settingState != EGroundLightState.None;
            }
        }
    }
}
