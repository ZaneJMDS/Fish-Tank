using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject basic_enemy;
    public GameObject loot_fish;
    
    [SerializeField] private float interval; // Time between spawns
    [SerializeField] private int level_enemies = 0; // How many enemies the player will fight before going to the next level
    private float timer = 0f;

    // Count enemies
    GameObject[] enemies;
    int enemy_count = 0;

    int rand;
    bool level_complete = false;

    // Update is called once per frame
    void Update()
    {
        // Count enemies
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
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
            // First number is INCLUSIVE, second is EXCLUSIVE!
            rand = Random.Range(0, 2);

            // Basic
            if (rand == 0)
            {
                // Spawn from a random x-val on top of the screen
                Instantiate(basic_enemy, new Vector3(Random.Range(-5f, 5f), 5f, 0), Quaternion.identity);
            }

            // Loot fish
            else if (rand == 1)
            {
                // Spawn from a random x-val on top of the screen
                Instantiate(loot_fish, new Vector3(Random.Range(-5f, 5f), 5f, 0), Quaternion.identity);
            }
        }
    }
}
