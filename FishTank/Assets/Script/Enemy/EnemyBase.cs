/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Variables and methods every Enemy should have
**************************************************************************/

using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Enemy stat field
    [SerializeField] public int health;
    [SerializeField] public int enemy_score;
    [SerializeField] public bool ink_splat;
    [SerializeField] public bool can_shoot;
    [SerializeField] private int bubbles_dropped;

    // Summon these game objects
    [SerializeField] private GameObject bubble;
    [SerializeField] private GameObject hit_explosion;
    [SerializeField] private GameObject projectile;

    private float timer;

    void Update()
    {
        // Is enemy dead?
        if (health <= 0)
        {
            // Spawn bubbles near where the enemy died
            for (int i = 0; i < bubbles_dropped; i++)
            {
                Instantiate(bubble, new Vector3(transform.position.x + Random.Range(-1, 2), transform.position.y + Random.Range(-1, 2), transform.position.z), Quaternion.identity);
            }

            GameManager.AddScore(enemy_score);

            Destroy(this.gameObject);
        }

        // Fire in facing direction
        if (can_shoot)
        {
            timer += Time.deltaTime;
            if (timer > projectile.GetComponent<ProjectileBehaviour>().time_between_firing)
            {
                timer = 0f;

                // Summon a projectile at the player's position and where they are facing
                Quaternion bullet_offset = transform.rotation * Quaternion.Euler(0, 0, 90 + Random.Range(-30, 30));

                // Current projectile will kill enemy
                Instantiate(projectile, transform.position, bullet_offset);

            }
        }
    }

    // If the enemy gets hit by something 
    private void OnTriggerEnter2D(Collider2D _other)
    {
        // If hit by a projectile, decrease health and increase score
        if (_other.CompareTag("Projectile"))
        {
            Destroy(_other.gameObject);
            SummonHitExplosion();

            health -= _other.gameObject.GetComponent<ProjectileBehaviour>().damage;
        }
    }

    // Summon explosion on this enemy when hit
    public void SummonHitExplosion()
    {
        Instantiate(hit_explosion, transform.position, transform.rotation);
    }
}
