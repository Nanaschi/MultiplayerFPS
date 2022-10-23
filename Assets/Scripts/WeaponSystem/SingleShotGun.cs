using Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace WeaponSystem
{
    public class SingleShotGun: Gun
    {
        [SerializeField] private Camera _camera;
        public override void Use()
        {
            print($"Using gun {ItemInfo.ItemName}" );
            Shoot();
        }

        private void Shoot()
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            ray.origin = _camera.transform.position;
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                print($"We hit {raycastHit.collider.gameObject.name}");
                
                raycastHit.collider.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)ItemInfo).DamageAmount);
            }
        }
    }
}