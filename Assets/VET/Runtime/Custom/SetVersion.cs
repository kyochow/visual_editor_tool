#if UNITY_EDITOR
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace VET.Node
{
    [UnitCategory("VET/Build/SetVersion")]
    public class SetVersion : BaseBuildNode
    {
        private string KEY_OVERRIDE = "version";
        
        public ValueInput X;
        public ValueInput Y;
        public ValueInput Z;
        
        protected override void Definition()
        {
            base.Definition();
            X = ValueInput("X", "0");
            Y = ValueInput("Y", "0");
            Z = ValueInput("Z", "0");
        }

        public override void Process(Flow flow)
        {
            if (Variables.IsDefined(KEY_OVERRIDE))
            {
                Debug.Log($"process setversion {Variables.Get(KEY_OVERRIDE)}");
            }
            else
            {
                Debug.Log($"process setversion self {flow.GetValue(X)},{flow.GetValue(Y)},{flow.GetValue(Z)}");
            }
        }
        
        public override string GetPrefix()
        {
            return " SetVersion ";
        }
    }
}
#endif