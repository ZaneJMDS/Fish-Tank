/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Player aiming / shooting component
File name   :   PlayerLook.cs
**************************************************************************/

using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent] // Unity is stupid, Essentially pragma once

public class PlayerLook : Look
{
    // Sprites for guns
    public GameObject Gun;
    private SpriteRenderer gun_sprite_renderer;

    public Sprite BasicGun;
    public Sprite MiniGun;
    public Sprite LaserGun;

    // Sprites for firing
    public GameObject TurretFire;
    private SpriteRenderer turret_fire_sprite_renderer;

    // For projectiles
    public GameObject projectile;

    [SerializeField] private float overheat_timer = 0;

    InputAction attack_action;

    // Timers
    public bool primary_fire = true;
    public bool secondary_fire = true;
    private float primary_timer;
    private float secondary_timer;

    // For Lerp
    [SerializeField] private float lerp_speed = 1f;
    private Vector3 currentAngle;

    // Line
    public LineRenderer target_line;

    void Start()
    {
        attack_action = InputSystem.actions.FindAction("Attack");
        gun_sprite_renderer = Gun.GetComponent<SpriteRenderer>(); // Change the gun sprite
        turret_fire_sprite_renderer = TurretFire.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate towards mouse
        currentAngle = transform.eulerAngles; // Update the current angle each frame
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get mouse position relative to the screen
        Vector2 target_direction = (mouse_pos - transform.position); // Get Direction
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(currentAngle.z, FaceTarget(target_direction), Time.deltaTime * lerp_speed)); // Lerp so not instant

        // Convert current rotation to direction
        float angle_rad = (currentAngle.z + 90) * Mathf.Deg2Rad;
        Vector2 current_direction = new(Mathf.Cos(angle_rad), Mathf.Sin(angle_rad));

        // Draw a line from the player to current direction
        // target_line.SetPosition(0, transform.position);
        // target_line.SetPosition(1, current_direction);

        // Primary fire delay
        if (!primary_fire)
        {
            primary_timer += Time.deltaTime;
            if (primary_timer > projectile.GetComponent<MinigunBehaviour>().time_between_firing)
            {
                turret_fire_sprite_renderer.enabled = false;
                primary_fire = true;
                primary_timer = 0f;
            }
        }

        // Secondary fire delay
        if (!secondary_fire)
        {
            secondary_timer += Time.deltaTime;
            if (secondary_timer > HitscanBehaviour.time_between_firing)
            {
                turret_fire_sprite_renderer.enabled = false;
                secondary_fire = true;
                secondary_timer = 0f;
            }
        }

        // Left MB held
        if (attack_action.IsPressed() && primary_fire && !Input.GetMouseButton(1))
        {
            primary_fire = false;
            turret_fire_sprite_renderer.enabled = true;

            gun_sprite_renderer.sprite = MiniGun;

            // Summon a projectile at the player's position and where they are facing
            Quaternion bullet_offset = transform.rotation * Quaternion.Euler(0, 0, 270);
            Instantiate(projectile, transform.position, bullet_offset);
        }

        // Right MB held
        if (Input.GetMouseButton(1) && secondary_fire && !Input.GetMouseButton(0))
        {
            secondary_fire = false;
            turret_fire_sprite_renderer.enabled = true;

            gun_sprite_renderer.sprite = LaserGun;

            // Hitscan holding all enemies hit
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, current_direction);

            // PIERCING LETS FREAKING GO
            for (int i = 0; i < hit.Length; i++)
            {
                // Only care about enemies
                if (hit[i].collider.CompareTag("Enemies"))
                {
                    Debug.Log("Hit" + hit[i].collider.gameObject.name);
                    hit[i].collider.gameObject.GetComponent<EnemyLook>().health -= HitscanBehaviour.damage; // Decrease the enemy's health if hit
                }
            }
        }
    }
}
