using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bubble>(out Bubble bubble))
        {
            bubble.SetTarget(transform.parent.position);
        }
    }
}
