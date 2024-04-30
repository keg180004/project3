using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

namespace ObjectEditor.Tools
{
    public class IP_ObjectEditor : EditorWindow
    {
        #region Variables
        GameObject[] selected = new GameObject[0];
        string wantedPrefix;
        string wantedName;
        string wantedSuffix;
        string wantedGroup;
        bool addNumbering;
        #endregion

        #region Builtin Methods
        public static void LaunchEditor()
        {
            var win = GetWindow<IP_ObjectEditor>();
            GUIContent content = new GUIContent("Edit Objects");
            win.titleContent = content;
            win.Show();
        }

        private void OnGUI()
        {
            //renamer section
            //get current selected objects
            selected = Selection.gameObjects;
            EditorGUILayout.LabelField("Selected: " + selected.Length.ToString("000"));

            //display our user UI
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);

            EditorGUILayout.BeginVertical();
            GUILayout.Space(10);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Space(10);

            wantedPrefix = EditorGUILayout.TextField("Prefix: ", wantedPrefix, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            wantedName = EditorGUILayout.TextField("Name: ", wantedName, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            wantedSuffix = EditorGUILayout.TextField("Suffix: ", wantedSuffix, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            addNumbering = EditorGUILayout.Toggle("Add Numbering? ", addNumbering);

            GUILayout.Space(10);
            EditorGUILayout.EndVertical();
            
            if(GUILayout.Button("Rename Selected Objects", GUILayout.Height(45), GUILayout.ExpandWidth(true)))
            {
                RenameObject();
            }
            GUILayout.Space(10);
             EditorGUILayout.EndVertical();

            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();

            //grouping section
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(10);

            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Group Name", EditorStyles.boldLabel);
            wantedGroup = EditorGUILayout.TextField(wantedGroup);

            if(GUILayout.Button("Group Selected Objects", GUILayout.ExpandWidth(true), GUILayout.Height(45)))
            {
                Debug.Log("Grouping Selected!");
            }

            EditorGUILayout.Space(10);
            EditorGUILayout.EndVertical();

             EditorGUILayout.Space(10);
            EditorGUILayout.EndHorizontal();

            Repaint();
        }
        #endregion

        #region Custom Methods
        void RenameObject()
        {
            Array.Sort(selected, delegate(GameObject objA, GameObject objB){return objA.name.CompareTo(objB.name);});
            
            for(int i = 0; i < selected.Length; i++)
            {
                string finalName = string.Empty;
                if(wantedPrefix.Length > 0)
                {
                    finalName += wantedPrefix;
                }

                if(wantedName.Length > 0)
                {
                    finalName += "_" + wantedName;
                }

                if(wantedSuffix.Length > 0)
                {
                    finalName += "_" + wantedSuffix;
                }

                if(addNumbering)
                {
                    finalName+= "_" + (i+1).ToString("000");
                }

                selected[i].name = finalName;
            }
        }
        #endregion
    }
}