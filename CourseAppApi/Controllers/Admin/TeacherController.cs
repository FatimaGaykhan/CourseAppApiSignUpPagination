using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Students;
using Service.DTOs.Admin.Teachers;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseAppApi.Controllers.Admin
{
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService,
                                ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto request)
        {
            try
            {
                await _teacherService.CreateAsync(request);
                return CreatedAtAction(nameof(Create), new { response = "Data succesfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _teacherService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _teacherService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            await _teacherService.DeleteAysnc(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var result = await _teacherService.DetailAsync(id);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] TeacherEditDto request)
        {
            await _teacherService.EditAsync(request, id);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SearchByNameOrSurname([FromQuery] string searchNameOrSurname)
        {
            return Ok(await _teacherService.SearchByNameOrSurnameAsync(searchNameOrSurname));

        }

        [HttpGet]
        public async Task<IActionResult> SortBy([FromQuery] string text, bool? isDescending)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Ok(await _teacherService.GetAllAsync());
            }

            var result = await _teacherService.SortByAsync(text, isDescending);

            return Ok(result);


        }

    
    }
}

