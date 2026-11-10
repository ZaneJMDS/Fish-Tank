/***********************************************************************
Author      :	BMO dev
Description :   Player collision behaviour
**************************************************************************/

using UnityEngine;

public class Collector : MonoBehaviour
{
    // Player collides with things
    private void OnTriggerEnter2D(Collider2D _other)
    {
        // Player picks up the bubble on collision
        if (_other.CompareTag("Bubbles"))
        {
            transform.parent.GetComponent<PlayerMovement>().GainHealth(100f);
            Destroy(_other.gameObject);
        }

        // Player takes damage on collision with enemies
        if (_other.CompareTag("Enemies"))
        {
            transform.parent.GetComponent<PlayerMovement>().GainHealth(-10f);
            _other.GetComponent<EnemyLook>().SummonHitExplosion();
            Destroy(_other.gameObject);
        }

        // Player gets hit by projectile
        if (_other.CompareTag("EnemyProjectile"))
        {
            transform.parent.GetComponent<PlayerMovement>().GainHealth(_other.GetComponent<ProjectileBehaviour>().damage);
            Destroy(_other.gameObject);
        }
    }
}