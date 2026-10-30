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
            // PlayerMovement.GainHealth(5f);
            Destroy(_other.gameObject);
        }
    }
}