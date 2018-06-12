using System.Data;

namespace testewappa.Integration
{
    public interface IGoogleAddress
    {
         string[] GetCoordinates(string address); 
    }
}