#if UNITY_EDITOR
using Unity.VisualScripting;
namespace VET.Node
{
   [UnitCategory("VET/Start")]
   public class StartNode : Unit
   {
      [DoNotSerialize] public ControlOutput outputTrigger;

      protected override void Definition()
      {
         outputTrigger = ControlOutput("outputTrigger");
      }
   }
}
#endif