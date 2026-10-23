using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.forward * Time.deltaTime * speed;
    }
}
