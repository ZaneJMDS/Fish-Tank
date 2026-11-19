/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Enemy moves to the side or down the screen
**************************************************************************/

using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool obstacle;
    [SerializeField] private bool move_side;
    
    private bool move_right;

    private void Start()
    {
        if (transform.position.x > 0)
        {
            move_right = false;
        }

        else
        {
            move_right = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity;

        if (move_side)
        {
            if (move_right)
            {
                velocity = new Vector3(1, 0, 0);
            }

            // Move left
            else
            {
                velocity = new Vector3(-1, 0, 0);
            }
        }

        // Move straight down the screen
        else
        {
            velocity = new Vector3(0, -1, 0);
        }
        
        velocity *= speed;
        transform.position += velocity * Time.deltaTime;

        // If enemy is too far down or to the side kill them.
        if (transform.position.y < -6.0f || transform.position.x < -10f || transform.position.x > 10f)
        {
            Destroy(this.gameObject);
        }
    }

    // If the enemy gets hit by something 
    private void OnTriggerEnter2D(Collider2D _other)
    {
        // If hit by a projectile, decrease health and increase score
        if (_other.CompareTag("Projectile") && obstacle)
        {
            Destroy(_other.gameObject);
        }
    }
}
