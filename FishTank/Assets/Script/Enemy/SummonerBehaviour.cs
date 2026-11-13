using Unity.VisualScripting;
using UnityEngine;

public class SummonerBehaviour : MonoBehaviour
{
    [SerializeField] GameObject summons;
    float timer = 0;
    bool can_summon = true;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Summon more enemies
        if (timer > 3 && can_summon)
        {
            Instantiate(summons, new Vector2(Random.Range(-1f, 1f) + transform.position.x, Random.Range(-1f, 1f) + transform.position.y), transform.rotation);
            Instantiate(summons, new Vector2(Random.Range(-1f, 1f) + transform.position.x, Random.Range(-1f, 1f) + transform.position.y), transform.rotation);

            can_summon = false;
        }
    }
}
