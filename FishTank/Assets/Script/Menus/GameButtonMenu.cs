using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonMenu : MonoBehaviour
{
    [SerializeField] GameObject enemy_spawner;
    [SerializeField] GameObject game_over_text;
    public static GameButtonMenu Instance { get; private set; }

    void Start()
    {
        Instance.gameObject.SetActive(false);
        game_over_text.SetActive(false);
    }

    void Update()
    {
    }

    // Should reload the gamescene
    public void Replay()
    {
        enemy_spawner.GetComponent<EnemySpawner>().ResetLevel();
        
        Instance.gameObject.SetActive(false);
        game_over_text.SetActive(false);
    }

    // Return to menu
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
