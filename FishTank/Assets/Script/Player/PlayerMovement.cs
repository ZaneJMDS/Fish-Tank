/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Player moves around / other tank stuff
File name   :   PlayerMovement.cs
**************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent] // Unity is stupid, Essentially pragma once

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float max_move_speed;
    private float move_speed = 5f;

    public float health;
    private const float max_health = 100f;
    public Slider health_slider;

    private Rigidbody2D rb; // Player can control this object
    SpriteRenderer sprite_renderer;
    private Vector2 move_input;
    private float sprite_radius;

    private CameraBehaviour camera_behaviour; // So player dosn't go out of bounds

    [SerializeField] Sprite[] player_sprites; // Switch between health sprites
    
    [SerializeField] GameObject button_manager;
    [SerializeField] GameObject game_over_text;

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
        if (health >= 0)
        {
            button_manager.SetActive(false);
            game_over_text.SetActive(false);

            // Decrease health slowly over time
            if (health > 1f)
            {
                health -= 0.002f;
            }

            // TODO Increase move speed if held
           /* if (move_speed < max_move_speed)
            {
                move_speed += Time.deltaTime * max_move_speed / 10;
            }*/

            // If out of bounds of the camera, player can't move in that direction
            if (transform.position.x - sprite_radius < camera_behaviour.cameraRect.xMin && move_input.x < 0) { move_input.x = 0; } // Left
            if (transform.position.x + sprite_radius > camera_behaviour.cameraRect.xMax && move_input.x > 0) { move_input.x = 0; } // Right
            if (transform.position.y - sprite_radius < camera_behaviour.cameraRect.yMin && move_input.y < 0) { move_input.y = 0; } // Up
            if (transform.position.y + sprite_radius > camera_behaviour.cameraRect.yMax && move_input.y > 0) { move_input.y = 0; } // Down
            

            /*if (move_input.x == 0 && move_input.y == 0)
            {
                if (move_speed > 0)
                {
                    move_speed -= Time.deltaTime * max_move_speed / 10;
                }
            }*/

            // Update health indicators
            // Full health
            if (health >= max_health / 2)
            {
                sprite_renderer.sprite = player_sprites[0];
            }

            // Medium health
            if (health < max_health / 2)
            {
                sprite_renderer.sprite = player_sprites[1];
            }

            // Low health
            if (health < max_health / 4)
            {
                sprite_renderer.sprite = player_sprites[2];
            }
        }

        // Player is dead
        else
        {
            // Activate buttons and game over text here
            button_manager.SetActive(true);
            game_over_text.SetActive(true);

            move_input = Vector2.zero;
        }

        // Print health to slider
        health_slider.value = health;
        rb.linearVelocity = move_input * move_speed; // Current speed
    }

    // Move in direction held
    public void Move(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }

    // Change player health
    public void GainHealth(float _health)
    {
        if (health < max_health) { health += _health; }
        if (health > max_health) { health = max_health; }
    }

    public void Reset()
    {
        health = max_health;
        transform.position = new Vector2(0, -3);
    }
}