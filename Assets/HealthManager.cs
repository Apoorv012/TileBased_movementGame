using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;
    public int health = 8;
    int totalHearts = 8;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


    private void Awake()
    {
        Instance = this;
        health = 8;
    }

    public void UpdateHearts()
    {
        if (health <= 0)
        {
            GameManager.Instance.Gameover();
        }
            
        for (int i = 0; i < totalHearts; i++)
        {
            if (health >= i + 1)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }

    }
}
