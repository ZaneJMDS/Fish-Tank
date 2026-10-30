/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Player shoots at current direction
File name   :   PlayerShoot.cs
**************************************************************************/

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    // For projectiles
    public GameObject projectile;

    [SerializeField] private float overheat_timer = 0;

    InputAction attack_action;
    InputAction switch_action;

    // Timers
    public bool primary_fire = true;
    public bool secondary_fire = true;
    private float primary_timer;
    private float secondary_timer;

    private Vector3 currentAngle;

    // Change sprites for guns
    public GameObject Gun;
    private SpriteRenderer gun_sprite_renderer;
    [SerializeField] Sprite[] gun_sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the actions for input
        attack_action = InputSystem.actions.FindAction("Attack");
        switch_action = InputSystem.actions.FindAction("Interact");

        gun_sprite_renderer = Gun.GetComponent<SpriteRenderer>(); // Change the gun sprite
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
                GetComponent<SpriteRenderer>().enabled = false;
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
                GetComponent<SpriteRenderer>().enabled = false;
                secondary_fire = true;
                secondary_timer = 0f;
            }
        }

        // Primary Fire
        if (attack_action.IsPressed() && primary_fire && !switch_action.IsPressed())
        {
            primary_fire = false;

            GetComponent<SpriteRenderer>().enabled = true;
            gun_sprite_renderer.sprite = gun_sprites[1]; // Minigun

            // Summon a projectile at the player's position and where they are facing
            Quaternion bullet_offset = transform.rotation * Quaternion.Euler(0, 0, 270);
            Instantiate(projectile, transform.position, bullet_offset);
        }

        // Secondary fire
        if (switch_action.IsPressed() && secondary_fire && !attack_action.IsPressed())
        {
            secondary_fire = false;

            GetComponent<SpriteRenderer>().enabled = true;
            gun_sprite_renderer.sprite = gun_sprites[2]; // Laser gun

            // Hitscan holding all enemies hit
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, current_direction);

            // PIERCING LETS FREAKING GO
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
}
