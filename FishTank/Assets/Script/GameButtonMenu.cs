using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonMenu : MonoBehaviour
{
    // Should reload the gamescene
    public void Replay()
    {
        SceneManager.LoadScene("Level1");
    }

    // Return to menu
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
