using Newtonsoft.Json;
using SimpleSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;


public class SimpleSaveManager : MonoBehaviour
{
    public SimpleSaveSettings saveSettings;

    public static void SaveToFile(SimpleSaveSettings settings, string key, object saveObject)
    {
        SSave.ReadFile(settings, out var data);

        data[key] = saveObject;

        SSave.WriteFile(data, settings);
    }

    public static bool ReadFromFile(SimpleSaveSettings settings, string key, out object saveObject)
    {
        SSave.ReadFile(settings, out var data);

        if (data.TryGetValue(key, out saveObject))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void DeleteFile(SimpleSaveSettings settings)
    {
        SimpleSaveFileHandler.Delete(settings);
    }

    // Statics
    public static void SaveAllGameObjectsInScene(SimpleSaveSettings settings)
    {
        SSave.ReadFile(settings, out var data);

        foreach (var saveable in FindObjectsOfType<SaveGameObject>())
        {
            data[saveable.id] = saveable.CaptureAllComponents();
        }

        SSave.WriteFile(data, settings);
    }

    public static void LoadAllGameObjectsInScene(SimpleSaveSettings settings)
    {
        if (SSave.ReadFile(settings, out Dictionary<string, object> data))
        {
            foreach (var saveable in FindObjectsOfType<SaveGameObject>())
            {
                saveable.RestoreAllComponents(data[saveable.id]);
            }
        }
    }

    public static object LoadAndGetData(SimpleSaveSettings settings, string id)
    {
        if (SSave.ReadFile(settings, out Dictionary<string, object> data))
        {
            if (data.TryGetValue(id, out var val))
            {
                return val;
            }
        }
        return null;
    }



    // Tools
    public void OpenFileLocation()
    {
        string path = saveSettings.GetCurrentPath();
        Application.OpenURL("file://" + path);
    }



}
#if UNITY_EDITOR

[CustomEditor(typeof(SimpleSaveManager))]
public class SimpleSaveManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SimpleSaveManager myTarget = (SimpleSaveManager)target;

        DrawDefaultInspector();

        //if (GUILayout.Button("Save All")) myTarget.SaveAlltheDataInScene();
        //if (GUILayout.Button("Load All")) myTarget.LoadAllDataInScene();
        if (GUILayout.Button("Open File Location")) myTarget.OpenFileLocation();
        
    }
}
#endif