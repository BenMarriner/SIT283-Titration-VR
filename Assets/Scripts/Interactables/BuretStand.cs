using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuretStand : MonoBehaviour
{
    private GameObject _buretPivot;
    private Collider _clampCollider;

    public GameObject clamp;

    // Start is called before the first frame update
    void Start()
    {
        _buretPivot = clamp.transform.Find("BuretPivot").gameObject;
        _clampCollider = clamp.GetComponent<Collider>();

        foreach (var collider in GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(GetComponentInChildren<Collider>(), collider);
        }

        foreach (var rigidBody in GetComponentsInChildren<Rigidbody>())
        {
            rigidBody.centerOfMass = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
