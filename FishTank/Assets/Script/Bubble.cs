using UnityEngine;

public class Bubble : MonoBehaviour
{
    bool has_target;
    Vector3 target_position;
    [SerializeField] public Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (has_target) 
        { 
            Vector2 target_direction = (target_position - transform.position).normalized;
            rb.linearVelocity = new Vector2(target_direction.x, target_direction.y) * 5f;
        }
    }

    public void SetTarget(Vector3 _position)
    {
        target_position = _position;
        has_target = true;
    }
}
