using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    public static void Load(ref PlayerData data)
    {
        if (!PlayerPrefs.HasKey("record"))
            return;

        data.record = PlayerPrefs.GetInt("record");
    }

    public static void Save(PlayerData data)
    {
        var record = PlayerPrefs.HasKey("record") ? PlayerPrefs.GetInt("record") : 0;
        
        if(data.scores > record)
        {
            PlayerPrefs.SetInt("record", data.scores);
            PlayerPrefs.Save();
        }
    }
}
