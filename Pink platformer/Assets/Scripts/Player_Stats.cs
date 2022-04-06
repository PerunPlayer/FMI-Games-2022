using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Stats : MonoBehaviour
{
    bool hasKey = false;
    int lifes;
    GameObject HUDKey, HUDHeart1, HUDHeart2, HUDHeart3;
    public Sprite fullKeySprite, emptyHeart;
    Scene hud;

    void Start()
    {
        hud = SceneManager.GetSceneByName("HUD");
        lifes = 3;
    }

    private void LateUpdate()
    {
        if (HUDKey == null)
        {
            HUDKey = GameObject.Find("HUDKey");
            HUDHeart1 = GameObject.Find("Heart1");
            HUDHeart2 = GameObject.Find("Heart2");
            HUDHeart3 = GameObject.Find("Heart3");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Spikes"))
        {
            lifes--;
            Debug.Log("Lifes: " + lifes);
            if (lifes < 1)
            {
                HUDHeart1.GetComponent<Image>().sprite = emptyHeart;
            }
            else if (lifes < 2)
            {
                HUDHeart2.GetComponent<Image>().sprite = emptyHeart;
                Camera.main.GetComponent<VignetteShader>().enabled = true;
            }
            else if (lifes < 3)
            {
                HUDHeart3.GetComponent<Image>().sprite = emptyHeart;
            }

            if (lifes > 0)
            {
                transform.position = new Vector2(0, -1.5f); //respawn
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }
        if (collision.gameObject.tag.Equals("Key"))
        {
            Destroy(collision.gameObject);
            hasKey = true;
            if (HUDKey == null)
            {
                Debug.Log("null key");
            }
            HUDKey.GetComponent<Image>().sprite = fullKeySprite;
        }
    }
}
