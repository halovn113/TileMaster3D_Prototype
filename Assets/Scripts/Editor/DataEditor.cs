using Codice.Client.BaseCommands;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

#if UNITY_EDITOR

public class DataEditor : EditorWindow
{
    public LevelDataSO LevelDataSO;
    private int SelectedMapTabIndex = -1;
    private Vector2 scrollPos;

    [MenuItem("Window/Game Settings")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DataEditor));
    }

    void OnGUI()
    {
        // default source:
        LevelDataSO = Resources.Load("ScriptableObject/GameSettings") as LevelDataSO;

        // level data
        LevelDataSO = EditorGUILayout.ObjectField(LevelDataSO, typeof(LevelDataSO), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as LevelDataSO;
        if (LevelDataSO)
        {
            #region Map
            EditorGUILayout.BeginHorizontal();
            if (LevelDataSO.Maps.Count > 0)
            {
                var selectedMap = GUILayout.Toolbar(SelectedMapTabIndex, GetMapNames().ToArray(), GUILayout.Width(250f));
                if (selectedMap >= 0)
                {
                    SelectedMapTabIndex = selectedMap;
                }
            }

            if (GUILayout.Button("+", GUILayout.Width(100f)))
            {
                LevelDataSO.Maps.Add(new Map
                {
                    Name = $"Map {LevelDataSO.Maps.Count + 1}",
                    DisplayName = $"Level {LevelDataSO.Maps.Count + 1}",
                    Level = LevelDataSO.Maps.Count + 1,
                    ListTileData = new List<TileData>(),
                    Time = 400
                });
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();

            if (LevelDataSO.Maps.Count > 0 && SelectedMapTabIndex >= 0)
            {
                var map = LevelDataSO.Maps[SelectedMapTabIndex];
                map.Name = EditorGUILayout.TextField("Name", map.Name);
                map.DisplayName = EditorGUILayout.TextField("Display Name", map.DisplayName);
                map.Level = EditorGUILayout.IntField("Level", map.Level);
                map.Time = EditorGUILayout.IntField("Play Time (s)", map.Time);

                if (map.ListTileData.Count > 0)
                {
                    EditorGUILayout.Space(50f);
                    EditorGUILayout.BeginVertical();

                    EditorGUILayout.BeginHorizontal();

                    GUIStyle TitleStyles = GUIStyle.none;
                    TitleStyles.normal.textColor = Color.cyan;
                    TitleStyles.fontStyle = FontStyle.Bold;
                    GUI.backgroundColor = Color.clear;

                    EditorGUILayout.LabelField("Idx", TitleStyles, GUILayout.Width(50f));
                    EditorGUILayout.LabelField("Sprite", TitleStyles, GUILayout.Width(100f));
                    EditorGUILayout.LabelField("Chances", TitleStyles, GUILayout.Width(50f));
                    EditorGUILayout.EndHorizontal();

                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

                    for (int i = 0; i < map.ListTileData.Count; i++)
                    {

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(50f));

                        map.ListTileData[i].Sprite = EditorGUILayout.ObjectField(map.ListTileData[i].Sprite, typeof(Sprite),
                            false, GUILayout.Height(100f), GUILayout.Width(100f)) as Sprite;

                        map.ListTileData[i].Chances = EditorGUILayout.IntField(map.ListTileData[i].Chances, GUILayout.Width(50f));
                        if (GUILayout.Button("Remove", GUILayout.Width(100f)))
                        {
                            map.ListTileData.Remove(map.ListTileData[i]);
                        }
                        //map.ListTileData[i].Id = EditorGUILayout.TextField("Name", map.Name);
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUILayout.EndScrollView();
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.Space(50f);
                if (GUILayout.Button("Add new tile", GUILayout.Width(100f)))
                {
                    map.ListTileData.Add(new TileData()
                    {
                        Id = map.ListTileData.Count,
                        Sprite = null,
                        Chances = 1,
                    });
                }

            }

            if (GUILayout.Button("Remove map", GUILayout.Width(200f)))
            {
                LevelDataSO.Maps.Remove(LevelDataSO.Maps[SelectedMapTabIndex]);
                SelectedMapTabIndex = -1;
            }

            EditorGUILayout.EndVertical();
            #endregion
        }
        EditorUtility.SetDirty(LevelDataSO);
    }

    private IEnumerable<string> GetMapNames()
    {
        foreach(var name in LevelDataSO.Maps.Select(map => map.Name))
        {
            yield return name;
        }
    }

}

#endif