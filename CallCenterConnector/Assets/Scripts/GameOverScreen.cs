using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour {
    private void Awake() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RESTART() {
        SceneManager.LoadScene(1);
    }

    public void MAINMENU() {
        SceneManager.LoadScene(0);
    }

}
