using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MoveScripts))]
public class MoveUserInput_2 : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float sensitivity = 1.5f;
    //[SerializeField][Range(0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField][Range(0f, 0.5f)] float moveSmoothTimeCamera = 0.03f;
    private Camera playerCam;
    private Rigidbody playerRB;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelo = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelo = Vector2.zero;



    private MoveScripts moveLogic;
    private Animator playerAnimator;

    private float cameraPitch = 0.0f;

    private bool lockCursor = true;
    private bool freeze = true;


    private void Start()
    {
        // Luodaan yhteys näihin peliobjektin komponentteihin
        playerAnimator = GetComponent<Animator>();
        playerCam = GetComponentInChildren<Camera>();
        moveLogic = GetComponent<MoveScripts>();
        playerRB = GetComponent<Rigidbody>();

    }

    private void Update()
    {

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        HandleMovement();
        HandleRotation();
        MoveAnimations(HandleMovement());

        freeze = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature|jump");

    }

    private float HandleMovement()
    {
        // Tässä luetaan käyttäjän näppäimistön syötteet x ja z akselilla - liikkumiselle ja tallennetaan se muuttujiin
        float _zMov = Input.GetAxisRaw("Vertical");

        if (!freeze)
        {
            if (_zMov > 0)
            {
                transform.Translate(Vector3.forward * _zMov * walkSpeed * Time.deltaTime);

            }
            else if (_zMov < 0 && _zMov != 0)
            {
                _zMov = -0.5f;
                transform.Translate(Vector3.forward * _zMov * walkSpeed * Time.deltaTime);
            }
            
        }
        return _zMov;
    }
    private void HandleRotation()
    {
        // Tässä luetaan käyttäjän hiiren syötteet x ja y akselilla-liikkumiselle ja lokaaleihin muuttujiin
        float _xRot = Input.GetAxisRaw("Mouse X");
        float _yRot = Input.GetAxisRaw("Mouse Y");

        Vector2 targetMouseDelta = new Vector2(_xRot, _yRot);

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelo, moveSmoothTimeCamera);

        cameraPitch -= currentMouseDelta.y * sensitivity;

        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        playerCam.transform.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * sensitivity);
    }

    private void MoveAnimations(float moveDir)
    {
        // Jos pelaajan liike on erisuuri kuin 0 pyöritetään juoksemis animaatiota, jos pelaajan syöte on 0 niin ei tapahdu mitään
        if (moveDir > 0)
        {
            playerAnimator.SetInteger("Run", 1);

            if (Input.GetKeyDown(KeyCode.Space) && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature|run_jump"))
            {
                playerAnimator.SetTrigger("RunJump");
            }
        }
        else if (moveDir < 0)
        {
            playerAnimator.SetInteger("Walk", 1);
        }
        else
        {
            playerAnimator.SetInteger("Run", 0);
            playerAnimator.SetInteger("Walk", 0);

            if (Input.GetKeyDown(KeyCode.Space) && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Armature|jump"))
            {
                playerAnimator.SetTrigger("Jump");
                
                
            }
            
        }
    }

    private IEnumerator PerformJump()
    {
        yield return null;
    }
}
