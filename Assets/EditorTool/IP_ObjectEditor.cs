using System.Collections;
using System.Collections.Generic;
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
        bool addNumbering;
        #endregion

        #region Builtin Methods
        public static void LaunchEditor()
        {
            var win = GetWindow<IP_ObjectEditor>();
            GUIContent content = new GUIContent("Rename Objects");
            win.titleContent = content;
            win.Show();
        }

        private void OnGUI()
        {
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
                Debug.Log("Renaming Objects");
            }
            GUILayout.Space(10);
             EditorGUILayout.EndVertical();

            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();

            Repaint();
        }
        #endregion

        #region Custom Methods
        #endregion
    }
}