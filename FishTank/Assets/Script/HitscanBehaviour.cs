/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Default hitscan behaviour
File name   :   HitscanBehaviour.cs
**************************************************************************/


using UnityEngine;

// Stats for hitscan weapons
public class HitscanBehaviour : MonoBehaviour
{
    // Stat field
    public static int damage = 10;
    public static float time_between_firing = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundEffectManager.Play("Projectile"); // Play projectile sounds when object is called
    }
}
