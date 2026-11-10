using UnityEngine;

// Its only purpose is to die
// die and jig around a bit
public class HitExplosionBehaviour : MonoBehaviour
{
    float alive_timer = 0;

    void Start()
    {
        transform.position = transform.position + new Vector3 (Random.Range(-0.3f, 0.4f), Random.Range(-0.3f, 0.4f), transform.position.z);     // randomise x & y position
        transform.Rotate(transform.rotation.x, transform.rotation.y, Random.Range(0f,360f));                                                    // randomise rotation
        transform.localScale = transform.localScale + new Vector3 (Random.Range(-0.6f, 0f), Random.Range(-0.6f, 0f), transform.localScale.z);   // randomise scale
    }

    // Update is called once per frame
    void Update()
    {
        alive_timer += 0.01f;
        if (alive_timer > 1f)
        {
            Destroy(this.gameObject);
        }
    }
}
