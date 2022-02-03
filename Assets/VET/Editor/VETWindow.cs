/** 
 *Author:       Kyo Zhou
 *Descrp:       Visual Editor Tool Main Window
*/
using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using VET;


public class VETWindow : EditorWindow
{
    public static void ShowWindow()
    {
        var win = CreateInstance<VETWindow>();
        win.titleContent = new GUIContent("VET Window");
        win.minSize = new Vector2(300, 600);
        win.maxSize = new Vector2(500, 1200);
        win.Show(true);
    }
    
    private VETSetting             _setting;
    private string                 _plansPath;  
    private string                 _planType;   

    private DropdownField          _planDrop;
    private Label                  _lbTitle; 
    private Label                  _lbDesc;  
    private ListView               _listView;
    private List<string>           _listPlanGroups = new List<string>();
    private string                 _currPlanGroup;
    private List<string>           _listPlans = new List<string>();
    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;
        
        _listView = new ListView();
        
        Refresh();
        
        _planDrop = new DropdownField("Plan Group",_listPlanGroups,0,OnPlanType);
        _lbTitle = new Label();
        _lbDesc = new Label();
        PrepareListView();

        root.Add(_planDrop);
        root.Add(_listView);
        root.Add(_lbTitle);
        root.Add(_lbDesc);
    }

    public void OnDestroy()
    {
        _listView.Clear();
        _listView = null;
    }

    private void PrepareListView()
    {
        _listView.style.width = new StyleLength(Length.Percent(100));
        _listView.style.height = new StyleLength(Length.Percent(100));
        _listView.style.left = 1;
        _listView.style.right = 1;
        _listView.style.top = 1;
        _listView.style.bottom = 1;
        _listView.showBorder = true;
        _listView.itemHeight = 25;
        _listView.makeItem = () =>
        {
            var label = new Label();
            label.AddManipulator(new ContextualMenuManipulator((ContextualMenuPopulateEvent evt) =>
            {
                evt.menu.AppendAction("Run Plan", (e) =>
                {
                    RunGraph(label.text);
                });
                evt.menu.AppendAction("Edit Plan", (e) =>
                {
                    EditGraph(label.text);
                });
            }));
            return label;
        };
        _listView.bindItem = (lb,i)=>((Label)lb).text = _listPlans[i];
        _listView.onSelectionChange += (objects) =>
        {
            foreach (var obj in objects)
            {
                var fullPath = $"Path : {_plansPath}/{_currPlanGroup}/{obj}.asset";
                var sga = SelectGraph(obj.ToString());
                
                var title = string.Empty;
                if(!string.IsNullOrEmpty(sga.graph.title))
                    title = $"Title : {sga.graph.title}";
                _lbTitle.text = $"{fullPath}\n{title}";
                
                var desc = string.Empty;
                if (!string.IsNullOrEmpty(sga.graph.summary))
                    desc = $"Summary :\n {sga.graph.summary}";
                _lbDesc.text = desc;
            }
        };
    }

    private void Refresh()
    {
        _setting = VETMenu.GetVETSetting();
        _plansPath = _setting.PlansPath;
        DirectoryInfo diRoot = new DirectoryInfo(_plansPath);
        if (!diRoot.Exists)
        {
            throw new Exception(" plan path not exist,please set in VETSetting");
        }
        
        var dis = diRoot.GetDirectories();
        _listPlanGroups.Clear();
        foreach (var di in dis)
        {
            _listPlanGroups.Add(di.Name);
        }
    }

    private string OnPlanType(string dma)
    {
        _currPlanGroup = dma;
        if (!string.IsNullOrEmpty(_currPlanGroup))
        {
            string fullDir = _plansPath + "/" + _currPlanGroup;
            DirectoryInfo di = new DirectoryInfo(fullDir);
            FileInfo[] fis = di.GetFiles("*.asset", SearchOption.AllDirectories);
            
            _listView.Clear();
            _listPlans.Clear();
            foreach (var fi in fis)
            {
                _listPlans.Add(Path.GetFileNameWithoutExtension(fi.Name));
            }

            _listView.itemsSource = _listPlans;
            _listView.Refresh();
            _listView.ClearSelection();
        }
        return dma;
    }

    private ScriptGraphAsset SelectGraph(string planName)
    {
        var fullPath = $"{_plansPath}/{_currPlanGroup}/{planName}.asset";
        Debug.Log($"select plan {fullPath}");
        return AssetDatabase.LoadAssetAtPath<ScriptGraphAsset>(fullPath);
    }
    
    private void EditGraph(string planName)
    {
        var sga = SelectGraph(planName);
        GraphReference graphReference  = GraphReference.New(sga, true);
        GraphWindow.OpenActive(graphReference);
    }
    
    private void RunGraph(string planName)
    {
        var sga = SelectGraph(planName);
        VExecutor.Do(sga);
    }
}