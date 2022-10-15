using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class ItemBase : MonoBehaviour
    {
        public virtual void ApplyToCharacter(Character character)
        {
            if (ItemManager.IsCreated)
            {
                ItemManager.Instance.OnItemApplied(this);
            }
            Destroy(gameObject);
        }
    }
}
