using UnityEngine;

// Get edge of the play area
public class CameraBehaviour : MonoBehaviour
{
    public Rect cameraRect;

    void Awake()
    {
        // Edge of the screen
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        cameraRect = new Rect(-4f, bottomLeft.y, 9f - -4f, topRight.y - bottomLeft.y);
    }
}
