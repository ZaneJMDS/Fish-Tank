using UnityEngine;

// Scroll the background
public class ScrollingBackground : MonoBehaviour
{
    Vector3 pos;
    public bool change_bg = false;
    float timer = 2f;

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

        // Fade in and out
        if (change_bg)
        {
            timer -= Time.deltaTime;

            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Abs(timer));

            if (timer < -2)
            {
                timer = 2f;
                change_bg = false;
            }
        }
    }
}
