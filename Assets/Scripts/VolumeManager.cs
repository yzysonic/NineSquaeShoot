using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace NSS
{
    public class VolumeManager : Singleton<VolumeManager>
    {
        [SerializeField]
        private Volume defaultVolume;

        [SerializeField]
        private Volume playerCounterEffectVolume;

        public Volume DefaultVolume => defaultVolume;
        public Volume PlayerCounterEffectVolume => playerCounterEffectVolume;
    }
}
