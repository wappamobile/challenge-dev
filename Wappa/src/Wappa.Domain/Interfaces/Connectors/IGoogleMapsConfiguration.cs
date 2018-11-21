namespace Wappa.Domain.Interfaces.Connectors
{
    public interface IGoogleMapsConfiguration
    {
        string Url { get; }
        string Key { get; }
    }
}