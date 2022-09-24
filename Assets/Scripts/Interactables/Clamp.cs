using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MonoBehaviour
{
    public GameObject buretPivot;

    private void OnTriggerEnter(Collider other)
    {
        var buret = other.GetComponentInParent<Buret>();

        if (buret)
        {
            if (buret.buretInteractable.attachedToHand)
                buret.transform.SetParent(buretPivot.transform, false);
        }
    }
}
