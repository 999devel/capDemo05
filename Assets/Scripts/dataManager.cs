using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



[System.Serializable]
public class PlayerData
{
    public Vector3 location;
    public int quest;
    public PlayerData()
    {
        location = new Vector3(0, 0, 0);
        quest = (int)questList.quest_List.none;
    }
}
public class dataManager : MonoBehaviour
{
    private string path;
    private PlayerData nowPlayer = new PlayerData();

    public static dataManager instance;
    public GameObject player;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        path = Application.persistentDataPath + "/save";
    }

    public void save_Data()
    {
        nowPlayer.location = player.transform.position;
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path, data);
    }

    public void load_Data()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        if (File.Exists(dataManager.instance.path))
        {
            string data = File.ReadAllText(path);
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
            player.transform.position = nowPlayer.location;
        }
        else
        {
            Debug.Log("저장된 데이터 없음!");
        }
    }


    //매개변수 enum quest_List에서 넘기기
    public void update_Quest(int quest)
    {
        nowPlayer.quest = quest;
    }

    public int get_Quest()
    {
        return nowPlayer.quest;
    }
}
