using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;

    public GameObject cakePrefab;
    public Transform cakePos;
    public GameObject spaceShip;
    public GameObject[] spacheshipPieces;
    public Transform[] toolsPosition;
    public Transform startPos;
    public Text toolsFound;
    public Text cakeTxt;
    int tools = 0;
    int cakeNum = 0;

    public float timeRemaining;
    public bool timerIsRunning = false;
    public Text timerText;
    

    public static bool gameEnded = false;
    bool sceneLoaded = false;
    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "EasyLevel")
        {
            timerText.enabled = false;
            timerIsRunning = false;
        }
        else if (scene.name == "NormalLevel")
        {
            timeRemaining = 600.0f;
            timerText.enabled = true;
            timerIsRunning = true;
        }
        else if (scene.name == "HardLevel")
        {
            timeRemaining = 300.0f;
            timerText.enabled = true;
            timerIsRunning = true;
        }
        //timerIsRunning = true;
        SpawnCollectibles();
    }
    void Update()
    {
        CollectedObjects();
        WinLoseCondition();

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                gameEnded = true;
            }
        }


    }


    void CollectedObjects()
    {
        if (gameEnded == false)
        {
            toolsFound.text = "Tools Found: " + tools.ToString() + " / 5";
            cakeTxt.text = "Cake Found: " + cakeNum.ToString() + " / 1";
        }
        
    }

    public void CollectedTool()
    {
<<<<<<< HEAD
        tools += 1;
=======
        // Play PickUp sound
        AudioManager.Instance.PlaySound(2);
        tools += 1;

>>>>>>> 95a2e539a57dc5fcb85675a2724b784c3fb67d36
        if (tools >= 5)
        {
            tools = 5;
        }
    }

    public void CollectedCake()
    {
<<<<<<< HEAD
=======
        // Play PickUp Sound
        AudioManager.Instance.PlaySound(2);
>>>>>>> 95a2e539a57dc5fcb85675a2724b784c3fb67d36
        cakeNum = 1;
    }
    public void WinLoseCondition()
    {
        
        if (gameEnded == true && sceneLoaded == false)
        {
            timerIsRunning = false;
<<<<<<< HEAD
=======
            // Play LoseScreen Sound
            AudioManager.Instance.PlaySound(5);
>>>>>>> 95a2e539a57dc5fcb85675a2724b784c3fb67d36
            SceneManager.LoadScene("LoseScreen");
            this.enabled = false;
            sceneLoaded = true;
        }

        if (tools == 5 && cakeNum == 1)
        {
            spaceShip.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void SpawnCollectibles()
    {
        Instantiate(cakePrefab, cakePos.transform.position, Quaternion.identity);
        Instantiate(spacheshipPieces[0], toolsPosition[0].transform.position, Quaternion.identity);
        Instantiate(spacheshipPieces[1], toolsPosition[1].transform.position, Quaternion.identity);
        Instantiate(spacheshipPieces[2], toolsPosition[2].transform.position, Quaternion.identity);
        Instantiate(spacheshipPieces[3], toolsPosition[3].transform.position, Quaternion.identity);
        Instantiate(spacheshipPieces[4], toolsPosition[4].transform.position, Quaternion.identity);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00} : {1:00}", min, sec);
    }

    public void EnterSpaceship()
    {
        if (gameEnded == false)
        {
<<<<<<< HEAD
=======
            // Play WinScreen Sound
            AudioManager.Instance.PlaySound(6);
>>>>>>> 95a2e539a57dc5fcb85675a2724b784c3fb67d36
            SceneManager.LoadScene("WinScreen");
        }
    }

}
