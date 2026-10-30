/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Player moves around / other tank stuff
File name   :   PlayerMovement.cs
**************************************************************************/

using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent] // Unity is stupid, Essentially pragma once

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float move_speed = 4f;
    private float health;
    private const float max_health = 100f;

    public Slider health_slider;

    private Rigidbody2D rb; // Player can control this object
    SpriteRenderer sprite_renderer;
    private Vector2 move_input;
    private float sprite_radius;

    public GameObject Bubble; // Bubble exists in the world

    private CameraBehaviour camera_behaviour; // So player dosn't go out of bounds

    [SerializeField] Sprite[] player_sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = max_health;

        rb = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_radius = 0.05f * sprite_renderer.sprite.bounds.extents.x; // Unity's dumb radius math converting pixels to screen space

        camera_behaviour = GetComponent<CameraBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease health slowly over time
        if (health > 1f)
        {
            health -= 0.005f;
        }

        // If out of bounds of the camera, player can't move in that direction
        if (transform.position.x - sprite_radius < camera_behaviour.cameraRect.xMin && move_input.x < 0) { move_input.x = 0; } // Left
        if (transform.position.x + sprite_radius > camera_behaviour.cameraRect.xMax && move_input.x > 0) { move_input.x = 0; } // Right
        if (transform.position.y - sprite_radius < camera_behaviour.cameraRect.yMin && move_input.y < 0) { move_input.y = 0; } // Up
        if (transform.position.y + sprite_radius > camera_behaviour.cameraRect.yMax && move_input.y > 0) { move_input.y = 0; } // Down

        rb.linearVelocity = move_input * move_speed;

        // Update health indicators
        // Full health
        if (health >= 6f)
        {
            sprite_renderer.sprite = player_sprites[0];
        }

        // Medium health
        if (health < 6f)
        {
            sprite_renderer.sprite = player_sprites[1];
        }

        // Low health
        if (health < 3f)
        {
            sprite_renderer.sprite = player_sprites[2];
        }

        // Player is dead, take them to start menu
        if (health < 0f)
        {
            SceneManager.LoadScene("Menu");
        }

        // Print health to slider
        health_slider.value = health;
    }

    // Move in direction held
    public void Move(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }

    // Player collides with things
    private void OnTriggerEnter2D(Collider2D _other)
    {
        // Player takes damage on collision with enemies
        if (_other.CompareTag("Enemies"))
        {
            health--;
        }
    }

    // Change player health
    public void GainHealth(float _health)
    {
        if (health < max_health) { health += _health; }
    }
}