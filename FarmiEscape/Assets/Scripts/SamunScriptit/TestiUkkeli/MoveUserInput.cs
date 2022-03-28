using UnityEngine;

[RequireComponent(typeof(MoveScripts))]
public class MoveUserInput : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float sensitivity = 1.5f;
    [SerializeField] [Range(0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0f, 0.5f)] float moveSmoothTimeCamera = 0.03f;
    private Camera playerCam;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelo = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelo = Vector2.zero;



    private MoveScripts moveLogic;
    private Animator playerAnimator;

    private float cameraPitch = 0.0f;

    private bool lockCursor = true;


    private void Start()
    {
        // Luodaan yhteys näihin peliobjektin komponentteihin
        playerAnimator = GetComponent<Animator>();
        playerCam = GetComponentInChildren<Camera>();
        moveLogic = GetComponent<MoveScripts>();

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

    }

    private void HandleMovement()
    {
        // Tässä luetaan käyttäjän näppäimistön syötteet x ja z akselilla - liikkumiselle ja tallennetaan se muuttujiin
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector2 targetDirection = new Vector2(_xMov, _zMov);
        targetDirection.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDirection, ref currentDirVelo, moveSmoothTime);

        Vector3 _velocity = (transform.forward * _zMov + transform.right * _xMov) * walkSpeed;

        // Tässä haluttu liike kerrotaan käyttäjän antamalla syötteellä
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        // Tähän muuttujaan tallennetaan x ja z akselien liike ja niiden summa normalisoidaan ja siihen lisätään liike kertomalla nopeus


        // Tässä käyttäjän syöte lähetetään koneelle fysiikan laskentaan
        moveLogic.Move(_velocity);


        // Pyöritetään animaatiota aina kun käyttäjä lähettää koneelle liikkumis syötettä
        RunAnimation(_velocity);
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

        ////Vector2 mousedelta = new Vector2(_xRot, _yRot);
        ////cameraPitch = mousedelta.y;
        ////cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        //// Tähän muuttujaan tallennetaan käyttäjän y syöte ja lisätään liike kertomalla hiiren nopeus
        //Vector3 _rotation = new Vector3(0f, _yRot, 0f) * sensitivity;

        //// Tässä käyttäjän syöte lähetetään koneelle fysiikan laskentaan
        //moveLogic.Rotate(_rotation);
        //// Tähän muuttujaan tallennetaan käyttäjän x syöte ja lisätään liike kertomalla hiiren nopeus
        //Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * sensitivity;

        //// Tässä käyttäjän syöte lähetetään koneelle laskentaan
        //moveLogic.RotateCamera(_cameraRotation);
    }

    private void RunAnimation(Vector3 _playerVelocity)
    {
        // Jos pelaajan liike on erisuuri kuin 0 pyöritetään juoksemis animaatiota, jos pelaajan syöte on 0 niin ei tapahdu mitään
        if (_playerVelocity.z != 0)
        {
            playerAnimator.SetInteger("Run", 1);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimator.SetTrigger("RunJump");
            }
        }
        else
        {
            playerAnimator.SetInteger("Run", 0);
        }
    }
}
