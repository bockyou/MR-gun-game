using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20;
 
    void Start()
    {
        var velocity = speed * -GameObject.Find("RightHandAnchor").transform.right;
       
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddForce(velocity, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    { 
        other.SendMessage("OnHitBullet");
        Destroy(gameObject);
    }
}
    
