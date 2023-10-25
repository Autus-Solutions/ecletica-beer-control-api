namespace EcleticaBeerControl.Worker.Mqtt
{
    public static class RouteKeys
    {
        #region Device To Application Subscriptions

            public static readonly string DeviceApplicationTemepratureChangedRoute = "device/application/temperature-changed";
            public static readonly string DeviceApplicationHandshakeResultRoute = "device/application/handshake-result";

        #endregion

        #region Application To Device Publications

            public static string ApplicationDeviceHandshakeRoute(string deviceIdentifier) => $"application/device/handshake/{deviceIdentifier}";
            public static string ApplicationDeviceDisconnectRoute(string deviceIdentifier) => $"application/device/disconnect/{deviceIdentifier}";
            public static string ApplicationDeviceTemperatureChangeRoute(string deviceIdentifier) => $"application/device/temperature-change/{deviceIdentifier}";

        #endregion
    }
}
