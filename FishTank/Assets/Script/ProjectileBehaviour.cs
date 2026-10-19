/***********************************************************************
Author      :	Zane Jackson
Mail        :   Zane.Jackson@mds.ac.nz
Studio      :	FishTank games
Description :   Default projectile behaviour
File name   :   ProjectileBehaviour.cs
**************************************************************************/

using UnityEngine;

[DisallowMultipleComponent] // Unity is stupid, Essentially pragma once

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed = 6f;
    [SerializeField] public int damage = 2;
    public float time_between_firing = 0.5f;

    private CameraBehaviour camera_behaviour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SoundEffectManager.Play("Projectile"); // Play projectile sounds when object is called
        camera_behaviour = GetComponent<CameraBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        // Projectile move forward
        transform.position += -transform.right * Time.deltaTime * speed;

        // Destroy the projectile if it goes offscreen
        if (transform.position.x < camera_behaviour.cameraRect.xMin) { Destroy(this.gameObject); } // Left
        if (transform.position.x > camera_behaviour.cameraRect.xMax) { Destroy(this.gameObject); } // Right
        if (transform.position.y < camera_behaviour.cameraRect.yMin) { Destroy(this.gameObject); } // Up
        if (transform.position.y > camera_behaviour.cameraRect.yMax) { Destroy(this.gameObject); } // Down
    }
}
