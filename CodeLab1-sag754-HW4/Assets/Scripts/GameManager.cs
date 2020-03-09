using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Text infoText;
    public Text infoTime;
    public Text infoRecord;

    private const string FILE_ALL_RECORDS = "/All_records.txt";

    public float timer = 0; //start timer at 0 and keeps track of time

    public bool playing = true;

    private float score = 0;


    //.........................PROPERTY FOR SCORE
    public float Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
    //.........................END PROPERTY FOR SCORE

    public List<string> highScoreNames;
    public List<float> highScoreNums;

    //..........................SINGLETON

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //..........................END SINGLETON
    //.........................WRITES TO FILE

    // Start is called before the first frame update
    void Start()
    {
        highScoreNames = new List<string>(); //init highScoreNames
        highScoreNums = new List<float>();  //init highScoreNums

        if (File.Exists(Application.dataPath + FILE_ALL_RECORDS)) //if the high score file exists
        {
            string fileContents = File.ReadAllText(Application.dataPath + FILE_ALL_RECORDS); //get the contents of the file

            string[] scorePairs = fileContents.Split('\n'); //split it on the newline, making each space in the array a line in the file

            for (int i = 0; i < 5; i++)
            { //loop through the 10 scores
                string[] nameScores = scorePairs[i].Split(' '); //split each line on the space
                highScoreNames.Add(nameScores[0]); //the first part of the split is the name
                highScoreNums.Add(float.Parse(nameScores[1])); //the second part is the value
            }
        }
        else //if the high score file doesn't exist
        {
            for (int i = 0; i < 5; i++) //create a new default high score list
            {
                highScoreNames.Add("AAA");
                highScoreNums.Add(100 + i * 10);
            }
        }

        Debug.Log(Application.dataPath);

    }

    //.........................ENDS WRITES TO FILE
    //.........................TIMER AND INFO TEXT

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        infoText.text = "Coins x " + PlayerController.instance.Score;
        infoTime.text = "Time: " + (int)timer;
        infoRecord.text = "Best Time: " + highScoreNums[0];
    }

    //.........................END TIMER AND INFO TEXT
    //.........................UPDATES HIGH SCORE/RECORD LIST

    //function that updates the high score list
    public void UpdateHighScores()
    {

        bool newRecord = false; //by default, we don't have new high score

        for (int i = 0; i < highScoreNums.Count; i++)
        { //go through all the high scores
            if (highScoreNums[i] > timer)
            { //if we have a time that is lower than one of the high scores
                highScoreNums.Insert(i, timer); //insert this new score into the value list
                highScoreNames.Insert(i, "TOP"); //give it the name "TOP"
                newRecord = true; //we have a new high score
                break; //leave the for loop
            }
        }

        if (newRecord)
        { //if we have a new high score
            highScoreNums.RemoveAt(highScoreNums.Count - 1); //remove the final high score value so we are back down to 10
            highScoreNames.RemoveAt(5);
        }

        string fileContents = ""; //create a new string to insert into the file

        for (int i = 0; i < highScoreNames.Count; i++)
        { //loop through all the high scores
            fileContents += highScoreNames[i] + " " + highScoreNums[i] + "\n"; //build a string for all the high scores in the lists
        }

        File.WriteAllText(Application.dataPath + FILE_ALL_RECORDS, fileContents); //save the list to the file
    }

    //.........................END UPDATES HIGH SCORE/RECORD LIST
    //.........................RESET GAME VALUES

    //reset the important values when the game restarts
    public void Reset()
    {
        timer = 0;
        PlayerController.instance.Score = 0;
    }
}
