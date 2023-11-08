using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int moonRock;
}

public class DataManager : MonoBehaviour
{
    /* ------------ 저장 위치 변수 ------------- */
    private string path;

    /* -------------- 이벤트 함수 -------------- */
    void Start()
    {
        path = Path.Combine(Application.dataPath, "SaveData.json");
        Load();
    }

    /* --------------- 기능 함수 --------------- */
    public void Load()
    {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            GameManager.instance.MoonRock = 0;
            Save();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                GameManager.instance.MoonRock = saveData.moonRock;
            }
        }
    }

    public void Save()
    {
        SaveData saveData = new SaveData();

        saveData.moonRock = GameManager.instance.MoonRock;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }
}
