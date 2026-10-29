using UnityEngine;

// Get edge of the play area
public class CameraBehaviour : MonoBehaviour
{
    public Rect cameraRect;

    void Awake()
    {
        // Camera restrictions
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));
        cameraRect = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
    }
}
