using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserRepository : IUserRepository
    {

        
            private readonly DataContext _context;
             public UserRepository(DataContext context)
        {
            _context = context;
        }
        public Task<ChildDto> GetChildrens(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<School>> GetSchoolsAsync(string province,string city)
        {
            if(province !=null && city ==null)
            return  await _context.schools.Where(x => x.Province == province).ToListAsync<School>();

            if(province !=null && city !=null)
            return  await _context.schools.Where(x => x.Province == province)
            .Where(x => x.City == city).ToListAsync<School>();

            if(province ==null && city !=null)
            return  await _context.schools.Where(x => x.City == city).ToListAsync();

            return null;
        }

        public async Task<IEnumerable<Symptoms>> GetSymptoms(int id)
        {

        var childItem=  _context.child.Where(x => x.Id == id).SingleOrDefault();
            return  await _context.symptoms.Where(x => x.Age == childItem.age)
            .Where(x => x.Gender == childItem.gender).ToListAsync();
        }

        public Task<AppUser> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
          return await _context.users.Where(x => x.userName == username).SingleOrDefaultAsync();
        }

        public async Task<School> GetSchoolByIDAsync(int Id)
        {
          return await _context.schools.FindAsync(Id);
        }
        public async Task<Symptoms> GetSymptomByIDAsync(int Id)
        {
          return await _context.symptoms.FindAsync(Id);
        }

        public Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(AppUser user)
        {
            throw new NotImplementedException();
        }

        Task<School> IUserRepository.GetSymptomByIDAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}