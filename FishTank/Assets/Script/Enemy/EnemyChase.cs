using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float speed;
    Vector3 target_position;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] bool explosive;

    // Update is called once per frame
    void Update()
    {
        // Chase the player
        target_position = GameObject.Find("PlayerBase").transform.position;
        Vector2 target_direction = (target_position - transform.position).normalized;
        rb.linearVelocity = new Vector2(target_direction.x, target_direction.y) * speed;

        if (explosive)
        {

        }
    }

    public void Detonate()
    {

    }
}
