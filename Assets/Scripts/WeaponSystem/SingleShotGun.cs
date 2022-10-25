using System;
using Interfaces;
using Photon.Pun;
using ScriptableObjects;
using UnityEngine;

namespace WeaponSystem
{
    [RequireComponent(typeof(PhotonView))]
    public class SingleShotGun : Gun
    {
        [SerializeField] private Camera _camera;


        private PhotonView _photonView;
        private const float OffsetToAvoidClipping = .001f;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public override void Use()
        {
            print($"Using gun {ItemInfo.ItemName}");
            Shoot();
        }

        private void Shoot()
        {
            Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            ray.origin = _camera.transform.position;
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                print($"We hit {raycastHit.collider.gameObject.name}");

                raycastHit.collider.GetComponent<IDamageable>()
                    ?.TakeDamage(((GunInfo) ItemInfo).DamageAmount);

                _photonView.RPC(nameof(RPC_Shoot), RpcTarget.All, raycastHit.point,
                    raycastHit.normal);
            }
        }

        [PunRPC]
        void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
        {
            print(hitPosition);
            Instantiate(BulletImpactPrefab, hitPosition + hitNormal * OffsetToAvoidClipping,
                Quaternion.LookRotation(hitNormal, Vector3.up) *
                BulletImpactPrefab.transform.rotation);
        }
    }
}