using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CameraBehaviour))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject biome_text;
    [SerializeField] GameObject wave_text;
    [SerializeField] GameObject depth_text;

    string[] depth_names = { "0m", "-100m", "-1000m" };
    string[] biome_names = { "Whalefall Wasteland", "Trash Heap", "The Bleached Reef" };
    [SerializeField] GameObject Background;
    [SerializeField] Sprite[] background_sprites;

    [SerializeField] private float interval; // Time between spawns
    private float wave_interval = 3f; // Time between waves
    private int level_enemies = 0; // How many enemies the player has fought this wave
    private int max_enemies = 5; // How many enemies the player will fight this wave
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

        // Display the current biome name
        if (waves < 3)
        {
            biome_text.GetComponent<TextMeshProUGUI>().text = biome_names[waves];
            depth_text.GetComponent<TextMeshProUGUI>().text = "Depth: " + depth_names[waves];
            Background.GetComponent<SpriteRenderer>().sprite = background_sprites[waves];
        }

        else
        {
            biome_text.GetComponent<TextMeshProUGUI>().text = "You win";
        }

        biome_text.SetActive(true);
        depth_text.SetActive(true);

        if (start_timer > wave_interval)
        {
            biome_text.SetActive(false);
            depth_text.SetActive(false);

            // Spawn enemies on a timer
            timer += Time.deltaTime;

            // Move to the next wave
            if (level_enemies >= max_enemies)
            {
                waves++;
                wave_text.GetComponent<TextMeshProUGUI>().text = "Waves: " + waves;
                level_enemies = 0;
                start_timer = 0f;
                max_enemies += 5; // Player has to fight more enemies in the next wave
            }

            // Only spawn if there are less than the max enemies
            if (timer >= interval && level_enemies < max_enemies)
            {
                timer = 0f;
                level_enemies++;

                // Pick a random enemy to spawn from enemies length
                if (waves+1 < enemies.Length)
                {
                    rand = Random.Range(0, waves+1);
                }

                // Spawn all the enemies
                else
                {
                    rand = Random.Range(0, enemies.Length);
                }

                // Spawn from a random enemy and x-val on top of the screen
                Instantiate(enemies[rand], new Vector3(Random.Range(GetComponent<CameraBehaviour>().cameraRect.xMin, GetComponent<CameraBehaviour>().cameraRect.xMax), 5f, 0), Quaternion.identity);
            }
        }
    }
}
