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
    bool sidestep = false;
    [SerializeField] float speed;
    Rect rec;
    float rand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rec = GetComponent<CameraBehaviour>().cameraRect;
        rand = Random.Range(2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        // Move down
        if (!sidestep)
        {
            transform.position -= new Vector3(0f, 0.01f, 0f);

            if (transform.position.y < rand)
            {
                sidestep = true;
            }
        }

        // Start sidestep
        else
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
}
