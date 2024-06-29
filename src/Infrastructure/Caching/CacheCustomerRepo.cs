using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Infrastructure.EfCore.PgRepos;

namespace Infrastructure.Caching
{
    public sealed class CacheCustomerRepo : ICustomerRepo
    {
        private readonly PgCustomerRepo _customerRepo;
        
        private readonly IMemoryCache _cache;

        public CacheCustomerRepo(PgCustomerRepo customerRepo, IMemoryCache cache)
        {
            _customerRepo = customerRepo;
            _cache = cache;
        }

        public async Task AddAsync(Customer entity)
        {
            await _customerRepo.AddAsync(entity);
            _cache.Remove($"{nameof(Customer)}:{entity.Id}");
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            var cacheKey = $"{nameof(Customer)}:{id}";

            var customer = _cache.Get<Customer>(cacheKey);

            if (customer is not null)
            {
                return customer;
            }

            customer = await _customerRepo.GetByIdAsync(id);

            _cache.Set<Customer>(cacheKey, customer);
            return customer;
        }

        public async Task<bool> IsExistAsymc(int id)
        => await _customerRepo.IsExistAsymc(id);

        public async Task<bool> IsEmailInUseAsync(string email)
        => await _customerRepo.IsEmailInUseAsync(email);

        public async Task<bool> IsPhoneNumberInUseAsync(string phoneNumber)
        => await _customerRepo.IsPhoneNumberInUseAsync(phoneNumber);

        public void Update(Customer updatedCustomer)
        {
            _customerRepo.Update(updatedCustomer);
            _cache.Remove($"{nameof(Customer)}:{updatedCustomer.Id}");
        }
    }
}