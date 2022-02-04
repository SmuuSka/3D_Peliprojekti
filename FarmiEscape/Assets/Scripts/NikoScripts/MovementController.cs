using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed;
    public float horizontalInput, verticalInput;



    private void Update()
    {
        WASDMovement();
        MouseTurn();
    }

    private void WASDMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        if (verticalInput > 0 || horizontalInput > 0)
        {
            transform.position += new Vector3(horizontalInput, 0, verticalInput) * movementSpeed * Time.deltaTime;
        }
        else if (verticalInput < 0 || horizontalInput < 0)
        {
            transform.position -= new Vector3(-horizontalInput, 0, -verticalInput) * movementSpeed * Time.deltaTime;
        }
    }


    private void MouseTurn()
    {

    }

}
