using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class contentController : BaseApiController
    {
        private readonly DataContext _context;
        public contentController(DataContext context, ITokenService tokenService)
        {
            _context = context;
        }

        [HttpPost("symptoms")]
        public async Task<ActionResult<SymptomsDto>> Register(SymptomsDto symptomsDto)
        {
            var symptom = new Symptoms
    {
        Description=symptomsDto.Description,
        Age =symptomsDto.Age,
        Gender = symptomsDto.Gender,
    };
            _context.symptoms.Add(symptom);
            await _context.SaveChangesAsync();

            return symptomsDto;
        }

        [HttpPost("child")]
        public async Task<ActionResult<ChildDto>> Child(ChildDto childDto)
        {
            var child = new Child
    {
        age=childDto.age,
        name =childDto.name,
        gender=childDto.gender,
        parent = childDto.parent
    };
            _context.child.Add(child);
            await _context.SaveChangesAsync();

            return childDto;
        }
        
        [HttpPost("school")]
        public async Task<ActionResult<SchoolDto>> School(SchoolDto schoolDto)
        {
            var school = new School
            {
                Name = schoolDto.Name,
                PostalCode = schoolDto.PostalCode,
                Street = schoolDto.Street,
                Surbub = schoolDto.Surbub,
                Phone = schoolDto.Phone,
                Email = schoolDto.Email,
                Province = schoolDto.Province,
                City = schoolDto.City,
                link = schoolDto.link
            };
            _context.schools.Add(school);
            await _context.SaveChangesAsync();

            return schoolDto;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.users.AnyAsync(x => x.userName == username.ToLower());
        }
    }
}