using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VET
{
    public class VETSetting : ScriptableObject
    {
        [Header("PlansPath(begin with Assets/)")]
        public string PlansPath;
    }
}