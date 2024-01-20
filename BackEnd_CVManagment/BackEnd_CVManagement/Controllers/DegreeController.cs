using AutoMapper;
using BackEnd_CVManagement.Core.Context;
using BackEnd_CVManagement.Core.DTOs.Candidate;
using BackEnd_CVManagement.Core.DTOs.Degree;
using BackEnd_CVManagement.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_CVManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DegreeController : ControllerBase
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }
        public DegreeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // CRUD 

        // Read 
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<DegreeGetDto>>> GetDegrees()
        {
            var degrees = await _context.Degrees.OrderByDescending(q => q.CreationTime).ToListAsync();
            var convertedDegrees = _mapper.Map<IEnumerable<DegreeGetDto>>(degrees);
            return Ok(convertedDegrees);
        }

        // Create 
        [HttpPost]
        [Route("CreateDegree")]
        public async Task<IActionResult> CreateDegree([FromBody] DegreeCreateDto dto)
        {
            Degree newDegree = _mapper.Map<Degree>(dto);
            await _context.Degrees.AddAsync(newDegree);
            await _context.SaveChangesAsync();

            return Ok("Degree Created Successfully");
        }


        // Update
        [HttpPatch]
        [Route("UpdateDegree/{id}")]
        public async Task<ActionResult> UpdateDegree(long id, [FromBody] DegreeUpdateDto patch)
        {
            var ReadDegree = _context.Degrees.Find(id);

            if (ReadDegree == null)
            {
                return NotFound();
            }

            var DegreeUpdateDto = _mapper.Map<DegreeUpdateDto>(ReadDegree);

            DegreeUpdateDto = patch;

            ReadDegree.DegreeName = DegreeUpdateDto.DegreeName;
            ReadDegree.IsAssociated = DegreeUpdateDto.IsAssociated;
            await _context.SaveChangesAsync();

            return Ok(DegreeUpdateDto);
        }

        // Delete
        [HttpDelete]
        [Route("DeleteDegrees/{id}")]
        public bool DeleteDegrees(long id)
        {
            bool a = false;
            var Degree = _context.Degrees.Find(id);
            if (Degree != null)
            {
                a = true;
                _context.Entry(Degree).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            else
            {
                a = false;
            }
            return a;
        }
    }
}
