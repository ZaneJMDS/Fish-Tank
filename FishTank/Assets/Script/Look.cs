/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Face a direction
**************************************************************************/

using UnityEngine;

public class Look : MonoBehaviour
{
    // Face a specific target by getting the angle
    public float FaceTarget(Vector2 _direction)
    {
        float angle_rad = Mathf.Atan2(_direction.y, _direction.x); // Find Z
        float angle_deg = (180 / Mathf.PI) * angle_rad - 90; // Convert from radians to degrees

        return angle_deg;
    }
}
