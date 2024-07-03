using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Students;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseAppApi.Controllers.Admin
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService,
                                 ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _studentService.GetAllAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]StudentCreateDto  request)
        {
            try
            {
                await _studentService.CreateAsync(request);
                return CreatedAtAction(nameof(Create), new { response = "Data succesfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _studentService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _studentService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var result = await _studentService.DetailAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id,[FromBody] StudentEditDto request)
        {
            await _studentService.EditAsync(id,request);

            return Ok();
        }



        [HttpGet]
        public async Task<IActionResult> SearchByNameOrSurname([FromQuery] string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return Ok(await _studentService.GetAllAsync());
            }

            var result = await _studentService.SearchByNameOrSurnameAsync(searchText);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGroup([FromQuery] int groupId, [FromQuery] int studentId)
        {
            await _studentService.DeleteGroupAsync(studentId, groupId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddtoGroup([FromQuery] int groupId, [FromQuery] int studentId)
        {
            await _studentService.AddToGroupAsync(studentId, groupId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Filter([FromQuery] string surname,string name, int? age)
        {
            
            return Ok(await _studentService.FilterAsync(name, surname, age));
        }

    }
}

