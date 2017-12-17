using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour {

    private class ScoreEntry
    {
        public string name;
        public int score;

        public ScoreEntry(string n, int s)
        {
            name = n;
            score = s;
        }
    }

    
    private int scoreCount = 3;

    public Text highscoreText;
    public InputField inputField;

    private List<ScoreEntry> scores = new List<ScoreEntry>();

    public int currentScore = 0;

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        //highscoreText.enabled = true;
        //SaveToPlayerPrefsTest(); // TODO
        LoadFromPlayerPrefs();
        currentScore = 170; // TODO
        RefreshScoreboard();
    }

    private void LoadFromPlayerPrefs()
    {
        string first = PlayerPrefs.GetString("FirstScoreName");
        if (!string.IsNullOrEmpty(first))
        {
            scores.Add(new ScoreEntry(first, PlayerPrefs.GetInt("FirstScorePoints")));
        }
        string second = PlayerPrefs.GetString("SecondScoreName");
        if (!string.IsNullOrEmpty(second))
        {
            scores.Add(new ScoreEntry(second, PlayerPrefs.GetInt("SecondScorePoints")));
        }
        string third = PlayerPrefs.GetString("ThirdScoreName");
        if (!string.IsNullOrEmpty(third))
        {
            scores.Add(new ScoreEntry(third, PlayerPrefs.GetInt("ThirdScorePoints")));
        }
    }

    private void SaveToPlayerPrefsTest()
    {
        PlayerPrefs.SetInt("FirstScorePoints", 200);
        PlayerPrefs.SetString("FirstScoreName", "Moo");
        PlayerPrefs.SetInt("SecondScorePoints", 150);
        PlayerPrefs.SetString("SecondScoreName", "Nik");
        PlayerPrefs.SetInt("ThirdScorePoints", 100);
        PlayerPrefs.SetString("ThirdScoreName", "Gab");
    }

    private void SaveToPlayerPrefs()
    {
        switch(scores.Count)
        {
            case 1:
                {
                    PlayerPrefs.SetInt("FirstScorePoints", scores[0].score);
                    PlayerPrefs.SetString("FirstScoreName", scores[0].name);
                    break;
                }
            case 2:
                {
                    PlayerPrefs.SetInt("FirstScorePoints", scores[0].score);
                    PlayerPrefs.SetString("FirstScoreName", scores[0].name);
                    PlayerPrefs.SetInt("SecondScorePoints", scores[1].score);
                    PlayerPrefs.SetString("SecondScoreName", scores[1].name);
                    break;
                }
            case 3:
                {
                    PlayerPrefs.SetInt("FirstScorePoints", scores[0].score);
                    PlayerPrefs.SetString("FirstScoreName", scores[0].name);
                    PlayerPrefs.SetInt("SecondScorePoints", scores[1].score);
                    PlayerPrefs.SetString("SecondScoreName", scores[1].name);
                    PlayerPrefs.SetInt("ThirdScorePoints", scores[2].score);
                    PlayerPrefs.SetString("ThirdScoreName", scores[2].name);
                    break;
                }
        }
            
    }


    //// Use this for initialization
    //void Start () {
    //    CheckToSaveScore(currentScore);
    //}

    public void CheckToSaveScore(int score)
    {
        if (score > 0)
        {
            currentScore = score;
            Debug.Log("Score bigger 0");
            //for (int i = 0; i < scores.Count; i++)
            //{
            //    Debug.Log(i + " " + scores[i].name + " " + scores[i].score);
            //}

                if (scores.Count < 3 || score > scores[2].score)
            {
                inputField.gameObject.SetActive(true);
                inputField.enabled = true;
                inputField.ActivateInputField();
                Debug.Log("Score valid");
            } else
            {
                RefreshScoreboard();
            }
        }
    }

 //   // Update is called once per frame
 //   void Update () {
		
	//}

    public void HandleNameInput()
    {
        //Debug.Log("Input has ended "+ inputField.text);
        string name = inputField.text;
        
        //inputField.CancelInvoke();
        //inputField.DeactivateInputField();
        //inputField.enabled = false;
        
        if (!string.IsNullOrEmpty(name))
        {
            //Debug.Log("Is null:" + string.IsNullOrEmpty(name));
            SaveToScoreboard(currentScore, name);
            RefreshScoreboard();
        }
    }


    private void SaveToScoreboard(int score, string name)
    {
        if (scores.Count == 0)
        {
            scores.Insert(0, new ScoreEntry(name, score));
        }
        else
        {
            bool added = false;
            for (int i = 0; i < scores.Count; i++)
            {
                if (score > scores[i].score)
                {
                    scores.Insert(i, new ScoreEntry(name, score));
                    i = scores.Count;
                    added = true;
                    Debug.Log("Entry is saved");
                }
            }
            if (!added && scores.Count < scoreCount)
            {
                scores.Insert(scores.Count, new ScoreEntry(name, score));
            }
            // delete last entries until there are just 3 (scoreCount) left
            while (scores.Count > scoreCount)
            {
                scores.RemoveAt(scoreCount);
            }
        }

        
        for (int i = 0; i < scores.Count; i++)
        {
            Debug.Log(scores[i].name + ": " + scores[i].score);
        }
        SaveToPlayerPrefs();
    }

    public void RefreshScoreboard()
    {
        string text = "";
        for (int i = 0; i < scores.Count; i++)
        {
            text = text + (i + 1) + ".  " + scores[i].name + "\t \t" + scores[i].score + "\n";
        }
        highscoreText.text = text;
    }

}
