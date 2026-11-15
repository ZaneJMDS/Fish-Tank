/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Enemy moves down the screen
**************************************************************************/

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool bone_wall;

    // Update is called once per frame
    void Update()
    {
        // Move straight down the screen
        Vector3 velocity = new Vector3(0, -1, 0); 
        velocity *= speed;
        transform.position += velocity * Time.deltaTime;

        // If enemy is too far down kill them.
        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    // If the enemy gets hit by something 
    private void OnTriggerEnter2D(Collider2D _other)
    {
        // If hit by a projectile, decrease health and increase score
        if (_other.CompareTag("Projectile") && bone_wall)
        {
            Destroy(_other.gameObject);
        }
    }
}
