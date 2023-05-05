using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
    public Player player;
    private GameManager manager;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        manager = player.gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + manager.score.ToString();
    }
}
