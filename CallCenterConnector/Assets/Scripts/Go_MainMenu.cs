using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{
    private void Awake() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void MAINMENU() {
        SceneManager.LoadScene(0);
    }

}
