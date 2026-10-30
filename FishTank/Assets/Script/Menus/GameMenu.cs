/***********************************************************************
Author      :	ThunderDev
Description :   Main menu
File name   :   GameMenu.cs
**************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private string game_scene_name;

    public void Play()
    {
        SceneManager.LoadScene(game_scene_name);
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
