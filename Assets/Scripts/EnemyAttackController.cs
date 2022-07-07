using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private GameObject _player;
    private bool _isEnemyAttack;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        // if (_isEnemyAttack) 
    }


    void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) _isEnemyAttack = true;
    }


}
