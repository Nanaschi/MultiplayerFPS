using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    public ItemInfo _itemInfo;
    [SerializeField]
    private GameObject _itemGameObject;

    protected ItemInfo ItemInfo => _itemInfo;

    public GameObject ItemGameObject => _itemGameObject;

    public abstract void Use();
}
