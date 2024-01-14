using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            //restart scene on any key pressed
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
