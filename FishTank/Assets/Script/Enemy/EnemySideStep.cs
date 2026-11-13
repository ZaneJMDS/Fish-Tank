using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemySideStep : MonoBehaviour
{
    bool right = true;
    [SerializeField] float speed;
    Rect rec;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rec = transform.parent.GetComponent<CameraBehaviour>().cameraRect;
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            transform.position += speed * Time.deltaTime * -transform.right;

            if (transform.position.x < rec.xMin)
            {
                right = false;
            }
        }

        // Left
        else
        {
            transform.position += speed * Time.deltaTime * transform.right;

            if (transform.position.x > rec.xMax)
            {
                right = true;
            }
        }
    }
}
