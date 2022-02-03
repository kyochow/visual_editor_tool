/** 
 *Author:       Kyo Zhou
 *Descrp:       Visual Script executor for editor script
*/
using System;
using Unity.VisualScripting;
using UnityEngine;
using VET.Node;

namespace VET
{
    public class VExecutor
    {
        public static void Do(ScriptGraphAsset sg)
        {
            // GraphWindow win = null;
            //现克隆一份，防止原配置被修改掉
            var sga = sg.Clone(null, false);
            GraphReference graphReference = null;
            graphReference = GraphReference.New(sga, true);
            GraphWindow.OpenActive(graphReference);
            var win = GraphWindow.active;
            win.Repaint();
            win.graphInspectorEnabled = false;
            win.variablesInspectorEnabled = false;
            
            HandleCommandLineArgs(sga);


            var flow = Flow.New(graphReference);
            
            //现找到开始节点
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

            TryHighlight(unitCurr);
            
            
            foreach (var cot in unitCurr.controlOutputs)
            {
                flow.Run(cot);
            }
            Debug.Log("Build Finish");
        }

        /// <summary>
        /// 处理Commandline参数，直接塞到sga.graph.variables里去，这样每一个Node都可以拿到
        /// </summary>
        /// <param name="sga"></param>
        private static void HandleCommandLineArgs(ScriptGraphAsset sga)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length <= 0)
                return;
            foreach (var arg in args)
            {
                var spstrs = arg.Split('=');
                if (spstrs.Length == 2)
                {
                    Debug.Log($"Got param {spstrs[0]} : {spstrs[1]}");
                    sga.graph.variables.Set(spstrs[0],spstrs[1]);
                }
            }
        }

        public static void TryHighlight(IGraphElement item)
        {
            var win = GraphWindow.active;
            if (win != null)
                win.context.canvas.selection.Add(item);
        }
    }
}