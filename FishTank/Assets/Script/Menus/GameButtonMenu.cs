using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonMenu : MonoBehaviour
{
    [SerializeField] GameObject enemy_spawner;
    [SerializeField] GameObject game_over_text;

    void Start()
    {
        game_over_text.SetActive(false);
        this.gameObject.SetActive(false);
    }

    // Player want to replay from checkpoint
    public void Replay()
    {
        enemy_spawner.GetComponent<EnemySpawner>().ResetLevel();
        

        this.gameObject.SetActive(false);
        game_over_text.SetActive(false);
    }

    // Return to menu
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
