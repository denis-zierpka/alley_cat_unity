using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public bool success = false;

    public string successWindow;

    void Start() {
        int randomInt = Random.Range(1, 10);
        successWindow = "Window" + randomInt.ToString();
        GameObject.Find(successWindow).GetComponent<SpriteRenderer>().color = Color.green;
        for (int i = 1; i < 10; ++i) {
            if (i != randomInt) {
                GameObject.Find("Window" + i.ToString()).GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    void Update() {
        if (isGameOver) {
            if (Input.GetKeyDown(KeyCode.R)) {
                ResetGame();
            }
        }
    }

    private void ResetGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        Time.timeScale = 1; 
        GameObject.Find("GameResult").GetComponent<Text>().enabled = false;
        isGameOver = false;
    }

    public void GameOver(bool success = false) {
        Time.timeScale = 0; 
        isGameOver = true;

        Text theText = GameObject.Find("GameResult").GetComponent<Text>();
        if (success) {
            theText.text = "Success! (cntr + R - to restart)";
        } else {
            theText.text = "Failure! (cntr + R - to restart)";
        }
        
        theText.enabled = true;
    }
}
