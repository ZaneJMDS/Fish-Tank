using UnityEngine;

public class HitExplosionBehaviour : MonoBehaviour
{
    float alive_timer = 0;

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
