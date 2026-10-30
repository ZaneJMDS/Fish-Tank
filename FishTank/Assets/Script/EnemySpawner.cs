using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    [SerializeField] private float interval; // Time between spawns
    [SerializeField] private int level_enemies = 0; // How many enemies the player will fight before going to the next level
    private float timer = 0f;

    // Count enemies
    public GameObject[] enemies;
    int enemy_count = 0;

    int rand;
    bool level_complete = false;

    // Update is called once per frame
    void Update()
    {
        enemy_count = enemies.Length;

        // Spawn enemies on a timer
        timer += Time.deltaTime;

        // Move to the next level
        if (level_enemies > 20)
        {
            level_complete = true;
            Debug.Log("You win");
        }

        // Only spawn if there are less than 10 enemies on screen
        if (timer >= interval && enemy_count < 10)
        {
            timer = 0f;
            level_enemies++;

            // Pick a random enemy to spawn
            rand = Random.Range(0, 3);

            // Basic
            if (rand == 0)
            {
                // Spawn from a random x-val on top of the screen
                Instantiate(enemies[0], new Vector3(Random.Range(-5f, 5f), 5f, 0), Quaternion.identity);
            }

            // Loot fish
            else if (rand == 1)
            {
                // Spawn from a random x-val on top of the screen
                Instantiate(enemies[1], new Vector3(Random.Range(-5f, 5f), 5f, 0), Quaternion.identity);
            }

            // Tank
            else if (rand == 2)
            {
                // Spawn from a random x-val on top of the screen
                Instantiate(enemies[2], new Vector3(Random.Range(-5f, 5f), 5f, 0), Quaternion.identity);
            }
        }
    }
}
