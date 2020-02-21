using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System;

public class CRUD : MonoBehaviour
{
	private const string dbname = "english_simulator.db";
	public static string connectionString;
	public static SqliteConnection connection;
	public static SqliteCommand command;
	public static SqliteDataReader reader;

	/// <summary>
	/// Соединение с базой данных
	/// </summary>
	public void OpenDataBase()
	{
		string filepath = "";
		if (Application.platform == RuntimePlatform.Android)
			filepath = Application.persistentDataPath + "/" + dbname;
		else
			filepath = Application.streamingAssetsPath + "/" + dbname;
		if (!File.Exists(filepath))
		{
			WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + dbname);
			while (!loadDB.isDone) { }
			File.WriteAllBytes(filepath, loadDB.bytes);
		}

		connectionString = "URI=file:" + filepath;
		connection = new SqliteConnection(connectionString);
		connection.Open();
	}

	/// <summary>
	/// Закрыть подключение в базе данных
	/// </summary>
	public void CloseDataBase()
	{
		reader.Close();
		reader = null;
		command.Dispose();
		command = null;
		connection.Close();
		connection = null;
	}

    /// <summary>
    /// Оператор SELECT
    /// </summary>
    public SqliteDataReader Select(string query)
    {
        command = connection.CreateCommand();
        command.CommandText = query;
        reader = command.ExecuteReader();
        return reader;
    }

    public void InsertInto(string query)
    {
        try
        {
            command = connection.CreateCommand();
            command.CommandText = query;
            reader = command.ExecuteReader();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void DeleteQuery(string query)
    {
        try
        {
            command = connection.CreateCommand();
            command.CommandText = "pragma foreign_keys = on; " + query;
            reader = command.ExecuteReader();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void UpdateQuery(string query)
    {
        try
        {
            command = connection.CreateCommand(); // create empty command
            command.CommandText = query; // fill the command
            reader = command.ExecuteReader(); // execute command which returns a reader
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
