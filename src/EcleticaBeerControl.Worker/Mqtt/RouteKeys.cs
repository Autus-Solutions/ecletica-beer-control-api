namespace EcleticaBeerControl.Worker.Mqtt
{
    public static class RouteKeys
    {
        public static readonly string DeviceTemepratureChangedRoute = "device/temperature-changed";
        public static readonly string DeviceFeedbackRoute = "device/feedback";
        public static string DeviceMeIdentifierRoute(string deviceIdentifier) => $"device/me/{deviceIdentifier}";
    }
}
