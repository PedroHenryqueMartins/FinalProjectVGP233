using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{
    #region VARIABLES
    public CharacterController characterController;
    public GameObject _mainCharacter;
    public float moveSpeed = 10.0f;
    public float dividedSpeed = 0.0f;
    public float jumpForce = 4.0f;
    float maxplayerHP = 100.0f;
    public static float health;

    float gravity = -10.0f;
    Vector3 worldGravity;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundMask;

    bool isGrounded;

    public Image[] healthHUD;
    public Text healthValue;
    GameObject gameManager;
    GameManager gm;

    private Vector3 _playerPosition;

    // Animation variables
    Animator _animator;
    private float mCurrentSpeed = 0.0f;

    private enum PlayerState
    {
        Idle,
        Walk_Run,
        Jump,
        Fall,
        Attack,
        Defeated
    }

    PlayerState _playerState;
    #endregion

    #region AWAKE
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();
        ServiceLocator.Register<GameManager>(gm);

        _playerPosition = new Vector3(0.0f, 8.5f, 0.0f);

        _playerState = PlayerState.Idle;
    }
    #endregion

    #region START
    void Start()
    {
        // Player start point
        characterController.transform.position = _playerPosition;
        _mainCharacter.transform.position = _playerPosition;

        maxplayerHP = 100.0f;
        health = maxplayerHP;
        dividedSpeed = 1 / moveSpeed;

        _animator = GetComponent<Animator>();
    }
    #endregion

    #region UPDATE
    void Update()
    {

        UpdateLife();
        //healthHUD.fillAmount = health / maxplayerHP;
        //healthValue.text = health.ToString();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && worldGravity.y < 0)
        {
            worldGravity.y = -2.0f;
            // _animator.SetBool("isJumping", false);
        }

        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xAxis + transform.forward * zAxis;

        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            worldGravity.y = Mathf.Sqrt(jumpForce * -2.0f * gravity);
            // _animator.SetBool("isJumping", true);
        }

        worldGravity.y += gravity * Time.deltaTime;
        characterController.Move(worldGravity * Time.deltaTime);

        // Animation Walk / Run
        if(Input.GetKey(KeyCode.W))
        {
            mCurrentSpeed += moveSpeed * Time.deltaTime;
            _playerState = PlayerState.Walk_Run;
        }
        else
        {
            mCurrentSpeed -= moveSpeed * Time.deltaTime;

            if (mCurrentSpeed == 0)
            {
                _playerState = PlayerState.Idle;
            }
        }

        // Animation Falling
        if(characterController.transform.position.y < -10)
        {
            _playerState = PlayerState.Fall;
            // Vector3(0, 8.5, 0)
        }

        mCurrentSpeed = Mathf.Clamp(mCurrentSpeed, 0.0f, moveSpeed);

        UpdateAnimation();
        UpdatePosition();

    }
    #endregion

    #region ON CONTROLLER COLLIDER HIT
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Enemy"))
        {
            health -= 0.2f;
        }
    }
    #endregion

    #region UPDATE LIFE
    void UpdateLife()
    {
        if (healthHUD[0].fillAmount > 0)
        {
            healthHUD[0].fillAmount = health / maxplayerHP;
            if (healthHUD[0].fillAmount == 0)
            {
                health = maxplayerHP;
            }
        }
        else if (healthHUD[1].fillAmount > 0)
        {
            healthHUD[1].fillAmount = health / maxplayerHP;
            if (healthHUD[1].fillAmount == 0)
            {
                health = maxplayerHP;
            }

        }
        else if (healthHUD[2].fillAmount > 0)
        {
            healthHUD[2].fillAmount = health / maxplayerHP;
        }
        else 
        {
            GameManager.gameEnded = true;
            gm.WinLoseCondition();
        }
    }
    #endregion

    #region UPDATE ANIMATION
    private void UpdateAnimation()
    {
        if(_playerState.Equals(PlayerState.Walk_Run))
        {
            _animator.SetFloat("playerSpeed", mCurrentSpeed * dividedSpeed);
        }
        else if(_playerState.Equals(PlayerState.Fall))
        {
            _animator.SetBool("isFalling", true);
        }
    }
    #endregion

    #region UPDATE POSITION
    private void UpdatePosition()
    {
        if(characterController.transform.position.y < -100)
        {
            _playerState = PlayerState.Idle;
            _animator.SetBool("isFalling", false);
            characterController.transform.position = _playerPosition;
            _mainCharacter.transform.position = _playerPosition;
        }
    }
    #endregion
}
