using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ObjectEditor.Tools
{
    public class IP_Editor_Menu 
    {
        [MenuItem("Object Editor/Level/Tools/ObjectEditor #e")]
        public static void EditSelectedObjects()
        {
            IP_ObjectEditor.LaunchEditor();
        }
    }
}