using UnityEngine;

public class BasicGunBehaviour : ProjectileBehaviour
{
    [SerializeField] public int damage = 1;
    public float time_between_firing = 0.01f;
}
