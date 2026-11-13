/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Player shoots at current direction
File name   :   PlayerShoot.cs
**************************************************************************/

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    // For projectiles
    public GameObject projectile;

    [SerializeField] private float overheat_timer = 0; // TODO: Implement

    [SerializeField] GameObject gun_text;

    InputAction attack_action;
    InputAction switch_action;

    // Timers
    public bool primary_fire = true;
    public bool secondary_fire = true;
    private float primary_timer = 0f;
    private float secondary_timer = 0f;

    private Vector3 currentAngle;

    // Change sprites for guns
    public GameObject Gun;
    private SpriteRenderer gun_sprite_renderer;
    [SerializeField] GameObject[] gun_sprites;
    int current_gun = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the actions for input
        attack_action = InputSystem.actions.FindAction("Attack");
        switch_action = InputSystem.actions.FindAction("Interact");

        gun_sprite_renderer = Gun.GetComponent<SpriteRenderer>(); // Change the gun sprite
        GetComponent<SpriteRenderer>().enabled = false; // Turret fire
    }

    // Update is called once per frame
    void Update()
    {
        // Convert current rotation to direction
        currentAngle = transform.eulerAngles; // Update the current angle each frame
        float angle_rad = (currentAngle.z + 90) * Mathf.Deg2Rad;
        Vector2 current_direction = new(Mathf.Cos(angle_rad), Mathf.Sin(angle_rad));

        // Primary fire delay
        if (!primary_fire)
        {
            primary_timer += Time.deltaTime;

            if (primary_timer > projectile.GetComponent<ProjectileBehaviour>().time_between_firing)
            {
                GetComponent<SpriteRenderer>().enabled = false; // Turret fire
                primary_fire = true;
                primary_timer = 0f;
            }
        }

        // Secondary fire delay
        if (!secondary_fire)
        {
            secondary_timer += Time.deltaTime;

            // WHY IS THIS ALWAYS ACTIVE
            if (secondary_timer > HitscanBehaviour.time_between_firing)
            {
                GetComponent<SpriteRenderer>().enabled = false; // Turret fire
                secondary_fire = true;
                secondary_timer = 0f;
            }
        }

        // Fire
        if (attack_action.IsPressed())
        {
            // Primary
            if (current_gun == 0 && primary_fire)
            {
                primary_fire = false;

                GetComponent<SpriteRenderer>().enabled = true; // Turret 

                // Summon a projectile at the player's position and where they are facing
                Quaternion bullet_offset = transform.rotation * Quaternion.Euler(0, 0, 270);
                Instantiate(projectile, transform.position, bullet_offset);
            }

            // Secondary
            if (current_gun == 1 && secondary_fire)
            {
                SoundEffectManager.Play("Lazer");

                secondary_fire = false;

                GetComponent<SpriteRenderer>().enabled = true; // turret fire

                // Hitscan holding all enemies hit
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, current_direction);

                // PIERCING!
                for (int i = 0; i < hits.Length; i++)
                {
                    // Only care about enemies
                    if (hits[i].collider.CompareTag("Enemies"))
                    {
                        hits[i].collider.gameObject.GetComponent<EnemyLook>().health -= HitscanBehaviour.damage; // Decrease the enemy's health if hit
                        hits[i].collider.gameObject.GetComponent<EnemyLook>().SummonHitExplosion();
                    }
                }
            }
        }

        // Switch action is only pressed once
        if (switch_action.WasPressedThisFrame())
        {
            // Move Current Gun Left and New Gun Right
            gun_sprites[current_gun].transform.position = new Vector2(-8f, gun_sprites[current_gun].transform.position.y);
            gun_sprites[current_gun].transform.localScale = new Vector2(0.5f, 0.5f);

            if (current_gun < gun_sprites.Length - 1)
            {
                current_gun++;
            }

            // Loop back to the start
            else 
            {
                current_gun = 0;
            }

            gun_sprite_renderer.sprite = gun_sprites[current_gun].GetComponent<SpriteRenderer>().sprite;
            gun_text.GetComponent<TextMeshProUGUI>().text = "Current weapon: " + gun_sprites[current_gun].name;
            gun_sprites[current_gun].transform.position = new Vector2(-6f, gun_sprites[current_gun].transform.position.y);
            gun_sprites[current_gun].transform.localScale = new Vector2(0.75f, 0.75f);
        }
    }
}
