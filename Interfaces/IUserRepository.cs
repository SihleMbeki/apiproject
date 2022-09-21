using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<IEnumerable<School>> GetSchoolsAsync(String province,String city);
        Task<IEnumerable<Symptoms>> GetSymptoms(int  childId);
        Task<ChildDto> GetChildrens(string username);
        Task<School> GetSchoolByIDAsync(int Id);
        Task<School> GetSymptomByIDAsync(int Id);
    }
}