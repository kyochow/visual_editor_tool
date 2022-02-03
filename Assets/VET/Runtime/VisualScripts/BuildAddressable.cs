#if UNITY_EDITOR
using Unity.VisualScripting;
// using UnityEngine.AddressableAssets;
// using UnityEditor.AddressableAssets.Build;
// using UnityEditor.AddressableAssets.Settings;
using UnityEngine;


namespace VET.Node
{
    [UnitCategory("VET/Build/BuildAddressable")]
    public class BuildAddressable : BaseBuildNode
    {
        public ValueInput ClearAll;

        protected override void Definition()
        {
            base.Definition();
            ClearAll = ValueInput("ClearAll", false);
        }
        
        public override void Process(Flow flow)
        {
            // if (bool.Parse(dictDefault["ClearAll"].ToString()))
            // {
            //     Debug.Log("clear all assetbundles");
            //     AddressableAssetSettings.CleanPlayerContent();
            // }
            //
            // AddressableAssetSettings.BuildPlayerContent();
        }
        
        public override string GetPrefix()
        {
            return " SetVersion ";
        }
    }
}
#endif