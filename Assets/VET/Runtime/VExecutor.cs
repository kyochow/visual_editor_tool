/** 
 *Author:       Kyo Zhou
 *Descrp:       Visual Script executor for editor script
*/
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VET.Node;

namespace VET
{
    public class VExecutor
    {
        public static bool BatchMode = false;
        private static void Do(ScriptGraphAsset sg,Dictionary<string,string> dict)
        {
            //clone one , the origin asset will be protected
            var sga = sg.Clone(null, false);
            GraphReference graphReference = GraphReference.New(sga, true);

            if (!BatchMode)
            {
                if (dict != null)
                {
                    foreach (var kv in dict)
                    {
                        sga.graph.variables.Set(kv.Key,kv.Value);
                    }
                }
            }
            else
            {
                GraphWindow.OpenActive(graphReference);
                var win = GraphWindow.active;
                win.Repaint();
                win.graphInspectorEnabled = false;
                win.variablesInspectorEnabled = false;
            }

            var flow = Flow.New(graphReference);
            
            //find the start node
            IUnit unitCurr = null;
            foreach (var unit in sga.graph.units)
            {
                if (unit.GetType() == typeof(StartNode))
                {
                    unitCurr = unit;
                    break;
                }
            }
            
            if (unitCurr == null)
            {
                Debug.LogError("load VS Asset Error No EVT/StartNode");
                return;
            }

            foreach (var cot in unitCurr.controlOutputs)
            {
                flow.Run(cot);
            }
            Debug.Log("Build Finish");
        }

        /// <summary>
        /// got Commandline params and insert to sga.graph.variables
        /// </summary>
        /// <param name="sga"></param>
        private static Dictionary<string,string> HandleCommandLineArgs()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length <= 0)
                return null;
            Dictionary<string, string> pms = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                var spstrs = arg.Split('=');
                if (spstrs.Length == 2)
                {
                    Debug.Log($"Got param {spstrs[0]} : {spstrs[1]}");
                    pms.Add(spstrs[0],spstrs[1]);
                    // sga.graph.variables.Set(spstrs[0],spstrs[1]);
                }
            }

            return pms;
        }

#if UNITY_EDITOR
        
        /// <summary>
        /// run plan normally
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="planName"></param>
        public static void Run(ScriptGraphAsset sg)
        {
            BatchMode = false;
            Do(sg,null);
        }
        
        /// <summary>
        /// run plan by commandline
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="planName"></param>
        public static void BatchRun()
        {
            BatchMode = true;
            var dict = HandleCommandLineArgs();
            if (!dict.ContainsKey("group") || !dict.ContainsKey("plan"))
                throw new Exception("no groupName or planName");
            
            string group = dict["group"];
            string plan= dict["plan"];
            var setting = GetVETSetting();
            var fullPath = $"{setting.PlansPath}/{group}/{plan}.asset";
            var sga = UnityEditor.AssetDatabase.LoadAssetAtPath<ScriptGraphAsset>(fullPath);
            Do(sga,dict);
        }
        
        //Load the global VET setting
        public static VETSetting GetVETSetting()
        {
            var fs =UnityEditor.AssetDatabase.FindAssets("t:VETSetting",new []{"Assets"});
            if (fs.Length == 0)
                throw new Exception("There is no VETSetting");
            if(fs.Length >= 2)
                throw new Exception("There is 2 VETSetting");
            return UnityEditor.AssetDatabase.LoadAssetAtPath<VETSetting>(UnityEditor.AssetDatabase.GUIDToAssetPath(fs[0]));
        }
#endif
    }
}