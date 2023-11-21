using ModularHouse.Server.DeviceManagement.Application.CQRS.QueryResponses;
using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Server.DeviceManagement.Domain.DeviceAggregate;

namespace ModularHouse.Server.DeviceManagement.Application.DataMappers;

public static class DeviceDataMapper
{
    public static GetDeviceQueryResponse ToResponse(this Device device)
    {
        return new GetDeviceQueryResponse(new DeviceDto(device.Id, device.AdditionDate));
    }
}