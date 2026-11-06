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
            // Increase health
            transform.parent.GetComponent<PlayerMovement>().GainHealth(5f);
            Destroy(_other.gameObject);
        }

        // Player takes damage on collision with enemies
        if (_other.CompareTag("Enemies"))
        {
            // Decrease health
            transform.parent.GetComponent<PlayerMovement>().GainHealth(-5f);
        }
    }
}