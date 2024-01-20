using AutoMapper;
using BackEnd_CVManagement.Core.Context;
using BackEnd_CVManagement.Core.DTOs.Candidate;
using BackEnd_CVManagement.Core.DTOs.Degree;
using BackEnd_CVManagement.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_CVManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private ApplicationDbContext _context { get; }

        private IMapper _mapper { get; }
        public CandidateController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // CRUD 

        // Read 

        [HttpGet]
        [Route("GetCandidates")]
        public async Task<ActionResult<IEnumerable<CandidateGetDto>>> GetCandidates()
        {
            var candidates = await _context.Candidates.Include(candidate => candidate.Degree).OrderByDescending(q => q.CreationTime).ToListAsync();
            var convertedCandidates = _mapper.Map<IEnumerable<CandidateGetDto>>(candidates);
            return Ok(convertedCandidates);
        }

        // Create 
        [HttpPost]
        [Route("CreateCandidate")]
        public async Task<IActionResult> CreateCandidate([FromForm] CandidateCreateDto dto , IFormFile pdfFile)
        {
            // First => Save pdf to Server
            // Then => save url into our entity
            var fiveMegaByte = 5 * 1024 * 1024;
            var pdfMimeType = "application/pdf";

            if (pdfFile.Length > fiveMegaByte || pdfFile.ContentType != pdfMimeType)
            {
                return BadRequest("File is not valid");
            }

            var resumeUrl = Guid.NewGuid().ToString() + ".pdf";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs", resumeUrl);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }
            var newCandidate = _mapper.Map<Candidate>(dto);
            newCandidate.ResumeUrl = resumeUrl;
            
            await _context.Candidates.AddAsync(newCandidate);
            await _context.SaveChangesAsync();

            return Ok("Candidate Created Successfully");

        }

        // Update
        [HttpPatch]
        [Route("UpdateCandidate/{id}")]
        public async Task<ActionResult> UpdateCandidate(long id, [FromBody] CandidateUpdateDto patch)
        {
            var ReadCandidate = _context.Candidates.Find(id);

            if (ReadCandidate == null)
            {
                return NotFound();
            }

            var CandidateUpdateDto = _mapper.Map<CandidateUpdateDto>(ReadCandidate);

            CandidateUpdateDto = patch;

            ReadCandidate.FirstName = CandidateUpdateDto.FirstName;
            ReadCandidate.LastName = CandidateUpdateDto.LastName;
            ReadCandidate.EmailAddress = CandidateUpdateDto.EmailAddress;
            ReadCandidate.Mobile = CandidateUpdateDto.Mobile;
            ReadCandidate.ResumeUrl = CandidateUpdateDto.ResumeUrl;
            ReadCandidate.DegreeId = CandidateUpdateDto.DegreeId;
           
            await _context.SaveChangesAsync();

            return Ok(CandidateUpdateDto);
        }

        // Delete
        [HttpDelete]
        [Route("DeleteCandidate/{id}")]
        public bool DeleteCandidate(long id)
        {
            bool a = false;
            var Candidate = _context.Candidates.Find(id);
            if (Candidate != null)
            {
                a = true;
                _context.Entry(Candidate).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            else
            {
                a = false;
            }
            return a;
        }

        // Read (Download Pdf File)
        [HttpGet]
        [Route("download/{url}")]
        public IActionResult DownloadPdfFile(string url)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "pdfs", url);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File Not Found");
            }

            var pdfBytes = System.IO.File.ReadAllBytes(filePath);
            var file = File(pdfBytes, "application/pdf", url);
            return file;
        }
    }
}
