  

using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    public DataEvent LevelChange;
    private int level;
    private int recordLevel;

    private void Awake()
    {
        if (LevelChange == null)
            LevelChange = new DataEvent();

       // level = PlayerPrefs.GetInt("level");
        recordLevel = PlayerPrefs.GetInt("recordLevel");

        if (PlayerPrefs.HasKey("tempLevel"))
        {
            level = PlayerPrefs.GetInt("tempLevel");
            PlayerPrefs.DeleteKey("tempLevel");
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetRecordLevel()
    {
        return recordLevel;

    }
    public void SetLevel()
    {
        level++;
        LevelChange.Invoke(level);
       // PlayerPrefs.SetInt("level", level);

        if(level> recordLevel)
        {
            SetRecordLevel();
        }
    }

    private void SetRecordLevel()
    {
        recordLevel = level;
        PlayerPrefs.SetInt("recordLevel", recordLevel);
    }
}
