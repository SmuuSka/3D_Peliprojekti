using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveLogic : MonoBehaviour
{
    [SerializeField] Camera cam;


    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    private void PerformMovement()
    {    
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }

    private void PerformRotation()
    {
        playerRb.MoveRotation(playerRb.rotation * Quaternion.Euler(rotation));
        if (velocity != Vector3.zero)
        {
            playerRb.MovePosition(playerRb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

}
