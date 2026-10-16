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
    [SerializeField] private int health = 10;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float lerp_speed = 2f;
    [SerializeField] private float time_between_firing = 0.5f;

    private Vector3 currentAngle;
    public GameObject player; // Needs to know the player exists
    private PlayerLook player_look;
    public GameObject obj_game_manager;
    private GameManager game_manager;

    public GameObject bubble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Theres probbably a better way, blind leads the blind
        player_look = player.GetComponent<PlayerLook>();

        obj_game_manager = GameObject.FindGameObjectWithTag("Game Manager");
        game_manager = obj_game_manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = transform.eulerAngles; // Update the current angle each frame
        Vector2 direction = (player.transform.position - transform.position); // Convert to 2D

        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(currentAngle.z, FaceTarget(direction), Time.deltaTime * lerp_speed));

        // If health is less than or equal to 0 you shall be gone
        if (health <= 0)
        {
            // Spawn a bubble where the enemy died
            Instantiate(bubble, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    // If the enemy gets hit by something 
    private void OnTriggerEnter2D(Collider2D _other)
    {
        // If hit by a projectile, decrease health and increase score
        if (_other.CompareTag("Projectile"))
        {
            // Communicate with AddScore method and add random score between 5-15
            Destroy(_other.gameObject);
            game_manager.AddScore(Random.Range(5, 15));

            health -= _other.gameObject.GetComponent<ProjectileBehaviour>().damage; // WHY UNITY?
        }
    }
}
