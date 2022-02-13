#if UNITY_EDITOR
using Unity.VisualScripting;

namespace VET.Node
{
   [UnitCategory("VET/Build/BuildBase(No Used)")]
   public class BaseBuildNode : Unit
   {
      [DoNotSerialize] public ControlInput inputTrigger;
      [DoNotSerialize] public ControlOutput outputTrigger;

      public VariableDeclarations Variables => graph.variables;
      
      protected override void Definition()
      {
         inputTrigger = ControlInput("inputTrigger", (flow) =>
         {
            Process(flow);
            return outputTrigger;
         });
         outputTrigger = ControlOutput("outputTrigger");
      }

      public virtual void Process(Flow flow) { }

      public virtual string GetPrefix()
      {
         return null;
      }
   }
}
#endif