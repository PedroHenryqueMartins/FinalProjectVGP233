using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;

    public GameObject cakePrefab;
    public GameObject[] spacheshipPieces;

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
       
    }
    void Update()
    {

    }


    void DisplayStats()
    {
        
        
    }

    

    public void WinLoseCondition()
    {
        if (gameEnded == true)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }
}
