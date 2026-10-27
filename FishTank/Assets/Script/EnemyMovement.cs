using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * -transform.forward;
    }
}
