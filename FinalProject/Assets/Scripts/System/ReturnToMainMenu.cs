using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    GameObject gm;
    private void Start()
    {
        gm = GameObject.Find("GameManager");
        Destroy(gm);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnToMain()
    {
        
        SceneManager.LoadScene("MainMenu");
    }
}
