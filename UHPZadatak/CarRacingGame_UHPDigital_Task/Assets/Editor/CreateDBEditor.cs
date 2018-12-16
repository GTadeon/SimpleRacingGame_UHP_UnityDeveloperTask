using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CreateDB))]
[CanEditMultipleObjects]
public class CreateDbEditor : Editor
{
    private Dictionary<string, SerializedProperty> _properties = new Dictionary<string, SerializedProperty>();
    private List<Property> _timingProperties = new List<Property>();

    private class Property
    {
        public string name;
        public string text;

        public Property(string n, string t)
        {
            name = n;
            text = t;
        }
    }

    private CreateDB _createDB;

    #region Properties
    
    private readonly Property DATABASENAME = new Property("DatabaseName", "Database name");
    private readonly Property DATABASEPATH = new Property("DatabasePath", "Database path");


    #endregion


    private void OnEnable()
    {
        _createDB = (CreateDB)target; //need this for button
        _properties.Clear();
        SerializedProperty property = serializedObject.GetIterator();

        while (property.NextVisible(true))
        {
            _properties[property.name] = property.Copy();
        }

        if (ValidateDatabaseFormInput())
        {
            //connecting with the database:
            string databaseName = _properties[DATABASENAME.name].stringValue;
            string databasePath = _properties[DATABASEPATH.name].stringValue;
            CreateDB.ConnectWithDatabaseInspectorOnEnable(databaseName, databasePath);
        }
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _timingProperties.Clear();

        GUIStyle boldStyle = new GUIStyle();
        boldStyle.fontStyle = FontStyle.Bold;


        //displaying properties and button:
        DisplayRegularField(DATABASENAME);
        EditorGUILayout.Separator();

        if (_properties[DATABASEPATH.name].stringValue.Length==0)
        {
            _properties[DATABASEPATH.name].stringValue = "start typing antyhing in order to trigger popup window for selecting database path.";
        }

        EditorGUI.BeginChangeCheck();
        DisplayRegularField(DATABASEPATH);
        if (EditorGUI.EndChangeCheck())
        {
            string databasePath = PopUpSelectDatabasePathWindow();

            if (databasePath!=null && databasePath.Length>0)
            {
                _properties[DATABASEPATH.name].stringValue = databasePath;
                //making sure that streaming assets folder exists in path
                EnsureStreamingAssetsFolderExists(databasePath);
            }
        }
        EditorGUILayout.Separator();


        if (GUILayout.Button("Create Database"))
        {
            if (ValidateDatabaseFormInput())
            {

                string databaseName = _properties[DATABASENAME.name].stringValue;
                string databasePath = _properties[DATABASEPATH.name].stringValue;

                if (!DoesDatabaseExist(databaseName))
                {
                    CreateDB.ConnectWithDatabaseInspector(databaseName, databasePath);
                }
                else
                {
                    Debug.Log("database with name <i>" + databaseName + "</i> already exists in StreamingAssets folder.");
                }
            }
        }

        serializedObject.ApplyModifiedProperties();

    }

    /// <summary>
    /// both database name and path must be entered.
    /// Else, false is returned.
    /// </summary>
    private bool ValidateDatabaseFormInput()
    {
        if (_properties[DATABASEPATH.name].stringValue.Length == 0 || _properties[DATABASEPATH.name].stringValue == "start typing antyhing in order to trigger popup window for selecting database path.")
        {
            Debug.Log("<color=red>Cannot create database because you haven't entered database path. Please select path for your databse on database manager game object.</color>");
            return false;
        }
        else if (_properties[DATABASENAME.name].stringValue.Length == 0)
        {
            Debug.Log("<color=red>Cannot create database because you haven't entered database name. Please type in the name for your database on database manager game object.</color>");
            return false;
        }
        return true;
    }

    private string PopUpSelectDatabasePathWindow()
    {
        string path = EditorUtility.OpenFolderPanel("Folder to store database in", "", "");
        return path;
    }

    /// <summary>
    /// checks whether or not StreamingAssets folder exists in passed path
    /// If not, creates it!
    /// </summary>
    private void EnsureStreamingAssetsFolderExists(string databasePath)
    {
        //if (!AssetDatabase.IsValidFolder("Assets/StreamingAssets"))
        //{
        //    Directory.CreateDirectory("Assets/StreamingAssets");
        //    Debug.Log("<b>StreamingAssets folder created under Assets folder. StreamingAssets folder is used for storing databases (.db files) </b>");
        //}

        string streamingAssetsFinalPath = databasePath + "\\StreamingAssets";

        if (!Directory.Exists(streamingAssetsFinalPath))
        {
            Directory.CreateDirectory(streamingAssetsFinalPath);
            Debug.Log("<b>StreamingAssets folder created inside path: "+ databasePath + ". StreamingAssets folder is used for storing databases (.db files) </b>");
        }
    }

    /// <summary>
    /// checks for database with passed name in streaming assets folder
    /// located in specified path.
    /// If only one exists, returns true, else false.
    /// </summary>
    private bool DoesDatabaseExist(string databaseName)
    {
        DirectoryInfo dir = new DirectoryInfo( _properties[DATABASEPATH.name].stringValue + "\\StreamingAssets");
        FileInfo[] info = dir.GetFiles("*.db");
        for (int i = 0; i < info.Length; i++)
        {
            if ( info[i].Name == (databaseName + ".db") )
            {
                return true;
            }
        }
        return false;
    }



    private void DisplayRegularField(Property property)
    {
        EditorGUILayout.PropertyField(_properties[property.name], new GUIContent(property.text), true);
    }





}
