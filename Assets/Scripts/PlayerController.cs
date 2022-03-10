using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 input;
    Rigidbody2D rb;
    public GameObject firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    float rotateSpeed;
    public int shootCounter;
    bool outOfControl;
    public float rampageSpeed;
    public static PlayerController instance;
    UIScript ui;
    public int health;
    public Sprite full;
    public Sprite damaged;
    public GameOver gameOver;
    public GameObject sparks;
    public Transform aheadTarget;
    float timeElapsed;
    bool invin;
    public Color normalColor;
    public Color flashColor;
    public SpriteRenderer sr;
    public GameObject shield;

    private void Awake()
    {
        instance = this;
        timeElapsed = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ui = UIScript.instance;
        rotateSpeed = 1;
        shootCounter = 10;
        sr = GetComponent<SpriteRenderer>();
        AudioManager.instance.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        rotateSpeed = Mathf.PingPong(timeElapsed, 5) + 1; //Increments and decrecements rotate speed between 1 and 10
        if (outOfControl) return;
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            input.x = Input.GetAxisRaw("Horizontal");
        }

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            input.y = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = input * moveSpeed;
        if (!outOfControl)
        {
            transform.Rotate(Vector3.forward * rotateSpeed);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = (firePoint.transform.position - transform.position) * bulletSpeed; //Shoot in the direction the player is facing
        Destroy(bullet, 1);
        //Debug.Log(firePoint.transform.position - transform.position);
        shootCounter--;
        ui.UpdateBullets();
        if(shootCounter <= 0)
        {
            StartCoroutine(RampageCo());
        }
    }

    IEnumerator RampageCo()
    {
        outOfControl = true;
        input = Vector2.zero;
        input = (firePoint.transform.position - transform.position) * rampageSpeed;
        Debug.Log("Rampage speed: " + input);
        yield return new WaitForSeconds(1);
        input = Vector2.zero;
        outOfControl = false;
        shootCounter = 10;
        ui.UpdateBullets();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (invin) return;
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().hit = true;
            health--;
            if(health == 2)
            {
                shield.SetActive(false);
            }
            else if(health == 1)
            {
                sr.sprite = damaged;
            }
            AudioManager.instance.Play("Hit");
            Instantiate(sparks, transform.position, Quaternion.identity);
            StartCoroutine(InvinCo());
            if (health <= 0)
            {
                gameOver.EndGame();
            }
        }
    }

    IEnumerator InvinCo()
    {
        invin = true;
        int temp = 0;
        while (temp < 4)
        {
            sr.color = flashColor;
            yield return new WaitForSeconds(0.25f);
            sr.color = normalColor;
            yield return new WaitForSeconds(0.25f);
            temp += 1;
        }
        temp = 0;
        invin = false;
    }
}
