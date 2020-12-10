using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameEndedScreen : MonoBehaviour
{

    public Text infoData;
    public GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        ShowData();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Destroy(gm);
    }

    void ShowData()
    {
        float min = Mathf.FloorToInt(gm.GetComponent<GameManager>().timeRemaining / 60);
        float sec = Mathf.FloorToInt(gm.GetComponent<GameManager>().timeRemaining % 60);
        infoData.text = "Level concluded in: " + string.Format("{0:00} : {1:00} !", min, sec);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
