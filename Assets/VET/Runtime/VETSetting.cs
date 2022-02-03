using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VET
{
    [CreateAssetMenu(menuName = "VET/VETSetting", fileName = "VETSetting", order = 81)]
    public class VETSetting : ScriptableObject
    {
        [Header("PlansPath(begin with Assets/)")]
        public string PlansPath;
    }
}