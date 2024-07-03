using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Groups;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseAppApi.Controllers.Admin
{
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IGroupService groupService,
                              ILogger<GroupController> logger)
        {
            _groupService = groupService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]GroupCreateDto request)
        {
            try
            {
                await _groupService.CreateAsync(request);
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
            try
            {
                return Ok(await _groupService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBydId([FromRoute] int id)
        {
            var result = await _groupService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            await _groupService.DeleteAysnc(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var result =await _groupService.DetailAsync(id);

            return Ok(result); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody]GroupEditDto request,[FromRoute]int id)
        {
            await _groupService.EditAysnc(request,id);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return Ok(await _groupService.GetAllAsync());
            }

            var result =await _groupService.SearchAsync(searchText);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SortBy([FromQuery]string text,bool? isDescending)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Ok(await _groupService.GetAllAsync());
            }

            var result = await _groupService.SortByAsync(text, isDescending);

            return Ok(result);


        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromQuery] int groupId, [FromQuery] int teacherId)
        {
            await _groupService.AddToTeacherAsync(teacherId, groupId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTeacher([FromQuery] int groupId, [FromQuery] int teacherId)
        {
            await _groupService.DeleteTeacherAsync(teacherId, groupId);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginateDatas([FromQuery] int page=1 , [FromQuery] int take=2)
        {
            return Ok(await _groupService.GetPaginateDatasAsync(page, take));
        }
    }
}

