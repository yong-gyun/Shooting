using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData
{
    public string name;
    public int score;
}

public class DataManager
{
    public List<ScoreData> _scoreDatas = new List<ScoreData>();
    private const string SCORE_SAVE_KEY = "{0}Rank_Name";
    private const string NAME_SAVE_KEY = "{0}Rank_Score";

    public void Init()
    {
        LoadData();
    }

    public void SaveDatas()
    {
        if (_scoreDatas.Count == 0)
            return;

        var temp = _scoreDatas.OrderByDescending(x => x.score).ToList();
        for (int i = 0; i < 5; i++)
        {
            var info = _scoreDatas[i];
            SaveData(info, i + 1);
        }
    }

    public void SaveData(ScoreData info, int idx)
    {
        PlayerPrefs.SetInt(string.Format(SCORE_SAVE_KEY, idx), info.score);
        PlayerPrefs.SetString(string.Format(NAME_SAVE_KEY, idx), info.name);
    }

    public void LoadData()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (PlayerPrefs.HasKey(string.Format(NAME_SAVE_KEY, i)) == false)
                continue;

            string name = PlayerPrefs.GetString(string.Format(NAME_SAVE_KEY, i), "");
            int score = PlayerPrefs.GetInt(string.Format(SCORE_SAVE_KEY, i), 0);

            ScoreData info = new ScoreData();
            info.score = score;
            info.name = name;

            _scoreDatas.Add(info);
        }
    }

    public void Clear()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (PlayerPrefs.HasKey(string.Format(NAME_SAVE_KEY, i)) == false)
                continue;

            PlayerPrefs.DeleteKey(string.Format(NAME_SAVE_KEY, i));
            PlayerPrefs.DeleteKey(string.Format(SCORE_SAVE_KEY, i));
        }
        
        //PlayerPrefs.DeleteAll();
        _scoreDatas.Clear();
    }
}
