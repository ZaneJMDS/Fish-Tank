/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Enemy faces the player and fires projectile
**************************************************************************/

using UnityEngine;
[DisallowMultipleComponent] // Unity is stupid, Essentially pragma once

public class EnemyLook : Look
{
    [SerializeField] private float lerp_speed;
    private Vector3 currentAngle;
    private GameObject player; // Needs to look at the player

    void Start()
    {
        player = GameObject.Find("PlayerBase");
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate to face player
        currentAngle = transform.eulerAngles; // Update the current angle each frame
        Vector2 target_direction = (player.transform.position - transform.position); // Convert to 2D
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.LerpAngle(currentAngle.z, FaceTarget(target_direction), Time.deltaTime * lerp_speed));
    }
}
