using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaker : MonoBehaviour
{
    public List<GameObject> ignoredCollisions;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var obj in ignoredCollisions)
        {
            if (obj.TryGetComponent<Collider>(out var collider))
                Physics.IgnoreCollision(collider, transform.GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Liquid>())
        {
            Physics.IgnoreCollision(collision.collider, transform.GetComponent<Collider>());
        }
    }
}
