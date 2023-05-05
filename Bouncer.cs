using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bouncer : MonoBehaviour
{
    Rigidbody2D rb;
    public float maxVelocity = 13.0f;
    public float startSpeed = 8.0f;
    private string lastBounce;
    public Player player;
    private GameManager manager;
    public float paddingMultiplier = 1f;
    public float cornerPaddingMultiplier = 2f;
    System.Random r = new System.Random();
    public ParticleSystem bloodSplash;
    public int directionRange = 3;
    float previous = 0f;
    public Camera camera;
    private float width;
    private float height;
    private ScreenShake shake;
    bool vertical = false;
    bool damage = false;
    private bool set = false;

    public void Spawned()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(startSpeed, 0);
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        manager = player.gameObject.GetComponent<GameManager>();
        camera = GameObject.Find("Main Camera").gameObject.GetComponent<Camera>();
        shake = camera.gameObject.GetComponent<ScreenShake>();
    }
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(startSpeed, 0);
        player = GameObject.Find("/Player").gameObject.GetComponent<Player>();
        manager = player.gameObject.GetComponent<GameManager>();
        camera = GameObject.Find("/Main Camera").gameObject.GetComponent<Camera>();
        shake = camera.gameObject.GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    void Update()
    {
        float current = transform.position.x + transform.position.y;
        float velocity = (current - previous);
        float padding = velocity * paddingMultiplier;
        float corenerPadding = velocity * cornerPaddingMultiplier;
        bool corner = false;
        if (Screen.width != null && !set)
        {
            width = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0)).x;
            height = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0)).y;
            set = true;
        }
        previous = transform.position.x + transform.position.y;
        if (transform.position.x + padding >= width && lastBounce != "right")
        {
            if (transform.position.y - corenerPadding <= -height && transform.position.y - corenerPadding <= 0 && vertical == false)
            {
                rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y * -1) + 1);
                corner = true;
            }
            else if (transform.position.y + corenerPadding >= height && vertical == false)
            {
                rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y * -1) - 1);
                corner = true;
            }
            rb.velocity = new Vector2((rb.velocity.x * -1), rb.velocity.y);
            if (transform.position.y > 0 && corner == false && vertical == false)
            {
                if (transform.position.y < 3)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + r.Next(directionRange * -1, directionRange));
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + r.Next(directionRange * -1, 0));
                }
            }
            else if (corner == false && vertical == false)
            {
                if (transform.position.y > -3)
                {
                
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + r.Next(directionRange * -1, directionRange));
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + r.Next(0, directionRange));
                }
            }
            lastBounce = "right";
            if (manager.canScore)
            {
                manager.score++;
            }
            damage = true;
            vertical = false;
        }
        if (transform.position.x - padding <= -width && lastBounce != "left")
        {
            if (transform.position.y - corenerPadding <= -height && transform.position.y - corenerPadding <= 0 && vertical == false)
            {
                rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y * -1) + 1);
                corner = true;
            }else if(transform.position.y + corenerPadding >= height && vertical == false)
            {
                rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y * -1) - 1);
                corner = true;
            }
            rb.velocity = new Vector2((rb.velocity.x * -1), rb.velocity.y);
            if (transform.position.y > 0 && corner == false && vertical == false)
            {
                if (transform.position.y < 3)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + r.Next(directionRange * -1, directionRange));
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + r.Next(directionRange * -1, 0));
                }
            }
            else if (corner == false && vertical == false)
            {
                if (transform.position.y > -3)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + r.Next(directionRange * -1, directionRange));
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + r.Next(0, directionRange));
                }
            }
            lastBounce = "left";
            damage = true;
            if (manager.canScore)
            {
                manager.score++;
            }
        }
        vertical = false;
        if (transform.position.y + padding <= -height && lastBounce != "down" && !corner)
        {
            if (transform.position.x <= width - padding && transform.position.x >= -width + padding)
            {
                rb.velocity = new Vector2(rb.velocity.x + r.Next(-3, 3), (rb.velocity.y * -1));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y * -1));

            }
            lastBounce = "down";
            if (manager.canScore)
            {
                manager.score++;
            }
            vertical = true;
            damage = true;
        }
        else if (transform.position.y - padding >= height && lastBounce != "up" && !corner) 
        {
            if (transform.position.x <= width - padding && transform.position.x >= -width + padding)
            {
                rb.velocity = new Vector2(rb.velocity.x + r.Next(-3, 3), (rb.velocity.y * -1));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, (rb.velocity.y * -1));

            }
            lastBounce = "up";
            if (manager.canScore)
            {
                manager.score++;
            }
            damage = true;
            vertical = true;
        }
        if ((rb.velocity.x < maxVelocity * -1))
        {
            rb.velocity = new Vector2(maxVelocity * -1, rb.velocity.y);
        }
        if ((rb.velocity.x > maxVelocity))
        {
            rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
        }
        if ((rb.velocity.y < maxVelocity * -1))
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity * -1);
        }
        if ((rb.velocity.y > maxVelocity))
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity);
        }
        if (transform.position.x > width)
        {
            transform.position = new Vector2(width, transform.position.y);
            rb.velocity = new Vector2(-1 * startSpeed, rb.velocity.y);
        }
        if (transform.position.x < -width)
        {
            transform.position = new Vector2(-width, transform.position.y);
            rb.velocity = new Vector2(startSpeed, rb.velocity.y);
        }
        if (transform.position.y > height)
        {
            transform.position = new Vector2(transform.position.x, height);
            rb.velocity = new Vector2(rb.velocity.x, -1 * startSpeed);
        }
        if (transform.position.y < -height)
        {
            transform.position = new Vector2(transform.position.x, -height);
            rb.velocity = new Vector2(rb.velocity.x, startSpeed);
        }
        corner = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && damage == true)
        {
            if (manager.lastHit < manager.score - manager.bouncers )
            {
                if (PlayerPrefs.GetString("diff") == "Easy")
                {
                    player.health = player.health - r.Next(player.easyDamageMin, player.easyDamageMax);
                }
                else if (PlayerPrefs.GetString("diff") == "Medium")
                {
                    player.health = player.health - r.Next(player.mediumDamageMin, player.mediumDamageMax);
                }
                else if (PlayerPrefs.GetString("diff") == "Hard")
                {
                    player.health = player.health - r.Next(player.mediumDamageMin, player.mediumDamageMax);
                }
                Instantiate(bloodSplash, player.transform.position, Quaternion.identity);
                manager.lastHit = manager.score;
                shake.start = true;
            }
        }
    }
}