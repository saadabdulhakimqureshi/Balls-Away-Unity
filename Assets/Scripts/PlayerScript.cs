using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public new Transform transform;
    public Animator animator;
    float speed;
    int health;
    int deathHealth;
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject enemies;
    [SerializeField] Joystick joystick;

    void Start()
    {
        speed = 5f;
        health = 5;
        deathHealth = 0;
        healthBar.GetComponent<Slider>().value = 5;
    }

    void UpdateSlider()
    {
        healthBar.GetComponent<Slider>().value = health;
    }

    public void loseHealth()
    {
        health--;
        UpdateSlider();
        if (health > deathHealth)
        {
            Vector3 scaleDown = new Vector3(0.1f, 0.1f, 0);

            transform.localScale -= scaleDown;
            enemies.GetComponent<Movement>().speed+=0.2f;
        }

        if (health == deathHealth)
        {
            animator.enabled = true;
            Invoke("End", 1f);
        }
    }

    void End()
    {
        animator.enabled = false;
        gameManager.GetComponent<Main>().EndGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
            movement();
    }

    void movement()
    {
        Transform transform = GetComponent<Transform>();
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                joystick.GetComponent<Transform>().position = touch.position;
            }
        }
        transform.Translate(Vector2.right * joystick.Horizontal * 0.1f);
        transform.Translate(Vector2.up * joystick.Vertical * 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
