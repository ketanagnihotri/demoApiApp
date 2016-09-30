using System;

namespace Kenobi.TripsExtension.TripsRepository.Util
{
    internal static class Converter
    {
        internal static T StringToEnum<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name, true);
        }
    }
}
