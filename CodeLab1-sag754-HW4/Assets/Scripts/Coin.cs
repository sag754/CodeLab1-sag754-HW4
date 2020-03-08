using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) //If another GameObject with a 2D Collider on it hits this GameObject's collider
    {
        PlayerController.instance.Score++; //increase the player's score using the Singleton!
        Destroy(gameObject);

        if(GameManager.instance.Score >= 5)
        {
            SceneManager.LoadScene(3);
        }
    }
}