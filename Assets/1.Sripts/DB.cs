using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using System;
using UnityEngine.UI;
using TMPro;

public class DB : MonoBehaviour
{
    public static DB instance;
    private void Awake() {
        instance = this;
        SetDBPath();
        LoadStage();


    }
    string filepath;
    IDbConnection dbconn;
    IDbCommand dbcommand;
    IDataReader reader;

    string temp_path;
    string sqlQuery;
    public List<Stage> stageList = new List<Stage>();
    public struct Stage{
        public int num;
        public int score;
    }
    Stage stage = new Stage();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadStage(){
        try
        {
            dbconn = new SqliteConnection(temp_path);
            dbconn.Open();
            dbcommand = dbconn.CreateCommand();
            sqlQuery = string.Empty;
            sqlQuery = "SELECT StageNum, Score FROM Stage";//장비의 가격들을 가져온다
            dbcommand.CommandText = sqlQuery;
            reader = dbcommand.ExecuteReader();
            while (reader.Read())//완료한 스테이지 번호를 가져온다
            {
                stage.num = reader.GetInt32(0);
                stage.score = reader.GetInt32(1);
                stageList.Add(stage);
            }
        }
        catch (Exception e)
        {
            print(e);
        }
        finally
        {
            reader.Close();
            dbconn.Close();
        }
    }
    public void  SetDBPath()
    {
        filepath = string.Empty;
        if(Application.platform == RuntimePlatform.Android)
        {
            filepath = Application.persistentDataPath + "/DB.db";
            if (!File.Exists(filepath))
            {
                //UnityWebRequest unityWebRequest = UnityWebRequest.Get("jar:file://" + Application.dataPath + "!/assets/DB.db");
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/DB.db");
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone)
                {
                    //conn.text = "무한 while중...";
                }
                File.WriteAllBytes(filepath, loadDB.bytes);
            }
        }
        else
        {
            filepath = Application.dataPath + "/StreamingAssets/DB.db";
            if (!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + "/DB.db", filepath);
            }
        }
        try
        {
            temp_path = "URI=file:" + filepath;
            dbconn = new SqliteConnection(temp_path); 
        }
        catch(Exception e)
        {
            print(e);
        }
    }
}
