using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NSS
{
    public class ScreenBoard : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                var projectile = collision.gameObject.GetComponent<Projectile>();
                if (projectile != null)
                {
                    projectile.IsUsing = false;
                }
            }
        }
    }
}
