#if UNITY_EDITOR
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace VET.Node
{
    [UnitCategory("VET/Build/SetScriptingDefineSymbols")]
    public class SetScriptingDefineSymbols : BaseBuildNode
    {
        public ValueInput group;
        public ValueInput sym;
        protected override void Definition()
        {
            base.Definition();
            group = ValueInput("group", BuildTargetGroup.Android);
            sym = ValueInput("sym", "");
        }

        public override void Process(Flow flow)
        {
            var gp = (BuildTargetGroup)defaultValues["group"];
            var sym = defaultValues["sym"].ToString();
            PlayerSettings.SetScriptingDefineSymbolsForGroup(gp, sym);
        }
        
        public override string GetPrefix()
        {
            return " SetScriptingDefineSymbols ";
        }
    }
}
#endif