/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Enemy chases the player
**************************************************************************/

using System.Threading;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] bool explosive;

    private bool ignited = false;
    private float timer = 0f;
    private Vector3 target_position;

    // Update is called once per frame
    void Update()
    {
        // Chase the player
        target_position = GameObject.Find("PlayerBase").transform.position;
        Vector2 target_direction = (target_position - transform.position).normalized;
        rb.linearVelocity = new Vector2(target_direction.x, target_direction.y) * speed;

        // Blow up adter a bit
        if (explosive)
        {
            // Will blow up soon
            if (ignited)
            {
                timer += Time.deltaTime;

                if (timer > 3f)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    // Set up explosive
    public void Detonate()
    {
        ignited = true;
    }
}
