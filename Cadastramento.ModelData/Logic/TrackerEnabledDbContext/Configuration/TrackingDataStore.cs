using System.Collections.Concurrent;

namespace Cadastramento.ModelData.Logic.TrackerEnabledDbContext.Configuration
{
    internal static class TrackingDataStore
    {
        ////////////////////////// STORE /////////////////////////////

        internal static ConcurrentDictionary<string, TrackingConfigurationValue> EntityConfigStore = new ConcurrentDictionary<string, TrackingConfigurationValue>();
        internal static ConcurrentDictionary<PropertyConfiguerationKey, TrackingConfigurationValue> PropertyConfigStore = new ConcurrentDictionary<PropertyConfiguerationKey, TrackingConfigurationValue>();
    }
}
