using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
    private const string BaseUri = "api/users";
    
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserHttpClient> _logger;
    
    public UserHttpClient(IOptions<HttpClientsOptions> httpClientsOptions, ILogger<UserHttpClient> logger)
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(httpClientsOptions.Value.UMS.BaseUrl) };
        _logger = logger;
    }
    
    public async Task<UserDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var requestUri = $"{BaseUri}/{id}";
        var response = await _httpClient.GetAsync(requestUri, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var errorMessage = ErrorMessages.NotSuccessResponse(HttpMethod.Get, requestUri);
            List<string> errorDetails = [ErrorMessages.HttpResponseStatusCodeDetails(response.StatusCode)];
            if (!string.IsNullOrWhiteSpace(responseBody))
                errorDetails.Add(ErrorMessages.HttpResponseBodyDetails(JsonSerializer.Serialize(responseBody)));

            _logger.LogError(errorMessage);
            throw new InternalServerException(errorMessage, errorDetails.ToArray());
        }
        
        var userResponse = JsonSerializer.Deserialize<UserResponse>(responseBody);
        var userResponseAsDto = userResponse.AsDto();

        return userResponseAsDto;
    }

    public async Task<UserDto> CreateUserAsync(UserCreateDto user, CancellationToken cancellationToken = default)
    {
        var requestUri = BaseUri;
        var requestContent = JsonContent.Create(user.AsRequest());
        var response = await _httpClient.PostAsync(requestUri, requestContent, cancellationToken);
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            var errorMessage = ErrorMessages.NotSuccessResponse(HttpMethod.Post, requestUri);
            List<string> errorDetails = [ErrorMessages.HttpResponseStatusCodeDetails(response.StatusCode)];
            if (!string.IsNullOrWhiteSpace(responseBody))
                errorDetails.Add(ErrorMessages.HttpResponseBodyDetails(JsonSerializer.Serialize(responseBody)));

            _logger.LogError(errorMessage);
            throw new InternalServerException(errorMessage, errorDetails.ToArray());
        }
        
        var userResponse = JsonSerializer.Deserialize<UserResponse>(responseBody);
        var userResponseAsDto = userResponse.AsDto();
        
        return userResponseAsDto;
    }
}