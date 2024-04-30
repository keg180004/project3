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

        //renaming vars
        string wantedPrefix;
        string wantedName;
        string wantedSuffix;
        bool addNumbering;

        //grouping vars
        string wantedGroup = "Enter Group Name";
        int currentSelectionCount;

        //replacing vars
        GameObject wantedObject;
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
            //get current selected objects
            selected = Selection.gameObjects;
            EditorGUILayout.LabelField("Selected: " + selected.Length.ToString("000"));
            
            //renamer section
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical();
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Space(10);

            //UI
            EditorGUILayout.LabelField("Rename Objects", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
            GUILayout.Space(5);
            wantedPrefix = EditorGUILayout.TextField("Prefix: ", wantedPrefix, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            wantedName = EditorGUILayout.TextField("Name: ", wantedName, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            wantedSuffix = EditorGUILayout.TextField("Suffix: ", wantedSuffix, EditorStyles.miniTextField, GUILayout.ExpandWidth(true));
            addNumbering = EditorGUILayout.Toggle("Add Numbering? ", addNumbering);

            //rename button
            if(GUILayout.Button("Rename Selected Objects", GUILayout.Height(45), GUILayout.ExpandWidth(true)))
            {
                RenameObjects();
            }
            GUILayout.Space(10);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
            GUILayout.Space(10);
            EditorGUILayout.EndVertical();
            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();

            //grouping section
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(4/5);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Group Objects", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            GUILayout.Space(5);
            EditorGUILayout.LabelField("Group Name");
            wantedGroup = EditorGUILayout.TextField(wantedGroup);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            //grouping button
            if(GUILayout.Button("Group Selected Objects", GUILayout.ExpandWidth(true), GUILayout.Height(45)))
            {
                GroupObjects();
            }

            EditorGUILayout.Space(10);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(4/5);
            EditorGUILayout.EndHorizontal();

            //replacing section
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space(4/5);

            EditorGUILayout.BeginVertical();
            GUILayout.Space(15);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space(10);

            //UI
            EditorGUILayout.LabelField("Replace Objects", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
            GUILayout.Space(5);

            //replacing button
            wantedObject = (GameObject)EditorGUILayout.ObjectField("New Object: ", wantedObject, typeof(GameObject), true);
            if(GUILayout.Button("Replace Selected Objects", GUILayout.ExpandWidth(true), GUILayout.Height(40)))
            {
                ReplaceObjects();
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(10);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(10);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(4/5);
            EditorGUILayout.EndHorizontal();

            Repaint();
        }
        #endregion

        #region Custom Methods
        void RenameObjects()
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

        void GroupObjects()
        {
            if(selected.Length > 0)
            {
                if(wantedGroup != "Enter Group Name")
                {
                    GameObject groupGO = new GameObject(wantedGroup + "_GRP");

                    foreach(GameObject curgo in selected)
                    {
                        curgo.transform.SetParent(groupGO.transform);
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog("Grouper Message", "You must provide a name for your group!", "OK");
                }
        }
        }

        void ReplaceObjects()
        {
            if(!wantedObject)
            {
                EditorUtility.DisplayDialog("Replace Objects Warning", "New object is missing, please assign something!", "OK");
                return;
            }

            GameObject[] selectedObjects = Selection.gameObjects;
            for(int i = 0; i < selectedObjects.Length; i++)
            {
                Transform selectTransform = selectedObjects[i].transform;
                GameObject newObject = Instantiate(wantedObject, selectTransform.position, selectTransform.rotation);
                newObject.transform.localScale = selectTransform.localScale;

                DestroyImmediate(selectedObjects[i]);
            }
        }
        #endregion
    }
}