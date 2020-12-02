using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    public CharacterController characterController;
    public float moveSpeed = 10.0f;
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

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();
        ServiceLocator.Register<GameManager>(gm);
    }
    void Start()
    {
        maxplayerHP = 100.0f;
        health = maxplayerHP;
        
    }

   

    void Update()
    {

        UpdateLife();
        //healthHUD.fillAmount = health / maxplayerHP;
        //healthValue.text = health.ToString();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && worldGravity.y < 0)
        {
            worldGravity.y = -2.0f; 
        }

        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xAxis + transform.forward * zAxis;

        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            worldGravity.y = Mathf.Sqrt(jumpForce * -2.0f * gravity);
        }

        worldGravity.y += gravity * Time.deltaTime;
        characterController.Move(worldGravity * Time.deltaTime);

        

    }

    

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Enemy"))
        {
            health -= 0.2f;
        }
    }

    

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
}
