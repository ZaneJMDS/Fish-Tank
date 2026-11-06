using UnityEngine;

public class InkBehaviour : MonoBehaviour
{
    float timer = 2f;
    [SerializeField] Sprite[] ink_sprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = ink_sprites[Random.Range(0, ink_sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, timer);

        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
