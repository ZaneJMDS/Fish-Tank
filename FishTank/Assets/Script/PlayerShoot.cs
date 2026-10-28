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

    private SpriteRenderer turret_fire_sprite_renderer;
    private Vector3 currentAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the things
        attack_action = InputSystem.actions.FindAction("Attack");
        switch_action = InputSystem.actions.FindAction("Interact");
        turret_fire_sprite_renderer =  GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Convert current rotation to direction
        float angle_rad = (currentAngle.z + 90) * Mathf.Deg2Rad;
        Vector2 current_direction = new(Mathf.Cos(angle_rad), Mathf.Sin(angle_rad));

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

        // Primary Fire
        if (attack_action.IsPressed() && primary_fire && !switch_action.IsPressed())
        {
            primary_fire = false;
            turret_fire_sprite_renderer.enabled = true;

            // gun_sprite_renderer.sprite = MiniGun;

            // Summon a projectile at the player's position and where they are facing
            Quaternion bullet_offset = transform.rotation * Quaternion.Euler(0, 0, 270);
            Instantiate(projectile, transform.position, bullet_offset);
        }

        // Secondary fire
        if (switch_action.IsPressed() && secondary_fire && !attack_action.IsPressed())
        {
            secondary_fire = false;
            turret_fire_sprite_renderer.enabled = true;

            // gun_sprite_renderer.sprite = LaserGun;

            // Hitscan holding all enemies hit
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, current_direction);

            // PIERCING LETS FREAKING GO
            for (int i = 0; i < hits.Length; i++)
            {
                // Only care about enemies
                if (hits[i].collider.CompareTag("Enemies"))
                {
                    Debug.Log("Hit" + hits[i].collider.gameObject.name);
                    hits[i].collider.gameObject.GetComponent<EnemyLook>().health -= HitscanBehaviour.damage; // Decrease the enemy's health if hit
                }
            }
        }
    }
}
