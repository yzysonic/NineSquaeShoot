using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class ItemBase : MonoBehaviour
    {
        [SerializeField]
        private GameObject getEffect;

        public virtual void ApplyToCharacter(Character character)
        {
            if (ItemManager.IsCreated)
            {
                ItemManager.Instance.OnItemApplied(this);
            }

            if (getEffect)
            {
                GameObject effect = Instantiate(getEffect, transform.position + Vector3.back, Quaternion.identity);
                Destroy(effect, 1.0f);
            }

            Destroy(gameObject);
        }
    }
}
