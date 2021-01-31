namespace Ship.Game.Event
{
    public abstract class EventObject
    {

        private EEventType eventType;

        public EventObject(EEventType eventType)
        {
            this.eventType = eventType;
        }

        public EEventType GetEventType()
        {
            return eventType;
        }
    }
}
