using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ModularHouse.Server.AuthService.Application.Dto;
using ModularHouse.Server.AuthService.Application.HttpClients.UMS;
using ModularHouse.Server.AuthService.Infrastructure.MappingExtensions;
using ModularHouse.Server.Common.Domain;
using ModularHouse.Server.Common.Domain.Exceptions;
using ModularHouse.Shared.Models.Responses.UMS;

namespace ModularHouse.Server.AuthService.Infrastructure.HttpClients;

public class UserHttpClient : IUserHttpClient
{
    private readonly HttpClient _httpClient;
    
    public UserHttpClient(IOptions<HttpClientsOptions> httpClientsOptions)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(httpClientsOptions.Value.UMS.BaseUrl)
        };
    }
    
    public async Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // TODO Add logging
        // TODO Add proper error messages
        var response = await _httpClient.GetAsync($"api/users/{id}", cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            var errorDetails = new List<string>
            {
                $"HTTP GET - api/users/{id} request responded with not 200-OK status-code.",
                $"HttpStatusCode: {response.StatusCode.ToString()}"
            };
            if (!string.IsNullOrWhiteSpace(responseBody))
                errorDetails.Add($"ErrorResponse: {JsonSerializer.Serialize(responseBody)}.");

            throw new InternalServerException(ErrorMessages.InternalServer, errorDetails.ToArray());
        }
        
        var userResponse = JsonSerializer.Deserialize<UserResponse>(responseBody);
        var userResponseAsDto = userResponse.AsDto();

        return userResponseAsDto;
    }

    public async Task<UserDto> CreateUserAsync(UserCreateDto user, CancellationToken cancellationToken = default)
    {
        // TODO Add logging
        // TODO Add proper error messages
        var responseContent = JsonContent.Create(user.AsRequest());
        var response = await _httpClient.PostAsync("api/users", responseContent, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            var errorDetails = new List<string>
            {
                "HTTP POST - api/users request responded with not 200-OK status-code.",
                $"HttpStatusCode: {response.StatusCode.ToString()}"
            };
            if (!string.IsNullOrWhiteSpace(responseBody))
                errorDetails.Add($"ErrorResponse: {JsonSerializer.Serialize(responseBody)}.");
            
            throw new InternalServerException(ErrorMessages.InternalServer, errorDetails.ToArray());
        }
        
        var userResponse = JsonSerializer.Deserialize<UserResponse>(responseBody);
        var userResponseAsDto = userResponse.AsDto();
        
        return userResponseAsDto;
    }
}