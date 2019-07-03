using BuildVerse;

namespace CreativerseMod.Helpers
{
    public static class GameHelper
    {
        public static void Dispatch(this EntityComponentMessage msg)
        {
            var componentObject = msg.ComponentObject;
            componentObject.SendToServer(msg);
            componentObject.OnComponentMessage(msg);
        }
    }
}