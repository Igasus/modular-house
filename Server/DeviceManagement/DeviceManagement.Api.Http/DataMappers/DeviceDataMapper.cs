using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class DeviceDataMapper
{
    public static DeviceCreatedResponse ToCreatedResponse(this DeviceDto device)
    {
        return new DeviceCreatedResponse(device.Id, device.AdditionDate);
    }
}