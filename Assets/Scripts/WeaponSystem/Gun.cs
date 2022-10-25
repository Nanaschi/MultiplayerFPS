using UnityEngine;

namespace WeaponSystem
{
    public abstract class Gun: Item
    {
        public abstract override  void Use();

        [SerializeField] private GameObject _bulletImpactPrefab;

        public GameObject BulletImpactPrefab => _bulletImpactPrefab;
    }
}