namespace EcleticaBeerControl.Worker.Mqtt
{
    public static class RouteKeys
    {
        #region Device To Application Subscriptions

        public static readonly string DeviceApplicationTemepratureChangedRoute = "device/application/temperature-changed";
        public static readonly string DeviceApplicationHandshakeResultRoute = "device/application/handshake-result";

        #endregion

        #region Application To Device Publications

        public static string ApplicationConnectDeviceRoute(string deviceIdentifier) => $"application/device/connect/{deviceIdentifier}";
        public static string ApplicationDisconnectDeviceRoute(string deviceIdentifier) => $"application/device/disconnect/{deviceIdentifier}";
        public static string ApplicationDeviceChangeTemperatureRoute(string deviceIdentifier) => $"application/device/change-temperature/{deviceIdentifier}";

        #endregion
    }
}
