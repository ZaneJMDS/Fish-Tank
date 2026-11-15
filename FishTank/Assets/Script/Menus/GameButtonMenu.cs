using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonMenu : MonoBehaviour
{
    [SerializeField] GameObject enemy_spawner;

    // Should reload the gamescene
    public void Replay()
    {
        enemy_spawner.GetComponent<EnemySpawner>().ResetLevel();
    }

    // Return to menu
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
