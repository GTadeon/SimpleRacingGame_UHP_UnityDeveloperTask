using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// attached to database manager object. Calls method for creating database (if it isn't created already)
/// </summary>
public class CreateDB : MonoBehaviour {


    public class ConnectionInspector
    {
        public string DatabaseName { get; set; }
        public bool IsConnected { get; set; }
    }



    private static DataService dataService;

    [HeaderAttribute("e.g. Items")]
    [Tooltip("Name of database that will be saved in StreamingAssets folder")]
    public string DatabaseName; //extension .db doesen't have to be included in it. e.g. just "Employees" is sufficent
    public static string databaseName = "Items.db"; //this can be changed in inspector ofc,by changing DatabaseName value 

    [Tooltip("Path in which you want your database in. Database will be stored in StreamingAssets folder that will be created in that path. You must select folder located somewhere under Assets folder. For example, you cannot select folder located on your desktop (I mean, you can , but only if that's also the location of this unity project that you're working in right now ) xd .")]
    public string DatabasePath;
    public static string databasePath;

    public static bool isConnected;


    public static ConnectionInspector LastConnectionWithDatabaseInInspector;




    void Awake()
    {
        databaseName = EditConnectionString(DatabaseName);
        databasePath = DatabasePath;
    }

    void Start()
    {
        ConnectWithDatabase(databaseName, databasePath);
    }

    /// <summary>
    /// connects with database (if it's not already connected) in Streaming assets folder or creates it, if there is no such yet.
    /// </summary>
    public static void ConnectWithDatabase(string databaseName, string databasePath)
    {
        if (!isConnected)
        {
            dataService = new DataService(databaseName, databasePath);
            dataService.CreateDB();
            isConnected = true;
        }
    }



    /// <summary>
    /// connects with database (if it's not already connected) in Streaming assets folder or creates it, if there is no such yet.
    /// Returns data service object. Call this when you want to communicate with database when in inspector.
    /// </summary>
    public static DataService ConnectWithDatabaseInspector(string DatabaseName, string DatabasePath)
    {
        if (LastConnectionWithDatabaseInInspector == null || (LastConnectionWithDatabaseInInspector != null && LastConnectionWithDatabaseInInspector.DatabaseName != DatabaseName))
        {
            DatabaseName = EditConnectionString(DatabaseName);
            dataService = new DataService(DatabaseName, DatabasePath);
            dataService.CreateDB();
            LastConnectionWithDatabaseInInspector = new ConnectionInspector() { DatabaseName = DatabaseName, IsConnected = true };
        }
        else
        {
            Debug.Log("Database with given name already exists in <i>StreamingAssets</i> folder.");
        }
        return dataService;
    }


    public static DataService ConnectWithDatabaseInspectorOnEnable(string DatabaseName, string DatabasePath)
    {
        if (LastConnectionWithDatabaseInInspector == null || (LastConnectionWithDatabaseInInspector != null && LastConnectionWithDatabaseInInspector.DatabaseName != DatabaseName))
        {
            DatabaseName = EditConnectionString(DatabaseName);
            dataService = new DataService(DatabaseName, DatabasePath);
            dataService.CreateDB();
            LastConnectionWithDatabaseInInspector = new ConnectionInspector() { DatabaseName = DatabaseName, IsConnected = true };
        }
        return dataService;
    }



    /// <summary>
    /// edits database name string, if needed.
    /// </summary>
    /// <param name="databaseName">name of database entered in inspector </param>
    /// <returns>database name with extension ".db" </returns>
    private static string EditConnectionString(string databaseName)
    {
        string dbName = "";
        if (!databaseName.All(char.IsLetterOrDigit))
            throw new System.ArgumentException("database name can contain alpha numeric characters only. Please edit database name in database manager game object, under attached createDBScript script. ");
        else
            dbName = databaseName + ".db";
        return dbName;
    }

}
