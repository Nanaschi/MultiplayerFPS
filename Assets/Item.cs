using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemInfo _itemInfo;
    [SerializeField]
    private GameObject _itemGameObject;

    public ItemInfo ItemInfo => _itemInfo;

    public GameObject ItemGameObject => _itemGameObject;
}
