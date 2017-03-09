using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public void jugar () {
        SceneManager.LoadScene("Escena 1");
    }

    void Update() {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

}
