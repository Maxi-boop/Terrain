using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private int health;
    private int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;

    private Rigidbody rb;

    public void removeHealth()
    {
        health--;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = 5;
        numOfHearts = 5;
    }
    private void Update()
    {
        DrawHearts();
        if(health <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        health = numOfHearts;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

    }
    private void DrawHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numOfHearts && i < health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Enemy")){
            Debug.Log("Enemy");
            health--;
            DrawHearts();
            
        }
    }
}
