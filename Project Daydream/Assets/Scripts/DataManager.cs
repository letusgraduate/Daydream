using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int moonRock;
    public int speedLevel;
    public int maxHPLevel;
    public int powerLevel;
    public int skillLevel;
    public int dashLevel;
    public int itemLevel;
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
            UIManager.instance.SpeedLevel = 2;
            UIManager.instance.MaxHPLevel = 2;
            UIManager.instance.PowerLevel = 2;
            UIManager.instance.SkillLevel = 2;
            UIManager.instance.DashLevel = 2;
            UIManager.instance.ItemLevel = 2;
            Save();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null)
            {
                GameManager.instance.MoonRock = saveData.moonRock;
                UIManager.instance.ForSpeedUpgrade(saveData.speedLevel);
                UIManager.instance.ForMaxHPUpgrade(saveData.maxHPLevel);
                UIManager.instance.ForPowerUpgrade(saveData.powerLevel);
                UIManager.instance.ForSkillUpgrade(saveData.skillLevel);
                UIManager.instance.ForDashUpgrade(saveData.dashLevel);
                UIManager.instance.ForItemUpgrade(saveData.itemLevel);
            }
        }
    }

    public void Save()
    {
        SaveData saveData = new SaveData();

        saveData.moonRock = GameManager.instance.MoonRock;
        saveData.speedLevel = UIManager.instance.SpeedLevel;
        saveData.maxHPLevel = UIManager.instance.MaxHPLevel;
        saveData.powerLevel = UIManager.instance.PowerLevel;
        saveData.skillLevel = UIManager.instance.SkillLevel;
        saveData.dashLevel = UIManager.instance.DashLevel;
        saveData.itemLevel = UIManager.instance.ItemLevel;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }
}
