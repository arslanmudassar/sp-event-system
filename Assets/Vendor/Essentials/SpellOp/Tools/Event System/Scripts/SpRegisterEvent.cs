namespace SpellOp.Tools.Events
{
    public static class SpRegisterEvent
    {
        public static void SpEventStartListening<SpEvent>(this ISpEventListener<SpEvent> caller) where SpEvent : struct
        {
            SpEventManager.AddListener(caller);
        }

        public static void SpEventStopListening<SpEvent>(this ISpEventListener<SpEvent> caller) where SpEvent:struct
        {
            SpEventManager.RemoveListener(caller);
        }
    }
}