# visual_editor_tool
THE visual editor tool framework for unity 2021+(if lower then 2021,please integrate blot manually)
Visual Editor Tools(VET)

####目标
VET是一个极简的工具框架，目的在于提出一个统一思路，来解决项目中Editor工具链维护困难、不够直观的问题,VisualScripting（VS）是一个好的机制，但是它当前版本不支持Editor下运行，稍作处理即可

####特性
- 1，实现以往手写编辑器工具的可视化，避免因项目庞大、人员变动等原因失控
- 2，基于Node-base模块化，高复用性，借用VisualScripting机制，具有高扩展性
- 3，支持命令行调用及参数，适用各种应用场景
- 4，基于UIElement编写界面，css风格也算有趣

####开始
- 1，目录结构


- 2 ,VETSetting(右键->Create->VET->VETSetting)，这里只需要设置Plan路径，以便支持玩家自定义VET位置





- 3 ，VETWindow(Window->VET->VETWindow

- 4，Edit Plan (使用unity内置的Visual Script机制)，特别的，第一个节点必须是Vet/Start



####命令行调用

[UnityDir]/Unity -quit -batchmode -projectPath [ProjDir] -executeMethod VET.VExecutor.BatchRun -logFile [xxx.log] group=[] plan=[]  XXX1=VVV1 XXX2=VVV2

####自定义Node
可以按照VisualScript规则随意定义，但是EditorTool建议如下定义，便于统一管理
