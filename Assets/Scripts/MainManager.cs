using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; set; }

    public Color TeamColor { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadColor();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        var filepath = Application.persistentDataPath + "/savefile.json";
        var saveData = new SaveData() { TeamColor = TeamColor };
        var json = JsonUtility.ToJson(saveData);
        File.WriteAllText(filepath, json);
        Debug.Log($"Save data to: {filepath}");
    }

    public void LoadColor()
    {
        var filepath = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(filepath))
        {
            var json = File.ReadAllText(filepath);
            var saveData = JsonUtility.FromJson<SaveData>(json);
            TeamColor = saveData.TeamColor;
        }
    }
}
