
namespace SpellOp.Tools.Events
{
    public interface ISpEventListener<in T> : ISpEventListenerBase
    {
        public void OnSpEvent(T spEventData);
    }
}
