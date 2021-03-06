#### 目标
VET是一个极简的工具框架，目的在于提出一个统一思路，来解决项目中Editor工具链维护困难、不够直观的问题,VisualScripting（VS）是一个好的机制，但是它当前版本不支持Editor下运行，稍作处理即可

#### 特性
- 1，实现以往手写编辑器工具的可视化，避免因项目庞大、人员变动等原因失控
- 2，基于Node-base模块化，高复用性，借用VisualScripting机制，具有高扩展性
- 3，支持命令行调用及参数，适用各种应用场景
- 4，基于UIElement编写界面，css风格也算有趣

#### 开始
- 1 目录结构
![image](https://raw.githubusercontent.com/kyochow/visual_editor_tool/main/Misc/DIR.png)

- 2 VETSetting(右键->Create->VET->VETSetting)，这里只需要设置Plan路径，以便支持玩家自定义VET位置


- 3 VETWindow(Window->VET->VETWindow

![image](https://raw.githubusercontent.com/kyochow/visual_editor_tool/main/Misc/VETWindow.png)

- 4 Edit/Run Plan ,直观的流程界面，支持VS内置节点，特别的是，第一个节点必须是VET/Start
![image](https://raw.githubusercontent.com/kyochow/visual_editor_tool/main/Misc/Graph.png)


#### 命令行调用

[UnityDir]/Unity -quit -batchmode -projectPath [ProjDir] -executeMethod VET.VExecutor.BatchRun -logFile [xxx.log] group=[] plan=[]  XXX1=VVV1 XXX2=VVV2

#### 自定义Node
可以按照VisualScript规则随意定义，但是EditorTool建议如下定义，便于统一管理
![image](https://raw.githubusercontent.com/kyochow/visual_editor_tool/main/Misc/code.png)

##### PS: 新增Node后，需要到ProjectSetting/VisualScript中，刷新一下，否则在GraphWindow中右键->Add Node时无法找到并添加
![image](https://raw.githubusercontent.com/kyochow/visual_editor_tool/main/Misc/psvs.png)
