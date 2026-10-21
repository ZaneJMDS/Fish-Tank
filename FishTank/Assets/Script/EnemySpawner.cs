using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject basic_enemy;
    public GameObject loot_fish;
    
    [SerializeField] private float interval; // Time between spawns
    private float timer = 0f;

    // Count enemies
    GameObject[] enemies;
    int enemy_count = 0;

    // Update is called once per frame
    void Update()
    {

        // Count enemies
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        enemy_count = enemies.Length;

        // Spawn enemies on a timer
        timer += Time.deltaTime;

        if (timer >= interval && enemy_count < 10)
        {
            timer = 0f;
            Instantiate(basic_enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
            Instantiate(loot_fish, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        }
    }
}
