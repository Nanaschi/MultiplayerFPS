using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = nameof(ScriptableObjects) + "/" + nameof(GunInfo), fileName = nameof(GunInfo))]
    public class GunInfo: ItemInfo
    {
        [SerializeField] private float _damageAmount;

        public float DamageAmount => _damageAmount;
    }
}