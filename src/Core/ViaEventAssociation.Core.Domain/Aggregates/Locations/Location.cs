using ViaEventAssociantion.Core.domain.LocationProperties;
using ViaEventAssociation.Core.Tools.OperationResult;
using Type = System.Type;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class Location
{
    public LocationId Id { get; set; }
    public Type Type { get; set; }
    public string Name { get; set; }
    public Capacity Capacity { get; set; }

    public ResultBase UpdateName(string name)
    {
        Name = name;
        // return new Result<string>(new List<string> { "randomErrorMessage" });
        return new ResultBase();
    }
}
