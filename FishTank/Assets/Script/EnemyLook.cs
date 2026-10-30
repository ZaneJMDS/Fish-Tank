/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Enemy faces the player and fires projectile
File name   :   EnemyLook.cs
**************************************************************************/

using UnityEngine;
[DisallowMultipleComponent] // Unity is stupid, Essentially pragma once

public class EnemyLook : Look
{
    // Enemy stat field - Customise in the inspector
    [SerializeField] public int health;
    [SerializeField] private float lerp_speed;
    [SerializeField] private int enemy_score;
    [SerializeField] private int bubbles_dropped;

    // For projectiles
    private float timer;

    private Vector3 currentAngle;
    private GameObject player; // Needs to look at the player

    public GameObject bubble; // Spawns this on death

    public GameObject projectile;
    public GameObject hit_explosion;

    void Start()
    {
        player = GameObject.Find("PlayerBase");
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate to face player
        currentAngle = transform.eulerAngles; // Update the current angle each frame
        Vector2 target_direction = (player.transform.position - transform.position); // Convert to 2D
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(currentAngle.z, FaceTarget(target_direction), Time.deltaTime * lerp_speed));

        timer += Time.deltaTime;
        if (timer > projectile.GetComponent<ProjectileBehaviour>().time_between_firing)
        {
            timer = 0f;

            // Summon a projectile at the player's position and where they are facing
            Quaternion bullet_offset = transform.rotation * Quaternion.Euler(0, 0, 270 + Random.Range(-30, 30));

            // Current projectile will kill enemy
            // Instantiate(projectile, transform.position, bullet_offset);

        }

        // If health is less than or equal to 0 you shall be gone
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
    
    public void SummonHitExplosion()
    {
        Instantiate(hit_explosion, transform); // Summon explosion
    }
}
