using UnityEngine;

// Scroll the background
public class ScrollingBackground : MonoBehaviour
{
    Vector3 pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = new Vector3(transform.position.x, 5f, transform.position.z);
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(transform.position.x, 0.001f, transform.position.z); // Move down

        if (transform.position.y < -5f)
        {
            transform.position = pos;
        }
    }
}
