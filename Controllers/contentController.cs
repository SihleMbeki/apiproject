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
        private readonly ITokenService _tokenService;
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
        public async Task<ActionResult<SymptomsDto>> Child(SymptomsDto symptomsDto)
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
        
        [HttpPost("school")]
        public async Task<ActionResult<SchoolDto>> School(SchoolDto SchoolDto)
        {
            var school = new School
    {

       Name =SchoolDto.Name,
        PostalCode=SchoolDto.PostalCode,
        Street  =SchoolDto.Name,
        Surbub  =SchoolDto.Name,
         Phone   =SchoolDto.Name,
        Email  =SchoolDto.Name,
        Province   =SchoolDto.Name,
        City   =SchoolDto.Name,
       link   =SchoolDto.Name
    };
            _context.schools.Add(school);
            await _context.SaveChangesAsync();

            return SchoolDto;
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.users.AnyAsync(x => x.userName == username.ToLower());
        }
    }
}