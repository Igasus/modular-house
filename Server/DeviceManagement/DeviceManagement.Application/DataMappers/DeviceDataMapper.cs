using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.DataMappers;

public static class DeviceDataMapper
{
    public static DeviceDto ToDto(this Device device)
    {
        return new DeviceDto(device.Id, device.AdditionDate);
    }
}