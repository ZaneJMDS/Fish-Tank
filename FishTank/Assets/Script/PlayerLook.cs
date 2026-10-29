/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Player aims at where the mouse is pointing
File name   :   PlayerLook.cs
**************************************************************************/

using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent] // Unity is stupid, Essentially pragma once

public class PlayerLook : Look
{
    // For Lerp
    [SerializeField] private float lerp_speed = 1f;
    private Vector3 currentAngle;

    // Line
    public LineRenderer target_line;

    // Update is called once per frame
    void Update()
    {
        // Rotate towards mouse
        currentAngle = transform.eulerAngles; // Update the current angle each frame
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Get mouse position relative to the screen
        Vector2 target_direction = (mouse_pos - transform.position); // Get Direction
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(currentAngle.z, FaceTarget(target_direction), Time.deltaTime * lerp_speed)); // Lerp so not instant

        // Draw a line from the player to current direction
        // target_line.SetPosition(0, transform.position);
        // target_line.SetPosition(1, current_direction);
    }
}
