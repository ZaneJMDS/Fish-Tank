using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject biome_text;

    [SerializeField] private float interval; // Time between spawns
    private int level_enemies = 0; // How many enemies the player will fight before going to the next level
    private float timer = 0f; // Time to spawn enemies
    private float start_timer = 0f;
    private int waves = 0;

    public GameObject[] enemies;

    int rand;
    bool level_complete = false;

    // Update is called once per frame
    void Update()
    {
        start_timer += Time.deltaTime;

        if (start_timer > 3f)
        {
            biome_text.SetActive(false);

            // Spawn enemies on a timer
            timer += Time.deltaTime;

            // Move to the next level
            if (level_enemies == 20)
            {
                waves++;
                level_enemies = 0;

                Debug.Log("New wave");
            }

            // Only spawn if there are less than 20 overall enemies
            if (timer >= interval && level_enemies < 20)
            {
                timer = 0f;
                level_enemies++;

                // Pick a random enemy to spawn from enemies length
                rand = Random.Range(0, enemies.Length);

                // Spawn from a random enemy and x-val on top of the screen
                Instantiate(enemies[rand], new Vector3(Random.Range(-5f, 5f), 5f, 0), Quaternion.identity);
            }
        }
    }
}
