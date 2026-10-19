using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject basic_enemy;
    [SerializeField] private float interval;
    private float timer = 0f;

    GameObject[] enemies;
    int enemy_count = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
        }
    }
}
