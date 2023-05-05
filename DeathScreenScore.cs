using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenScore : MonoBehaviour
{

    private int score;
    public Text text;
    public Text diff;
    public GameObject player;
    private BoxCollider2D box;
    private Rigidbody2D rb;
    public Camera camera;

    // Start is called before the first frame update

    private void Awake()
    {
        score = GameObject.Find("Player").GetComponent<GameManager>().score;
        Destroy(GameObject.Find("Player"));
        text.text = "Score: " + score.ToString();

        if(PlayerPrefs.GetString("diff") == "Easy")
        {
            diff.text = "Easy Difficulty";
            diff.color = Color.green;
        }
        else if (PlayerPrefs.GetString("diff") == "Medium")
        {
            diff.text = "Medium Difficulty";
            diff.color = Color.yellow;
        }
        else if (PlayerPrefs.GetString("diff") == "Hard")
        {
            diff.text = "Hard Difficulty";
            diff.color = Color.red;
        }
    }

}
