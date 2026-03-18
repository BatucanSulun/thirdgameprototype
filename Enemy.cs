using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 5.0f;
    private Rigidbody _enemyRb;
    private GameObject _player;
    private Vector3 _playerPosition;
    private float _playerYPosition = -10f;
    Vector3 lookDirection;
    //private Vector3 distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < _playerYPosition)
        {
            Destroy(gameObject);    
        }
    }
    private void FixedUpdate()
    {
        EnemyHandler();
    }
    private void EnemyHandler()
    {
        _playerPosition = _player.transform.position;
        lookDirection = (_playerPosition - transform.position).normalized;
        lookDirection.y = 0;//Çarpışma sonucunda aşağı veya yukarı hareket olmaması için seçtik.
        //distance = (_playerPosition- _enemyRb.position).normalized*enemySpeed;
        //_enemyRb.linearVelocity = distance; 
        _enemyRb.AddForce(lookDirection * enemySpeed, ForceMode.Acceleration);//Player ile çarpışma sonucu istediğimiz gibi hareket etmesi için bu force modu seçtik.
    }
}
