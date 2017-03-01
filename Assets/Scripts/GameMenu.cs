using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

    public Warper warper;

    public void StartGame()
    {
        warper.StartGame();
        gameObject.SetActive(false);
    }

    public void GameOver()
    {
        gameObject.SetActive(true);
    }
}
