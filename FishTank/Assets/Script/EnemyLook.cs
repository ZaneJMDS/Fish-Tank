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

    private Vector3 currentAngle;
    public GameObject player; // Needs to know the player exists
    private PlayerLook player_look;
    public GameObject obj_game_manager;
    private GameManager game_manager;

    public GameObject bubble; // Spawns this on death

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Theres probbably a better way, blind leads the blind
        player_look = player.GetComponent<PlayerLook>();

        game_manager = obj_game_manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = transform.eulerAngles; // Update the current angle each frame
        Vector2 target_direction = (player.transform.position - transform.position); // Convert to 2D

        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(currentAngle.z, FaceTarget(target_direction), Time.deltaTime * lerp_speed));

        // If health is less than or equal to 0 you shall be gone
        if (health <= 0)
        {
            // CHANGE LATER
            // Spawn more bubbles if the fish is a loot fish
            if (true)
            {
                Debug.Log("ImSPECIAL");
                Instantiate(bubble, new(transform.position.x + 2, transform.position.y + 2, transform.position.z), Quaternion.identity);
                Instantiate(bubble, new(transform.position.x - 2, transform.position.y - 2, transform.position.z), Quaternion.identity);
            }

            // Spawn a bubble where the enemy died
            Instantiate(bubble, transform.position, Quaternion.identity);

            game_manager.AddScore(Random.Range(5, 15));

            Destroy(this.gameObject);
        }
    }

    // If the enemy gets hit by something 
    private void OnTriggerEnter2D(Collider2D _other)
    {
        Debug.Log("EnemyHit");

        // If hit by a projectile, decrease health and increase score
        if (_other.CompareTag("Projectile"))
        {
            Destroy(_other.gameObject);
            health -= _other.gameObject.GetComponent<MinigunBehaviour>().damage; // TODO SUPPORT OTHER WEAPONS
        }
    }
}
