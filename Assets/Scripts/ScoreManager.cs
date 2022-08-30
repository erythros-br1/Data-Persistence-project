using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    class SaveData
    {
        public string Name;
        public int Score;
    }

    public static ScoreManager Instance;

    public string PlayerName;
    public int PlayerScore;

    public string BestScoreName;
    public int BestScore;

    public Text BestScoreText;
    public InputField PlayerField;

    public void StartGame()
    {
        PlayerName = PlayerField.text;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScoreName = data.Name;
            BestScore = data.Score;
        }
    }

    public void SaveNewBestScore()
    {
        if(PlayerScore > BestScore)
        {
            SaveData data = new SaveData();
            data.Name = PlayerName;
            data.Score = PlayerScore;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
        BestScoreText.text = BestScoreText.text = "Best Score: " + BestScoreName + ": " + BestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
