using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX = 5f;
    [SerializeField] private GameObject enemy;
    public float rangeToCutScene = 6f;

    const float speedXMultiplier = 50f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private float _horizontalSpeed = 0f;
    private PlayerController _playerController;

    private bool _isGround;
    private bool _isJump;
    private bool _isFacingRight = true;
    private bool _isCutScene;
    private bool _isFight;
    private bool _isEnemyMeet;

    public bool IsEnemyMeet { get => _isEnemyMeet; }

    public bool IsCutScene { get => _isCutScene;}
    
    public bool IsFight { get => _isFight;}


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    { 
        HorizontalSpeed();

        JumpInput();

        AnimatorParameters(); 
    }

    void FixedUpdate()
    {
        HorizontalMovement();

        JumpForce();

        FlipDirection();

        CutScene();
    }

    void HorizontalSpeed() 
    {
        if (_isCutScene == false) _horizontalSpeed = Input.GetAxis("Horizontal");
        else _horizontalSpeed = 0;
    }

    void HorizontalMovement() 
    {
        if (_isCutScene == false) _rb.velocity = new Vector2(_horizontalSpeed * speedX * speedXMultiplier * Time.fixedDeltaTime, _rb.velocity.y);
    }

    void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && _isGround)  _isJump = true;
    }

    void JumpForce() 
    {
        if (_isJump && _isCutScene == false)
        {
            _rb.AddForce(new Vector2(0f, 300f));
            _isGround = false;
            _isJump = false;
        }
    }

    void FlipDirection()
    {
        if (_horizontalSpeed > 0f && !_isFacingRight)  Flip();
        else if (_horizontalSpeed < 0 && _isFacingRight)   Flip();
    }

    void AnimatorParameters()
    {
        _animator.SetFloat("speedX", Mathf.Abs(_horizontalSpeed));
        _animator.SetBool("_isGround", _isGround);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground")) _isGround = true;
    }

    void Flip() 
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    void CutScene() 
    {
        if (enemy.transform.position.x - transform.position.x <= rangeToCutScene && _isEnemyMeet == false && _isCutScene == false) 
        {
            _isCutScene = true;
            _isEnemyMeet = true;
            StartCoroutine (EndCutScene());
        }
    }

    IEnumerator EndCutScene() 
    {
        _rb.velocity = Vector3.zero;
        yield return new WaitForSeconds (5f);
        _isCutScene = false;
    }

    

}
