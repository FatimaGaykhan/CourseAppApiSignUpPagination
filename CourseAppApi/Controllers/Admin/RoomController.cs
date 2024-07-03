using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Rooms;
using Service.Services;
using Service.Services.Interfaces;

namespace CourseAppApi.Controllers.Admin
{
    public class RoomController : BaseController
    {
        private readonly IRoomService _roomService;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IRoomService roomService,
                              ILogger<RoomController> logger)
        {
            _roomService = roomService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomCreateDto request)
        {
            try
            {
                await _roomService.CreateAsync(request);
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
                return Ok(await _roomService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBydId([FromRoute] int id)
        {
            var result = await _roomService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            await _roomService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Detail([FromRoute] int id)
        {
            var result = await _roomService.DetailAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] RoomEditDto request)
        {
            await _roomService.EditAysnc(request, id);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SearchByName([FromQuery] string searchName)
        {
            return Ok(await _roomService.SearchByNameAsync(searchName));

        }

        [HttpGet]
        public async Task<IActionResult> SortBy([FromQuery] string text, bool? isDescending)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Ok(await _roomService.GetAllAsync());
            }

            var result = await _roomService.SortByAsync(text, isDescending);

            return Ok(result);


        }
    }
}

