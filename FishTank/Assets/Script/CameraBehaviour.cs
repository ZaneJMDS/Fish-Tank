/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Get the play area
**************************************************************************/

using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Rect cameraRect;

    void Awake()
    {
        // Edge of the screen
        // Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        // Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        Bounds play_space = GetComponent<BoxCollider2D>().bounds;
        cameraRect = new Rect(play_space.min.x, play_space.min.y, play_space.max.x, play_space.max.y);
    }
}
