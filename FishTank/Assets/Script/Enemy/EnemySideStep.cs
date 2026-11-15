/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Enemy that moves back and forth at the top of the screen
**************************************************************************/

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
        rec = GetComponent<CameraBehaviour>().cameraRect;
        transform.position -= new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move Right
        if (right)
        {
            transform.position += speed * Time.deltaTime * -transform.right;

            // Right side reached, move left
            if (transform.position.x < rec.xMin)
            {
                right = false;
            }
        }

        // Move Left
        else
        {
            transform.position += speed * Time.deltaTime * transform.right;

            // Left side reached, move right
            if (transform.position.x > rec.xMax)
            {
                right = true;
            }
        }
    }
}
