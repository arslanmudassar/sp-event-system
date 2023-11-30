using UnityEngine;
using UnityEngine.Events;

namespace SpellOp.Tools.Events
{
    [AddComponentMenu("SpTools/SpEventListener")]
    public class SpEventListener : MonoBehaviour, ISpEventListener<SpGameEvent>
    {
        [SerializeField] private string eventName;
        
        [SerializeField] private UnityEvent onSpGameEvent;

        private void OnEnable()
        {
            this.SpEventStartListening<SpGameEvent>();
        }

        private void OnDisable()
        {
            this.SpEventStopListening<SpGameEvent>();
        }

        public void OnSpEvent(SpGameEvent spEventData)
        {
            if(spEventData.EventName != eventName) return;
            onSpGameEvent?.Invoke();
        }
    }
}
