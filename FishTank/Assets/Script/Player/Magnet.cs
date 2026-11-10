/***********************************************************************
Author      :	BMO dev
Description :   Pickup enters range of the player
**************************************************************************/

using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        // If bubble is in player's range
        if (collision.gameObject.TryGetComponent<Bubble>(out Bubble bubble))
        {
            bubble.SetTarget(transform.parent.position);
        }

        // If enemy is explosive, detonate
        if (collision.gameObject.TryGetComponent<EnemyChase>(out EnemyChase enemy))
        {
            enemy.Detonate();
        }
    }
}
