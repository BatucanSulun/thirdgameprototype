using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds7 = new(7);
    [SerializeField] private float speed = 5f;
    [SerializeField] private InputAction movementInput;
    [SerializeField] private float powerupStrength = 150f;
    private GameObject _focalPoint;
    private float _verticalInput; // Sadece ileri-geri değerini tutacağız
    internal float _rotationInput; //Bu değeri benim RotateCamera scriptine göndermem lazım
    [SerializeField] private bool hasPowerUp;
    private Rigidbody _rb;
    [SerializeField] private GameObject powerUpIndicator; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        // Girdiyi oku: Vector2'nin 'y' bileşeni ileri-geriyi temsil eder (W/S veya Up/Down)
        _verticalInput = movementInput.ReadValue<Vector2>().y;
        _rotationInput = movementInput.ReadValue<Vector2>().x;
        powerUpIndicator.transform.position =gameObject.transform.position + new Vector3(0, -0.5f, 0);
    }
    private void FixedUpdate()
    {
        MovementHandler();

    }
    private void OnEnable()
    {
        // New Input System'da action'ları kullanmadan önce aktif etmelisin
        movementInput.Enable();
    }

    private void OnDisable()
    {
        // Bellek sızıntısını önlemek için kapatmalısın
        movementInput.Disable();
    }

    private void MovementHandler()
    {
        Vector3 forwardDirection=_focalPoint.transform.forward;
        forwardDirection.y = 0;//Aşağı yukarı gitmesini engelledik.
        forwardDirection.Normalize();
        Vector3 targetMove = _verticalInput * speed * forwardDirection;
        _rb.AddForce(_verticalInput * speed * forwardDirection);
        //_rb.linearVelocity = new Vector3(targetMove.x, _rb.linearVelocity.y, targetMove.z);//y değerini olduğu gibi tuttuk.lienarVelocity ile değer verirsen olmaz.
        //linearVelocity katı cismin pozisyonunun zamana göre değişimini belirtir, çarpışma hissi yaratmaz.

    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return _waitForSeconds7;
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) //BoxCollider'ın IsTrigger prop'u checklendiği zaman bu metodu kullanıyoruz.Fiziksel çaprışma gerçekleşmiyor ama trigger zone tetikleniyor.
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            powerUpIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
    private void OnCollisionEnter(Collision collision) //Fiziki olarak çarpışma gerçekleştiği zaman bişey olmasını istediğimiz için OnCollisonEnter metodunu çağrdık
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp) //collision argümanı çarpışma sırasında yaşanacak şeyleri temsil ediyor ve çarptığı cisme Tag üzerinden ulaşabilirsin.Ayrıca powerup kullanabilmek için gerekli olan koşulu hazırlamış oluyoruz.
        {
            Debug.Log("Collided with" + collision.gameObject.name +"with powerup set to "+ hasPowerUp);
            Rigidbody enemyBody= collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - gameObject.transform.position;
            awayFromPlayer.y = 0;
            enemyBody.AddForce(awayFromPlayer.normalized*powerupStrength,ForceMode.Impulse);
        }
    }




}
