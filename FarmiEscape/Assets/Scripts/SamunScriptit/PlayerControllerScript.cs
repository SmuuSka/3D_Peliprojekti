using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerLogicScript))]
public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.5f;
    [SerializeField]
    private float sensitivity = 3f;

    private PlayerLogicScript motor;
    private Animator playerAnimator;
    

    private void Start()
    {
        motor = GetComponent<PlayerLogicScript>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
        

        Debug.Log("Velocity: " + _velocity);

        motor.Move(_velocity);



        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * sensitivity;

        motor.Rotate(_rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * sensitivity;

        motor.RotateCamera(_cameraRotation);









        Run(_velocity);

    }

    private void Run(Vector3 _playerVelocity)
    {
        if (_playerVelocity.z != 0)
        {
            playerAnimator.SetFloat("Run",1);
        }
        else
        {
            playerAnimator.SetFloat("Run", 0);
        }
    }


}
