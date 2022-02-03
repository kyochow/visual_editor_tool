/** 
 *Author:       Kyo Zhou
 *Descrp:       Toolbar of VETWindow
*/

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

namespace VET
{
    public class VETToolbar
    {
        private DropdownField          _planDrop;
        private List<string>           _listPlanGroups = new List<string>();
        private string                 _currPlanGroup;
        
        private VisualElement          _barRoot;
        public VisualElement           BarRoot => _barRoot;

        private VETSetting             _setting;

        private EditorWindow           _win;
        private Action<string>         _onGroupChange;
        
        public VETToolbar(EditorWindow win, Action<string> onGroupChange)
        {
            _win = win;
            _onGroupChange = onGroupChange;

            Refresh();
            
            _barRoot = new VisualElement();
            _barRoot.style.width = new StyleLength(Length.Percent(100));
            _barRoot.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
        
            _planDrop = new DropdownField("Group",_listPlanGroups,0,OnPlanType);
            _planDrop.labelElement.style.minWidth = 60;
            _planDrop.labelElement.style.width = 60;
            _barRoot.Add(_planDrop);
        
            var btnAddGroup = new Button();
            btnAddGroup.text = "+";
            btnAddGroup.tooltip = "Add a new group";
            btnAddGroup.style.width = 25;
            _barRoot.Add(btnAddGroup);
        
            var btnDeleteGroup = new Button();
            btnDeleteGroup.text = "-";
            btnDeleteGroup.tooltip = "Delete this group";
            btnDeleteGroup.style.width = 25;
            btnDeleteGroup.style.right = 0;
            _barRoot.Add(btnDeleteGroup);
        
            var btnRefresh = new Button();
            btnRefresh.text = "R";
            btnRefresh.tooltip = "Refresh all";
            btnRefresh.style.width = 25;
            btnRefresh.style.right = 0;
            _barRoot.Add(btnRefresh);
        }
        
        public void Update()
        {
            _planDrop.style.width = _win.position.width-102;
        }
        
        private void Refresh()
        {
            _setting = VETMenu.GetVETSetting();
            var plansPath = _setting.PlansPath;
            DirectoryInfo diRoot = new DirectoryInfo(plansPath);
            if (!diRoot.Exists)
            {
                throw new Exception(" plan path not exist,please set in VETSetting");
            }
        
            var dis = diRoot.GetDirectories();
            foreach (var di in dis)
            {
                _listPlanGroups.Add(di.Name);
            }
        }
        
        private string OnPlanType(string dma)
        {
            if (!string.IsNullOrEmpty(dma))
            {
                _currPlanGroup = dma;
                _onGroupChange?.Invoke(_currPlanGroup);
            }
            return dma;
        }
    }
}