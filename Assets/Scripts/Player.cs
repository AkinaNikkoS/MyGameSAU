using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float BounceSpeed;
    public Rigidbody Rigidbody;
    public Transform Transform; 

    public void Bounce()
    {
        if (Transform.position.y < 0.162) Rigidbody.velocity = new Vector3(0, BounceSpeed, 0);
    }

}
