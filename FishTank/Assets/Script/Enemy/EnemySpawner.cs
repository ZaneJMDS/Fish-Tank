/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Spawn enemies and progress waves (Main game loop)
**************************************************************************/

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawner : MonoBehaviour
{
    // Text fields
    [SerializeField] private GameObject biome_text;
    [SerializeField] private GameObject wave_text;
    [SerializeField] private GameObject depth_text;

    // Visual indicators
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject progress_player;

    [SerializeField] private string[] depth_names = { "0m", "-100m", "-1000m" };
    [SerializeField] private Sprite[] biome_names;
    [SerializeField] private GameObject Background;
    [SerializeField] private Sprite[] background_sprites;

    // Timers
    // Time to spawn enemies
    private float timer = 0f;
    [SerializeField] private float interval;

    // Time between waves
    private float start_timer = 0f;
    private float wave_interval = 5f;
    
    // Waves
    private int waves = 0;
    private int wave_cost = 100;

    // Spawnables
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject LootFish;
    [SerializeField] private GameObject[] obstacles;

    private bool level_complete = false;

    private Rect spawn_space;

    private void Start()
    {
        spawn_space = GetComponent<CameraBehaviour>().cameraRect;
        progress_player.transform.position = new Vector2(8.4f, -4.25f);
    }

    // Update is called once per frame
    void Update()
    {
        // Only update if enemy has health
        if (player.GetComponent<PlayerMovement>().health > 0)
        {
            // Increase 
            start_timer += Time.deltaTime;

            // Display new biome stuff
            if (waves < 3 && start_timer > 2)
            {
                biome_text.SetActive(true);
                depth_text.SetActive(true);

                biome_text.GetComponent<SpriteRenderer>().sprite = biome_names[waves];
                depth_text.GetComponent<TextMeshProUGUI>().text = "Depth: " + depth_names[waves];
                Background.GetComponent<SpriteRenderer>().sprite = background_sprites[waves];
            }

            // Display the last oasis, and menu buttons
            else
            {
                level_complete = true;
            }

            // Start spawning enemies
            if (start_timer > wave_interval)
            {
                biome_text.SetActive(false);
                depth_text.SetActive(false);

                // Spawn enemies on a timer
                timer += Time.deltaTime;

                // Move to the next wave
                if (wave_cost <= 0 && GameObject.FindGameObjectsWithTag("Enemies").Length == 0)
                {
                    waves++;
                    wave_text.GetComponent<TextMeshProUGUI>().text = "Waves: " + waves;
                    Background.GetComponent<ScrollingBackground>().change_bg = true;

                    GameManager.SetScore();
                    ResetWaveCost();
                }

                // Only spawn if there are less than the max enemies
                if (timer >= interval && wave_cost > 0)
                {
                    timer = 0f;
                    int rand = Random.Range(0, 5); // Pick spawn type

                    // Spawn an enemy
                    if (rand < 3)
                    {
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

                        wave_cost -= enemies[rand].GetComponent<EnemyBase>().enemy_score;
                        progress_player.transform.position += new Vector3(0, enemies[rand].GetComponent<EnemyBase>().enemy_score / 50f, 0);

                        SpawnFromTop(enemies[rand]);
                    }

                    // Spawn a LootFish
                    if (rand == 3)
                    {
                        wave_cost -= LootFish.GetComponent<EnemyBase>().enemy_score;
                        progress_player.transform.position += new Vector3(0, LootFish.GetComponent<EnemyBase>().enemy_score / 50f, 0);

                        SpawnFromTop(LootFish);
                    }

                    // Spawn an obstacle
                    else
                    {
                        if (obstacles[waves].name == "Urchin")
                        {
                            rand = Random.Range(0, 2);

                            // Spawn from left
                            if (rand == 0)
                            {
                                Instantiate(obstacles[waves], new Vector3(-9, Random.Range(0f, 5f), 0), Quaternion.identity);
                            }

                            // Spawn from right
                            else
                            {
                                Instantiate(obstacles[waves], new Vector3(9, Random.Range(0f, 5f), 0), Quaternion.identity);
                            }
                        }

                        else
                        {
                            SpawnFromTop(obstacles[waves]);
                        }
                    }
                }
            }
        }
    }

    // Player wants to retry
    public void ResetLevel()
    {
        // Kill all enemies currently in the wave
        GameObject[] all_enemies = GameObject.FindGameObjectsWithTag("Enemies");
        for (int i = 0; i < all_enemies.Length; i++)
        {
            Destroy(all_enemies[i]);
        }

        ResetWaveCost();
        
        progress_player.transform.position = new Vector2(8.4f, -4.25f);
        player.GetComponent<PlayerMovement>().Reset();
        GameManager.ResetScore();
    }

    // Set wave cost based on current wave
    public void ResetWaveCost()
    {
        start_timer = 0f;
        wave_cost = 100 + waves * 10;
    }

    // Spawn from the top of the level
    public void SpawnFromTop(GameObject _spawn)
    {
        Instantiate(_spawn, new Vector3(Random.Range(spawn_space.xMin, spawn_space.xMax), 5f, 0), Quaternion.identity);
    }
}
