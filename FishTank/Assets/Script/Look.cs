/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Math to face a direction
File name   :   Look.cs
**************************************************************************/

using UnityEngine;

public class Look : MonoBehaviour
{
    // Face a specific target
    public float FaceTarget(Vector2 _direction)
    {
        float angle_rad = Mathf.Atan2(_direction.y, _direction.x); // Find Z
        float angle_deg = (180 / Mathf.PI) * angle_rad - 90; // Convert from radians to degrees

        return angle_deg;
    }
}
