/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Get the play area (Dosn't have anything to do with camera)
**************************************************************************/

using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Rect cameraRect;

    void Awake()
    {
        // Edge of the screen
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        // Actual play space
        cameraRect = new Rect(-4f, bottomLeft.y, 12f, topRight.y - bottomLeft.y); // minX, minY, maxX, MaxY
    }
}
