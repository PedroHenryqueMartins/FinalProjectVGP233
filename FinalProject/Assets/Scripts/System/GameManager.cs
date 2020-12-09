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

    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timerText;

    public static bool gameEnded = false;

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
        SpawnCollectibles();
        timerIsRunning = true;
    }
    void Update()
    {
        CollectedObjects();

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
        toolsFound.text = "Tools Found: " + tools.ToString() + " / 5";
        cakeTxt.text = "Cake Found: " + cakeNum.ToString() + " / 1";
    }

    public void CollectedTool()
    {
        tools += 1;
        if (tools >= 5)
        {
            tools = 5;
        }
    }

    public void CollectedCake()
    {
        cakeNum = 1;
    }
    public void WinLoseCondition()
    {
        if (gameEnded == true)
        {
            SceneManager.LoadScene("LoseScreen");
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
            SceneManager.LoadScene("WinScreen");
        }
    }
}
