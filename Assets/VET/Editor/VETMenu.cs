/** 
 *Author:       Kyo Zhou
 *Descrp:       All VET menu about
*/

using System;
using UnityEditor;
using UnityEngine;

namespace VET
{
    public class VETMenu
    {
        [MenuItem("Window/VET/VETWindow", false, 3010)]
        public static void Show()
        {
            VETWindow.ShowWindow();
        }
        
        [MenuItem("Window/VET/VETSetting", false, 3010)]
        public static void ShowSetting()
        {
            Selection.activeObject = GetVETSetting();
        }

        public static VETSetting GetVETSetting()
        {
            var fs =AssetDatabase.FindAssets("t:VETSetting",new []{"Assets"});
            if (fs.Length == 0)
                throw new Exception("There is no VETSetting");
            if(fs.Length >= 2)
                throw new Exception("There is 2 VETSetting");
            return AssetDatabase.LoadAssetAtPath<VETSetting>(AssetDatabase.GUIDToAssetPath(fs[0]));
        }
    }
}