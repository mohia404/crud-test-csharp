using CustomerManager.Contracts.Customers;
using MapsterMapper;
using Mc2.CrudTest.Presentation.Shared;
using Mc2.CrudTest.Presentation.Shared.Models;

namespace Mc2.CrudTest.Presentation.Client.Services;

public class CustomerService
{
    private readonly HttpService _httpService;
    private readonly ILogger<CustomerService> _logger;
    private readonly IMapper _mapper;
    private readonly string _baseAddress;

    public CustomerService(HttpService httpService, ILogger<CustomerService> logger, IMapper mapper)
    {
        _httpService = httpService;
        _logger = logger;
        _mapper = mapper;
        _baseAddress = $"companies/{CompanyConfiguration.CompanyId}/";
    }

    public async Task<CustomerDto?> GetByEmailAsync(string email)
    {
        _logger.LogInformation("Fetching Customer {email} from API.", email);

        CustomerResult? result = await _httpService.HttpGetAsync<CustomerResult>(_baseAddress + "customers/" + email);

        return result is null ? null : _mapper.Map<CustomerDto>(result);
    }

    public async Task<List<CustomerDto>?> ListAsync()
    {
        _logger.LogInformation("Fetching Customer Results from API.");

        List<CustomerResult>? result = await _httpService.HttpGetAsync<List<CustomerResult>>(_baseAddress + "customers");

        return result is null ? null : _mapper.Map<List<CustomerDto>>(result);
    }

    public async Task<bool> CreateAsync(CustomerDto request)
    {
        _logger.LogInformation("Create Customer");

        CustomerRequest customer = _mapper.Map<CustomerRequest>(request);

        return await _httpService.HttpPostAsync(_baseAddress + "customers", customer);
    }

    public async Task<bool> UpdateAsync(string email, CustomerDto request)
    {
        _logger.LogInformation("Update Customer {email}", email);

        CustomerRequest customer = _mapper.Map<CustomerRequest>(request);

        return await _httpService.HttpPutAsync(_baseAddress + "customers/" + email, customer);
    }

    public async Task<bool> DeleteAsync(string email)
    {
        _logger.LogInformation("Delete Customer {email}", email);

        return await _httpService.HttpDeleteAsync(_baseAddress + "customers", email);
    }
}