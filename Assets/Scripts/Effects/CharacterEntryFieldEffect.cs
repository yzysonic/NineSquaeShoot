using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class CharacterEntryFieldEffect : MonoBehaviour
    {
        private void OnAnimationFinished()
        {
            Destroy(gameObject);
        }
    }
}
