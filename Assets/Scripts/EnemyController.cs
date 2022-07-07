using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 3f;
    [SerializeField] private GameObject enemySpawn;
   
    private PlayerController _playerController;
    private Transform _playerTransform;
    private Rigidbody2D _rb;
    private GameObject _player;
    private Vector2 _enemyMove;
    private EnemySpawnController _enemySpawnController;
    private float multiplier;
    private Animator _animator;

    private bool _isFacingRight = true;
    

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _playerController = _player.GetComponent<PlayerController>(); 
        _playerTransform = _player.GetComponent<Transform>();
        _enemySpawnController = enemySpawn.GetComponent<EnemySpawnController>();
        _animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        _enemyMove = Vector2.right * enemySpeed * Time.fixedDeltaTime;
        float distance = _playerTransform.position.x - transform.position.x;
        multiplier = distance > 0 ? 1 : -1;
        _enemyMove *= multiplier;

        if (_playerController.IsCutScene == false && _playerController.IsEnemyMeet && _enemySpawnController.IsStartEnemyAttack)
        {
            _rb.MovePosition((Vector2)transform.position + _enemyMove);
            _animator.SetTrigger("run");
        }

        FlipDirection();
        
    }

    void FlipDirection()
    {
        if (multiplier < 0f && !_isFacingRight)  Flip();
        else if (multiplier > 0 && _isFacingRight)   Flip();
    }

    void Flip() 
    {
        _isFacingRight = !_isFacingRight;
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }

    

}   

