using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Repository : MonoBehaviour {

    //saves to database or updates of if it's already saved
    public static void SaveCarStartersConfigs(UserCarSettings setting)
    {
        var fromDatabase = DataService.GetConfigById(setting.StarterCarId);
        if (fromDatabase==null)
        {
            DataService.AddItemToStoragTable(typeof(UserCarSettings), setting);
        }
        else
        {
            setting.Id = fromDatabase.Id;
            DataService.UpdateAlreadyStoredItem(typeof(UserCarSettings), setting);
        }
    }


    //if any is made(saved) so far
    public static List<UserCarSettings> GetUserCarSettings()
    {
        return DataService.GetAllUserSettings();
    }

}
