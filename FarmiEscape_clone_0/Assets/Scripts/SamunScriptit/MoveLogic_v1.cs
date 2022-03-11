using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveLogic_v1 : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;

    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    private void PerformMove()
    {
        if (velocity != Vector3.zero)
        {
            playerRb.MovePosition(playerRb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void FixedUpdate()
    {
        PerformMove();
    }


}
