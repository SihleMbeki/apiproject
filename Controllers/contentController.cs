using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class contentController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        public contentController(DataContext context, ITokenService tokenService, IUserRepository userRepository)
        {
            _context = context;
            _userRepository=userRepository;
        }
        [Authorize]
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
        [Authorize]
        [HttpPost("child/{username}")]
        public async Task<ActionResult<ChildDto>> Child(ChildDto childDto,string username)
        {
            var user=_userRepository.GetUserByUsernameAsync(username);
            if(user==null){
               return NotFound();
            }
            var child = new Child
            {
                age = childDto.age,
                name = childDto.name,
                gender = childDto.gender,
                parent = user.Id

            };
            _context.child.Add(child);
            await _context.SaveChangesAsync();
            childDto.parent=child.parent;

            return childDto;
        }
        [Authorize]
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

        //get School
        [Authorize]
         [HttpGet("school")]
        public async Task<IEnumerable<API.Entities.School>> FindSchool(SchoolDto schoolDto)
        {
        if(await _userRepository.GetSchoolsAsync(schoolDto.Province, schoolDto.City)==null){
        return null;
        }else{
            return await _userRepository.GetSchoolsAsync(schoolDto.Province, schoolDto.City);
        }
        }

        //get symptoms
        [Authorize]
        [HttpGet("Symptoms/{id}")]
        public async Task<IEnumerable<API.Entities.Symptoms>> FindSymptoms(int id)
        {
        if(await _userRepository.GetSymptoms(id)==null){
        return null;
        }else{
            return await _userRepository.GetSymptoms(id);
        }
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.users.AnyAsync(x => x.userName == username.ToLower());
        }
    }
}