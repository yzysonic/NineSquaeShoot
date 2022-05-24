using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace NSS
{
    [CreateAssetMenu(menuName = "NSS/GlobalAsset", fileName = "GlobalAsset")]
    public class GlobalAsset : ScriptableObject
    {
        public AudioMixerGroup SEMixerGroup;

        public static GlobalAsset Instance { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Load()
        {
            Instance = (GlobalAsset)Resources.Load("GlobalAsset");
        }
    }
}
