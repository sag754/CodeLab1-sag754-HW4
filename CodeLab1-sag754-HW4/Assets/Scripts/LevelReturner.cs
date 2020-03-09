using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReturner : MonoBehaviour
{
    public static int currentLevel = 1;

    public static LevelReturner instance;

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
        currentLevel--;
        SceneManager.LoadScene(currentLevel);
    }
}