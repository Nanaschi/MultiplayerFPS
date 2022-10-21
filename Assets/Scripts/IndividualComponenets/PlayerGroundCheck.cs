using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public event Action<bool> OnGroundStanding;


    private void OnTriggerEnter(Collider other)
    {
        if(other == TryGetComponent(out PlayerController playerController)) return;
        OnGroundStanding?.Invoke(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other == TryGetComponent(out PlayerController playerController)) return;
        OnGroundStanding?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == TryGetComponent(out PlayerController playerController)) return;
        OnGroundStanding?.Invoke(false);
    }
}
