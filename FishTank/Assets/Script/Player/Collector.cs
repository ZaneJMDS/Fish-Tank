/***********************************************************************
Author      :	BMO dev
Description :   Player collision behaviour
**************************************************************************/

using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] GameObject ink;

    // Player collides with things
    private void OnTriggerEnter2D(Collider2D _other)
    {
        // Player picks up the bubble on collision
        if (_other.CompareTag("Bubbles"))
        {
            transform.parent.GetComponent<PlayerMovement>().GainHealth(10f);
            Destroy(_other.gameObject);
            SoundEffectManager.Play("Bubble");
        }

        // Player takes damage on collision with enemies
        if (_other.CompareTag("Enemies"))
        {
            transform.parent.GetComponent<PlayerMovement>().GainHealth(-20f);
            _other.GetComponent<EnemyLook>().SummonHitExplosion();

            // Summon an ink splat in the play area
            if (_other.GetComponent<EnemyLook>().ink_splat)
            {
                Rect rec = transform.parent.GetComponent<CameraBehaviour>().cameraRect;
                Instantiate(ink, new Vector2(Random.Range(rec.xMin, rec.xMax), Random.Range(rec.yMin, rec.yMax)), Quaternion.identity);
            }

            Destroy(_other.gameObject);
            SoundEffectManager.Play("Hit");
        }

        // Player gets hit by projectile
        if (_other.CompareTag("EnemyProjectile"))
        {
            transform.parent.GetComponent<PlayerMovement>().GainHealth(-_other.GetComponent<ProjectileBehaviour>().damage);
            Destroy(_other.gameObject);
            SoundEffectManager.Play("Hit");
        }
    }
}