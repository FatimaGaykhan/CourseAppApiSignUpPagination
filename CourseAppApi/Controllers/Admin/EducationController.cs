using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Students;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseAppApi.Controllers.Admin
{
    public class EducationController : BaseController
    {
        private readonly IEducationService _educationService;
        private readonly ILogger<EducationController> _logger;

        public EducationController(IEducationService educationService,
                                   ILogger<EducationController> logger)
        {
            _educationService = educationService;
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EducationCreateDto request)
        {


            await _educationService.CreateAsync(request);
            _logger.LogInformation("Education Create is working");
            return CreatedAtAction(nameof(Create), new { response = "Data succesfully created" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Education GetAll is working");
            return Ok(await _educationService.GetAllAsync());

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            await _educationService.DeleteAysnc(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBydId([FromRoute] int id)
        {
            _logger.LogInformation("Education GetById is working");
            var result = await _educationService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var result = await _educationService.DetailAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EducationEditDto request)
        {
            await _educationService.EditAysnc(request, id);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SearchByName([FromQuery]string searchName)
        {
            return Ok(await _educationService.SearchByNameAsync(searchName));

        }

        [HttpGet]
        public async Task<IActionResult> SortBy([FromQuery] string text, bool? isDescending)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Ok(await _educationService.GetAllAsync());
            }

            var result = await _educationService.SortByAsync(text, isDescending);

            return Ok(result);


        }
    }
}

