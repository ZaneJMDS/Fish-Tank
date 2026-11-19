/***********************************************************************
Author      :	Zane Jackson
Studio      :	FishTank games
Description :   Background scrolls and fades in and out
**************************************************************************/

using UnityEngine;

// Scroll the background
public class ScrollingBackground : MonoBehaviour
{
    Vector3 pos;
    private bool change_bg = false;
    private int current_background = 0;

    float timer = 2f;
    float end_timer = 5f;

    [SerializeField] GameObject end_screen;
    [SerializeField] GameObject button_manager;
    [SerializeField] private Sprite[] background_sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = new Vector3(transform.position.x, 5f, transform.position.z);
        transform.position = pos;


        end_screen.SetActive(false);
        button_manager.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Loop around to the start
        if (transform.position.y < -5f)
        {
            transform.position = pos;
        }

        // Fade in and out
        if (change_bg)
        {
            timer -= Time.deltaTime;

            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Mathf.Abs(timer));

            if (timer < 0f) { GetComponent<SpriteRenderer>().sprite = background_sprites[current_background]; }

            if (timer < -2f)
            {
                timer = 2f;
                change_bg = false;
            }
        }

        // Slow down to reach the pool
        if (current_background == background_sprites.Length - 1)
        {
            transform.position = pos;

            if (end_timer > 0f) { end_timer -= Time.deltaTime; }

            // Game has ended
            else 
            { 
                end_screen.SetActive(true); 
                button_manager.SetActive(true);            
            }

            transform.position -= new Vector3(transform.position.x, end_timer / 1000f, transform.position.z); // Move down but slowly
        }

        else
        {
            transform.position -= new Vector3(transform.position.x, 0.001f, transform.position.z); // Move down
        }
    }

    // Switch to the next background
    public void NextBackground()
    {
        change_bg = true;
        current_background++;
    }
}
