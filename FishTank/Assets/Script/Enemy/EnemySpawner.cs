using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CameraBehaviour))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject biome_text;
    [SerializeField] GameObject wave_text;
    [SerializeField] GameObject depth_text;

    [SerializeField] GameObject player;

    string[] depth_names = { "0m", "-100m", "-1000m" };
    string[] biome_names = { "Whalefall Wasteland", "Trash Heap", "The Bleached Reef" };
    [SerializeField] GameObject Background;
    [SerializeField] Sprite[] background_sprites;

    [SerializeField] private float interval; // Time between spawns
    private float wave_interval = 5f; // Time between waves
    private float timer = 0f; // Time to spawn enemies
    private float start_timer = 0f;
    private int waves = 0;
    private int wave_cost = 100;

    public GameObject[] enemies;

    int rand;
    bool level_complete = false;

    // Update is called once per frame
    void Update()
    {
        // Only update if enemy has health
        if (player.GetComponent<PlayerMovement>().health > 0)
        {
            start_timer += Time.deltaTime;

            // Display the current biome name
            if (waves < 3 && start_timer > 2)
            {
                biome_text.GetComponent<TextMeshProUGUI>().text = biome_names[waves];
                depth_text.GetComponent<TextMeshProUGUI>().text = "Depth: " + depth_names[waves];
                Background.GetComponent<SpriteRenderer>().sprite = background_sprites[waves];
            }

            else
            {
                
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
                if (wave_cost < 0)
                {
                    waves++;
                    wave_text.GetComponent<TextMeshProUGUI>().text = "Waves: " + waves;
                    start_timer = 0f;
                    Background.GetComponent<ScrollingBackground>().change_bg = true;
                    wave_cost = 100 + waves * 10; // Increase cost to move to next wave
                }

                // Only spawn if there are less than the max enemies
                if (timer >= interval && wave_cost >= 0)
                {
                    timer = 0f;

                    // Pick a random enemy to spawn from enemies length
                    if (waves + 1 < enemies.Length)
                    {
                        rand = Random.Range(0, waves + 1);
                    }

                    // Spawn all the enemies
                    else
                    {
                        rand = Random.Range(0, enemies.Length);
                    }

                    wave_cost -= enemies[rand].GetComponent<EnemyLook>().GetScore();

                    // Spawn from a random enemy and x-val on top of the screen
                    Instantiate(enemies[rand], new Vector3(Random.Range(GetComponent<CameraBehaviour>().cameraRect.xMin, GetComponent<CameraBehaviour>().cameraRect.xMax), 5f, 0), Quaternion.identity);
                }
            }
        }
    }

    // Player wants to retry
    public void ResetLevel()
    {
        wave_cost = 100 + waves * 10; // Increase cost to move to next wave
        start_timer = 0f;
        player.GetComponent<PlayerMovement>().Reset();
    }
}
