using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    [CreateAssetMenu(menuName = "NSS/Weapon Profile", fileName = "WeaponProfile")]
    public class WeaponProfile : ScriptableObject
    {
        public float fireInterval = 1;
        public uint damage = 10;
        public GameObject projectilePrefab;
        public uint projectileVelocity = 500;
    }
}
