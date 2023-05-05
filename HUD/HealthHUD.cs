using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    public Player player;
    public RectTransform healthBar;
    private float healthBarWidthMultiplier;
    private float startingMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        healthBarWidthMultiplier = healthBar.rect.width / player.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.sizeDelta = new Vector2(player.health * healthBarWidthMultiplier, healthBar.rect.height);
    }
}
