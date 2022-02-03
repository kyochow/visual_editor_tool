#if UNITY_EDITOR
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace VET.Node
{
    [UnitCategory("VET/Build/BuildApk")]
    public class BuildApk : BaseBuildNode
    {
        private const string KEY_DEVELOPMENT = "Development";
        public ValueInput Development;
        
        protected override void Definition()
        {
            base.Definition();
            Development = ValueInput(KEY_DEVELOPMENT, true);
        }
        
        public override void Process(Flow flow)
        {
            var opt = new BuildPlayerOptions();
            if(bool.Parse(defaultValues[KEY_DEVELOPMENT].ToString()) == true)
                opt.options = BuildOptions.Development;
            opt.target = BuildTarget.Android;
            opt.locationPathName = Application.dataPath + "/../../builds/test.apk";
            BuildPipeline.BuildPlayer(opt);
        }
        
        public override string GetPrefix()
        {
            return " BuildApk ";
        }
    }
}
#endif