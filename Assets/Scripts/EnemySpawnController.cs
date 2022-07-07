using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject enemyText;

    private Animator _dialogueAnimator;
    private PlayerController _playerController;

    private bool _isPlayerOnPoint;
    private bool _isDialogue;
    private bool _isDialogueStart = true;
    private bool _isStartEnemyAttack;
    private bool _isEndDialogue;

    public bool IsStartEnemyAttack { get => _isStartEnemyAttack; }

    void Start() 
    {
        _playerController = player.GetComponent<PlayerController>();
        _dialogueAnimator = dialogue.GetComponent<Animator>();

    }

    void Update()
    {
        EnemyAndPlayerDialogue();

        if ((dialogue.activeSelf == false) && (_playerController.IsEnemyMeet == true) && (_playerController.IsCutScene == false)) _isStartEnemyAttack = true;
    }

    void FixedUpdate()
    {
        EnemyMeeting();
    }

    void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player")) _isPlayerOnPoint = true;
    }

    void EnemyMeeting()
    {
        if (_isPlayerOnPoint)
        {
            enemy.SetActive(true);
            _isPlayerOnPoint = false;
        }
    }

    void EnemyAndPlayerDialogue()
    {
        if (_playerController.IsCutScene && _isDialogueStart)
        {
            StartCoroutine(EnemyDialogue());
            _isDialogueStart = false;
        }

        if (_isDialogue) 
        {
            StartCoroutine(ShowText());
            _isDialogue = false;
        }

        if (_playerController.IsCutScene == false) StartCoroutine(DialogueEnd());
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(0.5f);
        enemyText.SetActive(true);
    }

    IEnumerator EnemyDialogue()
    {
        yield return new WaitForSeconds(2f);
        dialogue.SetActive(true);
        _isDialogue = true;
    }

    IEnumerator DialogueEnd()
    {
        enemyText.SetActive(false);
        _dialogueAnimator.SetTrigger("isEnd");
        yield return new WaitForSeconds(0.4f);
        dialogue.SetActive(false);
    }
}
