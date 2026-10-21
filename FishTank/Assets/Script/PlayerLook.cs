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
    // For projectiles
    public GameObject projectile;
    public GameObject hitscan;

    // Stats
    public bool primary_fire = true;
    public bool secondary_fire = true;
    private float primary_timer;
    private float secondary_timer;

    // For Lerp
    [SerializeField] private float lerp_speed = 1f;
    private Vector3 currentAngle;

    // Line
    public LineRenderer target_line;

    // Update is called once per frame
    void Update()
    {
        // Rotate towards the mouse
        currentAngle = transform.eulerAngles; // Update the current angle each frame
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get mouse position relative to the screen
        Vector2 target_direction = (mouse_pos - transform.position); // Convert to 2D
        float angle_deg = FaceTarget(target_direction);
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(currentAngle.z, angle_deg, Time.deltaTime * lerp_speed)); // Lerp so not instant

        // Convert current rotation to radians
        float angle_rad = (currentAngle.z + 90) * Mathf.Deg2Rad;

        // Current direction
        Vector2 current_direction = new(Mathf.Cos(angle_rad), Mathf.Sin(angle_rad));

        // Draw a line from the player to the mouse
        target_line.SetPosition(0, transform.position);
        target_line.SetPosition(1, mouse_pos);
        Debug.DrawRay(transform.position, current_direction);

        // Primary fire delay
        if (!primary_fire)
        {
            primary_timer += Time.deltaTime;
            if (primary_timer > projectile.GetComponent<MinigunBehaviour>().time_between_firing)
            {
                primary_fire = true;
                primary_timer = 0f;
            }
        }

        // Secondary fire delay
        if (!secondary_fire)
        {
            secondary_timer += Time.deltaTime;
            if (secondary_timer > hitscan.GetComponent<HitscanBehaviour>().time_between_firing)
            {
                secondary_fire = true;
                secondary_timer = 0f;
            }
        }

        // Left MB held
        if (Input.GetMouseButton(0) && primary_fire && !Input.GetMouseButton(1))
        {
            primary_fire = false;

            // Summon a projectile at the player's position and where they are facing
            Quaternion bullet_offset = transform.rotation * Quaternion.Euler(0, 0, 270);
            Instantiate(projectile, transform.position, bullet_offset);
        }

        // Right MB held
        if (Input.GetMouseButton(1) && secondary_fire && !Input.GetMouseButton(0))
        {
            secondary_fire = false;

            // Hitscan
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, current_direction);

            // PIERCING LETS FREAKING GO
            for (int i = 0; i < hit.Length; i++)
            {
                // Only care about enemies
                if (hit[i].collider.CompareTag("Enemies"))
                {
                    Debug.Log("Hit" + hit[i].collider.gameObject.name);
                    hit[i].collider.gameObject.GetComponent<EnemyLook>().health -= hitscan.GetComponent<HitscanBehaviour>().damage; // Decrease the enemy's health if hit
                }
            }
        }
    }
}
