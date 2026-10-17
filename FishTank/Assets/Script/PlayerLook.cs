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
    public bool can_fire = true;
    private float timer;
    [SerializeField] public float time_between_firing = 0.5f;

    // For Lerp
    [SerializeField] private float lerp_speed = 1f;
    private Vector3 currentAngle;

    // Update is called once per frame
    void Update()
    {
        currentAngle = transform.eulerAngles; // Update the current angle each frame

        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get mouse position relative to the screen
        Vector2 direction = (mouse_pos - transform.position); // Convert to 2D

        float angle_deg = FaceTarget(direction);
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(currentAngle.z, angle_deg, Time.deltaTime * lerp_speed));

        Debug.DrawLine(transform.position, mouse_pos, Color.white, Time.deltaTime); // Draw a line from the player to the mouse

        // Delay between firing
        if (!can_fire)
        {
            timer += Time.deltaTime;
            if (timer > time_between_firing)
            {
                can_fire = true;
                timer = 0f;
            }
        }

        // Left MB held
        if (Input.GetMouseButton(0) && can_fire)
        {
            can_fire = false;
            // Summon a bullet at the player's position and where they are facing
            Quaternion bullet_offset = transform.rotation * Quaternion.Euler(0, 0, 270);
            Instantiate(projectile, transform.position, bullet_offset);
        }
    }
}
