using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    // Start is called before the first frame update
    public int score = 0;
    public int lastHit = 0;
    public int bouncers = 0;
    public Player player;
    private GameObject playerRef;
    private Rigidbody2D rb;
    public static float width;
    public static float height;
    float scoreGoal = 5;
    public float scoreGoalMultiplier = 3;
    public Bouncer bouncer;
    public ParticleSystem bouncerSpawn;
    public float spawnPadding = 1f;
    public bool canScore = true;
    public bool falling = false;
    public string scene;
    void Start()
    {
        score = 0;
        lastHit = 1;
        bouncers = 1;
        width = player.camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0)).x;
        height = player.camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0)).y;
    }

    private void Awake()
    {
        instance = this;

        canScore = true;

        falling = false;

        rb = player.gameObject.GetComponent<Rigidbody2D>();
        playerRef = player.gameObject;

        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.GetString("diff") == "Easy")
        {
            scoreGoalMultiplier = 4;
        }
        else if (PlayerPrefs.GetString("diff") == "Medium")
        {
            scoreGoalMultiplier = 3;
        }
        else if (PlayerPrefs.GetString("diff") == "Hard")
        {
            scoreGoalMultiplier = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(score >= scoreGoal)
        {
            var pos = new Vector3(UnityEngine.Random.Range((width - spawnPadding) * -1, width - spawnPadding), UnityEngine.Random.Range((height - spawnPadding) * -1, height - spawnPadding), transform.position.z);
            var newBouncer = Instantiate(bouncer, pos, Quaternion.identity);
            newBouncer.Spawned();
            Instantiate(bouncerSpawn, pos, Quaternion.identity);
            scoreGoal *= scoreGoalMultiplier;
        }
        if (player.health <= 0 && !falling)
        {
            Destroy(player);
            rb.AddTorque(360, ForceMode2D.Impulse);
            var dist = transform.position.x - player.camera.ScreenToWorldPoint(Input.mousePosition).x;
            rb.AddForce(transform.up * 500 + transform.right * (Math.Clamp(dist, -1, 1)) * -250);
            rb.gravityScale = 1f;
            canScore = false;
            falling = true; 
        }
        if (playerRef.transform.position.y <= -height - 30 && falling)
        {
            DontDestroyOnLoad(playerRef);
            scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Death");
        }
    }
}
