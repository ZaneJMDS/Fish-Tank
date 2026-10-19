/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Default hitscan behaviour
File name   :   HitscanBehaviour.cs
**************************************************************************/


using UnityEngine;

public class HitscanBehaviour : MonoBehaviour
{
    [SerializeField] public int damage = 2;
    [SerializeField] public float time_between_firing = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundEffectManager.Play("Projectile"); // Play projectile sounds when object is called
    }

    // Update is called once per frame
    void Update()
    {
        // When done destroy this game object
        Destroy(this.gameObject);
    }
}
