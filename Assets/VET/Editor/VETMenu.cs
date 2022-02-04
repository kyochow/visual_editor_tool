/** 
 *Author:       Kyo Zhou
 *Descrp:       All VET menu about
*/

using System;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

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
            Selection.activeObject = VExecutor.GetVETSetting();
        }
        
        [MenuItem("Assets/Create/VET/Create VET Group", false, 82)]
        public static void CreatePlanGroup()
        {
            var vetSetting = VExecutor.GetVETSetting();
            if (string.IsNullOrEmpty(vetSetting.PlansPath) || !Directory.Exists(vetSetting.PlansPath))
                throw new Exception("Plans Path not exist");
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(vetSetting.PlansPath, typeof(UnityEngine.Object));

            Selection.activeObject = obj;
            ProjectWindowUtil.CreateFolder();
        }
        
       
    }
}