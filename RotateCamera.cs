using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCamera : MonoBehaviour
{
    #region Input Action'lı Kod
    //[SerializeField] private float rotationalSpeed = 2f;
    //[SerializeField] private InputAction rotationInput;
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{

    //}

    //private void OnEnable()
    //{
    //    rotationInput.Enable(); 
    //}

    //private void OnDisable()
    //{
    //    rotationInput.Disable();    
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    HandleRotation();
    //}

    //private void HandleRotation()
    //{

    //    float inputX =-1* rotationInput.ReadValue<Vector2>().x;
    //    transform.Rotate(inputX * Time.deltaTime * Vector3.up, rotationalSpeed);
    //    //float inputX = rotationInput.ReadValue<Vector2>().x;
    //    // Space.World ekleyerek kameranın yana yatmasını engelliyoruz
    //    //transform.Rotate(inputX * rotationalSpeed * Time.deltaTime * Vector3.up, Space.World);
    //}


    #endregion

    #region Input Action'sız Kod
    //Tek bir input action kullanarak kamera döndürme işini yaptık
    //Bu sayede derleyicini verdiği uyarı mesajından kurtulduk.
    //Derleyici şöyle bir uyarı veriyordu:
    //"Focal Point isimli objedeki PlayerInput bileşeni için uygun bir kontrol şeması (Klavye, Gamepad vb.) aradım ama ya bulamadım ya da bulduğum tüm cihazlar zaten başka bir PlayerInput tarafından kapılmış.

    private PlayerController _playerControllerScript;
    [SerializeField] private float rotationalSpeed = 70f;
    private float _rotationInput;
    private void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();    
    }
    private void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        if (_playerControllerScript == null) 
        {
            Debug.Log("Player Control Scripti Bulunamıyor");
            return;
        }
        _rotationInput = _playerControllerScript._rotationInput;
         float inputX =-1* _rotationInput;
        transform.Rotate(Vector3.up, inputX * rotationalSpeed * Time.deltaTime);
    }

    #endregion

}
