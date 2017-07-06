using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour {

    public Warper warper;
    public GameObject[] gameMenus;

    public void StartGame(int gameMode)
    {
        this.ActivateWarpTrail();
        warper.StartGame(gameMode);
        gameObject.SetActive(false);
    }

    public void GameOver()
    {
        this.ChangeMenu(7);
        gameObject.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }

    public void ChangeMenu(int gameMenu)
    {
        for(var i = 0; i < gameMenus.Length; i++)
        {
            if(i == (int)gameMenu)
            {
                gameMenus[i].SetActive(true);
            }
            else
            {
                gameMenus[i].SetActive(false);
            }
        }
    }

    public void Start()
    {
        this.DeactivateWarpTrail();
        warper.StartGame(0);
    }

    private void ActivateWarpTrail()
    {
        warper.Warpel.SetActive(true);
        warper.WarpTrail.SetActive(true);
    }

    private void DeactivateWarpTrail()
    {
        warper.Warpel.SetActive(false);
        warper.WarpTrail.SetActive(false);
    }

    public enum GameMenus
    {
        MainMenu = 0,
        GameMenu = 1, 
        SettingMenu = 2,
        AboutMenu = 3,
    }
}
