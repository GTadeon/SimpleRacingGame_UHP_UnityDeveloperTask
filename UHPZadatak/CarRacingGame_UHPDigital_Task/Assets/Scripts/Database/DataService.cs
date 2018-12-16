using SQLite4Unity3d;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;


/// <summary>
/// contains all methods that do heav duty backend stuff on our sqlite database :). Mostly CRUD operations (create, read, update and deleting stuff)
/// </summary>
public class DataService  {

	private SQLiteConnection _connection;

    private static SQLiteConnection _connectionWrapper;

    public DataService(string DatabaseName, string DatabasePath)
    {

#if UNITY_EDITOR
            //var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName); //@"Assets/StreamingAssets/{0}", DatabaseName
            var dbPath = FormatDatabasePath(DatabasePath, DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            _connectionWrapper = _connection; //why another one? because I needed some kind of static wraper so I can use it in static methods :)
            Debug.Log("<color=#00ff00> Connection with database is succefully established! Database path: " + dbPath + " </color> ");     

	 }


    /// <summary>
    /// databasePath -> c://...//...//Assets/something/
    /// database name -> testDatabase 
    /// </summary>
    private string FormatDatabasePath(string databasePath, string databaseName)
    {
        string [] rightOfAssets = databasePath.Split(new[] { "Assets" }, StringSplitOptions.None);

        string dbPathBeginingWithAssets = "Assets" + rightOfAssets[1] + "\\StreamingAssets" + "\\{0}";

        string finalFormat = string.Format(@dbPathBeginingWithAssets, databaseName);

        return finalFormat ;
    }

    /// <summary>
    /// method for creating tables in our database. Called only when database is not alredy initialized. If you added new classes because you want some additional
    /// tables, then just add that class name here too (As a parameter of CreateTable method that will create table with columns that corespond to passed class
    /// properties)
    /// </summary>
    public void CreateDB()
    {
		_connection.CreateTable<UserCarSettings> ();
    }



    //public IEnumerable<ItemToolbarStored> GetAllStoredItemsInItemToolbar()
    //{
    //    return _connection.Table<ItemToolbarStored>();
    //}


    public static bool DoesStarterConfigExists(string starterId)
    {
        return _connectionWrapper.Table<UserCarSettings>().Any(x=>x.StarterCarId==starterId);
    }

    public static List<UserCarSettings> GetAllUserSettings()
    {
        return _connectionWrapper.Table<UserCarSettings>().ToList();
    }

    public static UserCarSettings GetConfigById(string starterId)
    {
        return _connectionWrapper.Table<UserCarSettings>().Where(x=>x.StarterCarId==starterId).FirstOrDefault();
    }




    public static void AddItemToStoragTable(Type table, object item)
    {
        //Debug.Log("I got item of type "+ item.GetType() + " and throving it in  table:  " + table);
        var storageTableInstance = Activator.CreateInstance(table);
        var TableProperties = storageTableInstance.GetType().GetProperties().OrderBy(x => x.Name).Select(x => x.Name);

        //assigning values to columns (properties) of storageTableInstance:
        foreach (var tableColumnName in TableProperties)
        {
            storageTableInstance.GetType().GetProperty(tableColumnName).SetValue(storageTableInstance, item.GetType().GetProperty(tableColumnName).GetValue(item, null), null);
        }
        _connectionWrapper.Insert(storageTableInstance);
    }


    /// <summary>
    /// removes (deletes) passed item to a table  in databse that is used as storage. Both passed variables must be of same data type (class).
    /// </summary>
    /// <param name="table">table class that corresponds to a table name that you want to store your item in</param>
    /// <param name="item">item you want to store in that table that is the same data type as passed table</param>
    public static void RemoveItemFromStorageTable(Type table, object item)
    {
        var storageTableInstance = Activator.CreateInstance(table);
        var TableProperties = storageTableInstance.GetType().GetProperties().OrderBy(x => x.Name).Select(x => x.Name);

        //assigning values to columns (properties) of storageTableInstance:
        foreach (var tableColumnName in TableProperties)
        {
            storageTableInstance.GetType().GetProperty(tableColumnName).SetValue(storageTableInstance, item.GetType().GetProperty(tableColumnName).GetValue(item, null), null);
        }
        _connectionWrapper.Delete(storageTableInstance);
    }


    public static void UpdateAlreadyStoredItem(Type table, object item)
    {
        var storageTableInstance = Activator.CreateInstance(table);
        var TableProperties = storageTableInstance.GetType().GetProperties().OrderBy(x => x.Name).Select(x => x.Name);

        //assigning values to columns (properties) of storageTableInstance:
        foreach (var tableColumnName in TableProperties)
        {
            storageTableInstance.GetType().GetProperty(tableColumnName).SetValue(storageTableInstance, item.GetType().GetProperty(tableColumnName).GetValue(item, null), null);
        }
        _connectionWrapper.Update(storageTableInstance);
    }

}
