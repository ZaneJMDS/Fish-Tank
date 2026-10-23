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
    // Enemy stat field
    [SerializeField] public int health = 10;
    [SerializeField] private float lerp_speed = 2f;

    // For projectiles
    private float timer;

    private Vector3 currentAngle;
    public GameObject player; // Needs to know where the player is

    public GameObject bubble; // Spawns this on death

    public GameObject projectile;

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
        if (timer > projectile.GetComponent<MinigunBehaviour>().time_between_firing)
        {
            timer = 0f;

            // Summon a projectile at the player's position and where they are facing
            Quaternion bullet_offset = transform.rotation * Quaternion.Euler(0, 0, 270 + Random.Range(-30, 30));
            // Instantiate(projectile, transform.position, bullet_offset);

        }

        // If health is less than or equal to 0 you shall be gone
        if (health <= 0)
        {
            // TODO Spawn more bubbles if the fish is a loot fish
            if (true)
            {
                Instantiate(bubble, new(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1), transform.position.z), Quaternion.identity);
            }

            // Spawn a bubble where the enemy died
            Instantiate(bubble, transform.position, Quaternion.identity);

            GameManager.AddScore(Random.Range(5, 15));

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
            health -= _other.gameObject.GetComponent<MinigunBehaviour>().damage; // TODO SUPPORT OTHER WEAPONS
        }
    }
}
