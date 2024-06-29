using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Validators;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EfCore.PgRepos;

public class PgCommonRepo<T> : ICommmonRepo<T> where T : Base
{
    private readonly DataBaseContext _db;

    public PgCommonRepo(DataBaseContext db)
    => _db = db;

    public virtual async Task AddAsync(T entity)
    {
        BusinessModelValidator.Validate((IValidatableObject)entity, DataValidation.All);

        entity.Created = entity.Created.ToUniversalTime();

        entity.Deleted = entity.Deleted.ToUniversalTime();
        
        await _db.AddAsync<T>(entity);
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    => await _db
        .Set<T>()
        .FirstOrDefaultAsync(x => x.Deleted == DateTime.MinValue && x.Id == id);
}
