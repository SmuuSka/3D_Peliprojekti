using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveLogic))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float sensitivity = 3f;

    private MoveLogic moveLogic;
    private Animator playerAnimator;

    private void Start()
    {
        // Luodaan yhteys näihin peliobjektin komponentteihin
        moveLogic = GetComponent<MoveLogic>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Tässä luetaan käyttäjän näppäimistön syötteet x ja z akselilla-liikkumiselle ja tallennetaan se muuttujiin
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        // Tässä haluttu liike kerrotaan käyttäjän antamalla syötteellä
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        // Tähän muuttujaan tallennetaan x ja z akselien liike ja niiden summa normalisoidaan ja siihen lisätään liike kertomalla nopeus
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        Debug.Log($"Velocity: {_velocity}");

        // Tässä käyttäjän syöte lähetetään koneelle fysiikan laskentaan
        moveLogic.Move(_velocity);

        // Tässä luetaan käyttäjän hiiren syötteet x ja y akselilla-liikkumiselle ja muuttujiin
        float _yRot = Input.GetAxisRaw("Mouse X");
        float _xRot = Input.GetAxisRaw("Mouse Y");

        // Tähän muuttujaan tallennetaan käyttäjän y syöte ja lisätään liike kertomalla hiiren nopeus
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * sensitivity;

        // Tässä käyttäjän syöte lähetetään koneelle fysiikan laskentaan
        moveLogic.Rotate(_rotation);

        // Tähän muuttujaan tallennetaan käyttäjän x syöte ja lisätään liike kertomalla hiiren nopeus
        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * sensitivity;

        // Tässä käyttäjän syöte lähetetään koneelle fysiikan laskentaan
        moveLogic.RotateCamera(_cameraRotation);

        // Pyöritetään animaatiota aina kun käyttäjä lähettää koneelle liikkumis syötettä
        RunAnimation(_velocity);
    }

    private void RunAnimation(Vector3 _playerVelocity)
    {
        // Jos pelaajan liike on erisuuri kuin 0 pyöritetään juoksemis animaatiota, jos pelaajan syöte on 0 niin ei tapahdu mitään
        if (_playerVelocity.z != 0)
        {
            playerAnimator.SetInteger("Run", 1);
        }
        else
        {
            playerAnimator.SetInteger("Run", 0);
        }
    }
}
