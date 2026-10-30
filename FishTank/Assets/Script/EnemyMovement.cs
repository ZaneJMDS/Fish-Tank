using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        // Move staright down the screen
        Vector3 velocity = new Vector3(0, -1, 0); 
        velocity *= speed;
        transform.position += velocity * Time.deltaTime;
    }
}
