using ModularHouse.Server.DeviceManagement.Application.Dto;
using ModularHouse.Shared.Models.Responses.DMS;

namespace ModularHouse.Server.DeviceManagement.Api.Http.DataMappers;

public static class DeviceDataMapper
{
    public static DeviceResponse ToResponse(this DeviceDto device)
    {
        return new DeviceResponse(device.Id, device.AdditionDate);
    }
}