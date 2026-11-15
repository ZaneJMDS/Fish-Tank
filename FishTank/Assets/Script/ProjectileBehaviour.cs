/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Default projectile behaviour
**************************************************************************/

using UnityEngine;

[DisallowMultipleComponent] // Unity is stupid, Essentially pragma once

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public int damage;
    [SerializeField] public float time_between_firing;

    [SerializeField] GameObject play_space;
    private Rect move_space;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (FindAnyObjectByType<SoundEffectManager>() != null)
        {
            SoundEffectManager.Play("Projectile"); // Play projectile sounds when object is called
        }
        
        move_space = play_space.GetComponent<CameraBehaviour>().cameraRect;
    }

    // Update is called once per frame
    void Update()
    {
        // Projectile move forward
        transform.position += speed * Time.deltaTime * -transform.right;

        // Destroy the projectile if it goes offscreen
        if (transform.position.x < move_space.xMin) { Destroy(this.gameObject); } // Left
        if (transform.position.x > move_space.xMax) { Destroy(this.gameObject); } // Right
        if (transform.position.y < move_space.yMin) { Destroy(this.gameObject); } // Up
        if (transform.position.y > move_space.yMax) { Destroy(this.gameObject); } // Down
    }
}
