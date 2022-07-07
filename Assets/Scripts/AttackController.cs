using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{   
    [SerializeField] private GameObject player;

    private Animator _animator;
    private PlayerController _playerController;

    private bool _isAttack1;
    private bool _isAttack2;

    void Start() 
    {
        _playerController = player.GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    void Update() 
    {
        PlayerAttack();
    }

    void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0) && _playerController.IsCutScene == false) 
        {
            _isAttack1 = true;
            _animator.SetTrigger("attack1");
        }

        if (Input.GetMouseButtonDown(1) && _playerController.IsCutScene == false) 
        {
            _isAttack2 = true;
            _animator.SetTrigger("attack2");
        }
    }
}

