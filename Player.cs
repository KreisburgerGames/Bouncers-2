using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float speed = 25f;
    float dirx;
    float diry;
    public int health = 100;
    public GameManager manager;
    public Camera camera;
    private ScreenShake screenShake;
    public int easyDamageMin = 5;
    public int easyDamageMax = 15;
    public int mediumDamageMin = 10;
    public int mediumDamageMax = 25;
    public int hardDamageMin = 20;
    public int hardDamageMax = 35;
    public int maxHealth = 100;
    private bool mouseControls;
    private float width;
    private float height;
    private float padding = 0.5f;
    private bool pushingx = false;
    private bool pushingy = false;
    public ParticleSystem bloodSplash;
    public ParticleSystem EasySpawn;
    public ParticleSystem MediumSpawn;
    public ParticleSystem HardSpawn;
    public float friction = 1.5f;
    public float force = 20f;
    private bool right;
    private bool up;
    private Vector2 mousePos;
    public static string scene;

    Rigidbody2D rb;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("mouseControls") == 1)
        {
            mouseControls = true;
        }
        else
        {
            mouseControls = false;
        }

        pushingx = false;
        pushingy = false;

        scene = SceneManager.GetActiveScene().name;

        screenShake = camera.GetComponent<ScreenShake>();

        if(SceneManager.GetActiveScene().name == "Game")
        {
            if(PlayerPrefs.GetString("diff") == "Easy")
            {
                Instantiate(EasySpawn, transform.position, Quaternion.identity);
            }
            else if (PlayerPrefs.GetString("diff") == "Medium")
            {
                Instantiate(MediumSpawn, transform.position, Quaternion.identity);
            }
            else if (PlayerPrefs.GetString("diff") == "Hard")
            {
                Instantiate(HardSpawn, transform.position, Quaternion.identity);
            }
        }
    }

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        manager.score = 0;
        width = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0)).x;
        height = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0)).y;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (mouseControls == false && !pushingx && !pushingy)
        {
            dirx = Input.GetAxisRaw("Horizontal");
            diry = Input.GetAxisRaw("Vertical");
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -width + padding, width - padding),
            Mathf.Clamp(transform.position.y, -height + padding, height - padding), transform.position.z);
        if (transform.position.x >= width - padding)
        {
            health -= Random.Range(3, 7);
            rb.velocity = new Vector2(-force, rb.velocity.y);
            pushingx = true;
            right = true;
            screenShake.start = true;
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
        }
        if (transform.position.x <= -width + padding)
        {
            health -= Random.Range(3, 7);
            rb.velocity = new Vector2(force, rb.velocity.y);
            pushingx = true;
            right = false;
            screenShake.start = true;
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
        }
        if (transform.position.y >= height - padding)
        {
            health -= Random.Range(3, 7);
            rb.velocity = new Vector2(rb.velocity.x, -force);
            pushingy = true;
            up = true;
            screenShake.start = true;
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
        }
        if (transform.position.y <= -height + padding)
        {
            health -= Random.Range(3, 7);
            rb.velocity = new Vector2(rb.velocity.x, force);
            pushingy = true;
            up = false;
            screenShake.start = true;
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
        }


    }

    public void FixedUpdate()
    {
        if (!pushingx && !pushingy && mouseControls == false)
        {
            rb.velocity = new Vector2(dirx * speed, diry * speed);
        }
        else if (!pushingx && !pushingy)
        {
            transform.position = Vector2.MoveTowards(transform.position, mousePos, speed * Time.deltaTime);
        }
        else if (pushingx)
        {
            if (right)
            {
                if (rb.velocity.x >= 0)
                {
                    pushingx = false;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x + friction, rb.velocity.y);
                }
            }
            else
            {
                if (rb.velocity.x <= 0)
                {
                    pushingx = false;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x - friction, rb.velocity.y);
                }
            }
        }
        else if (pushingy)
        {
            if (up)
            {
                if (rb.velocity.y >= 0)
                {
                    pushingy = false;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + friction);
                }
            }
            else
            {
                if (rb.velocity.y <= 0)
                {
                    pushingy = false;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - friction);
                }
            }
        }
    }
}
