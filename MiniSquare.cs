using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MiniSquare : MonoBehaviour
{
    public float speed = 5f;
    public Player player;
    public Camera camera;
    private ScreenShake shake;
    private GameManager manager;
    private Rigidbody2D rb;
    public int easyMinDamage = 10;
    public int easyMaxDamage = 15;
    public int mediumMinDamage = 20;
    public int mediumMaxDamage = 25;
    public int hardMinDamage = 30;
    public int hardMaxDamage = 35;
    public ParticleSystem spawn;
    public ParticleSystem destroy;
    public ParticleSystem bloodSplash;
    // Start is called before the first frame update
    void Start()
    {
        manager = player.gameObject.GetComponent<GameManager>();   
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        shake = camera.gameObject.GetComponent<ScreenShake>();
    }

    // Update is called once per frame
    private void Awake()
    {
        if (PlayerPrefs.GetString("diff") == "Easy")
        {
            speed = 3f;
        }
        else if (PlayerPrefs.GetString("diff") == "Medium")
        {
            speed = 5f;
        }
        else if (PlayerPrefs.GetString("diff") == "Hard")
        {
            speed = 7f;
        }
    }

    private void FixedUpdate()
    {
        if (!manager.falling)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.gameObject.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (PlayerPrefs.GetString("diff") == "Easy") 
            {
                player.health -= UnityEngine.Random.Range(easyMinDamage, easyMaxDamage);
            } 
            else if (PlayerPrefs.GetString("diff") == "Medium")
            {
                player.health -= UnityEngine.Random.Range(mediumMinDamage, mediumMaxDamage);
            } 
            else if (PlayerPrefs.GetString("diff") == "Hard")
            {
                player.health -= UnityEngine.Random.Range(hardMinDamage, hardMaxDamage);
            }
            shake.start = true;
            Instantiate(destroy, transform.position, Quaternion.identity);
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
            transform.position = new Vector3(UnityEngine.Random.Range(10.5f * -1, 10.5f), UnityEngine.Random.Range(4.7f * -1, 4.7f), transform.position.z);
            Instantiate(spawn, transform.position, Quaternion.identity);
        }
    }
}