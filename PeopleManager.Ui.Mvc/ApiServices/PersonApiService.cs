using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Ui.Mvc.ApiServices.Extensions;
using PeopleManager.Ui.Mvc.Stores;
using Vives.Services.Model;

namespace PeopleManager.Ui.Mvc.ApiServices
{
    public class PersonApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenStore _tokenStore;

        public PersonApiService(
            IHttpClientFactory httpClientFactory,
            TokenStore tokenStore)
        {
            _httpClientFactory = httpClientFactory;
            _tokenStore = tokenStore;
        }

        public async Task<IList<PersonResult>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            httpClient.AddAuthorization(_tokenStore.GetToken());
            var route = "/api/people";
            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var people = await httpResponse.Content.ReadFromJsonAsync<IList<PersonResult>>();

            return people ?? new List<PersonResult>();
        }

        public async Task<PersonResult?> Get(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            httpClient.AddAuthorization(_tokenStore.GetToken());
            var route = $"/api/people/{id}";
            var httpResponse = await httpClient.GetAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            return await httpResponse.Content.ReadFromJsonAsync<PersonResult>();
        }

        public async Task<ServiceResult<PersonResult?>> Create(PersonRequest person)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            httpClient.AddAuthorization(_tokenStore.GetToken());
            var route = "/api/people";
            var httpResponse = await httpClient.PostAsJsonAsync(route, person);

            httpResponse.EnsureSuccessStatusCode();

            var serviceResult = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<PersonResult?>>();
            if (serviceResult is null)
            {
                return new ServiceResult<PersonResult?>().ApiError();
            }
            return serviceResult;
        }

        public async Task<ServiceResult<PersonResult?>> Update(int id, PersonRequest person)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            httpClient.AddAuthorization(_tokenStore.GetToken());
            var route = $"/api/people/{id}";
            var httpResponse = await httpClient.PutAsJsonAsync(route, person);

            httpResponse.EnsureSuccessStatusCode();

            var serviceResult = await httpResponse.Content.ReadFromJsonAsync<ServiceResult<PersonResult?>>();
            if (serviceResult is null)
            {
                return new ServiceResult<PersonResult?>().ApiError();
            }
            return serviceResult;
        }

        public async Task<ServiceResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            httpClient.AddAuthorization(_tokenStore.GetToken());
            var route = $"/api/people/{id}";
            var httpResponse = await httpClient.DeleteAsync(route);

            httpResponse.EnsureSuccessStatusCode();

            var serviceResult = await httpResponse.Content.ReadFromJsonAsync<ServiceResult>();
            if (serviceResult is null)
            {
                return new ServiceResult().ApiError();
            }
            return serviceResult;
        }
    }
}
