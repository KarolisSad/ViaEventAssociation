using ViaEventAssociantion.Core.domain.LocationProperties;
using Type = ViaEventAssociantion.Core.domain.LocationProperties.Type;

namespace ViaEventAssociantion.Core.domain;

public class Location
{
    public LocationId Id { get; set; }
    public Type Type { get; set; }
    public Capacity Capacity { get; set; }
}