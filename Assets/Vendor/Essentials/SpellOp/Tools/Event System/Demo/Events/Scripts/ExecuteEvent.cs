using SpellOp.Tools.Events;
using UnityEngine;

namespace SpellOp.Demo.Events.Scripts
{
   public class ExecuteEvent : MonoBehaviour
   {
      [SerializeField] private string eventName;
      public void Execute()
      {
         SpGameEvent.Trigger(eventName);
      }
   }
}
