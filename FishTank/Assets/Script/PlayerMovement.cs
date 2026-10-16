/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Player moves around / other tank stuff
File name   :   PlayerMovement.cs
**************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent] // Unity is stupid, Essentially pragma once

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float move_speed = 4f;
    [SerializeField] private int health = 100;
    private Rigidbody2D rb; // Player can control this object
    SpriteRenderer sprite_renderer;
    private Vector2 move_input;
    private float sprite_radius;

    private GameObject Bubble; // Bubble exists in the world

    private CameraBehaviour camera_behaviour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_radius = 0.05f * sprite_renderer.sprite.bounds.extents.x; // Unity's dumb radius math converting pixels to screen space

        // find the bubble
        Bubble = GameObject.FindGameObjectWithTag("Bubbles");
        camera_behaviour = GetComponent<CameraBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // If out of bounds of the camera, player can't move in that direction
        if (transform.position.x - sprite_radius < camera_behaviour.cameraRect.xMin && move_input.x < 0) { move_input.x = 0; } // Left
        if (transform.position.x + sprite_radius > camera_behaviour.cameraRect.xMax && move_input.x > 0) { move_input.x = 0; } // Right
        if (transform.position.y - sprite_radius < camera_behaviour.cameraRect.yMin && move_input.y < 0) { move_input.y = 0; } // Up
        if (transform.position.y + sprite_radius > camera_behaviour.cameraRect.yMax && move_input.y > 0) { move_input.y = 0; } // Down

        rb.linearVelocity = move_input * move_speed;
    }

    // Move in direction held
    public void Move(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }


    private void OnTriggerEnter2D(Collider2D _other)
    {
        // Player picks up the bubble
        if (_other.CompareTag("Bubbles"))
        {
            health += 5;
            Destroy(_other.gameObject);
        }
    }
}