using System;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Rooms;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class RoomService:IRoomService
	{
        private readonly IRoomRepository _roomRepo;
        private readonly IMapper _mapper;

		public RoomService(IRoomRepository roomRepo,
                          IMapper mapper)
		{
            _roomRepo = roomRepo;
            _mapper = mapper;
		}

        public async Task CreateAsync(RoomCreateDto model)
        {
            if (model is null) throw new ArgumentNullException();

            await _roomRepo.CreateAsync(_mapper.Map<Room>(model));
        }

        public async Task DeleteAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existRoom = await _roomRepo.GetById((int)id);

            if (existRoom is null) throw new NullReferenceException();

             await _roomRepo.DeleteAsync(existRoom);
        }

        public async Task<RoomDetailDto> DetailAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var data = await _roomRepo.FindBy(m => m.Id == id, source => source.Include(m => m.Groups)).FirstOrDefaultAsync();

            return _mapper.Map<RoomDetailDto>(data);
        }

        public async Task EditAysnc(RoomEditDto model, int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existRoom = await _roomRepo.GetById((int)id);

            if (existRoom is null) throw new NullReferenceException();

            await _roomRepo.EditAsync(_mapper.Map(model, existRoom));
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<RoomDto>>(await _roomRepo.GetAllAsync());

        }

        public async  Task<RoomDto> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existRoom = await _roomRepo.GetById((int)id);

            if (existRoom is null) throw new NullReferenceException();

            return _mapper.Map<RoomDto>(existRoom);
        }

        public async Task<IEnumerable<RoomSearchByNameDto>> SearchByNameAsync(string text)
        {
            if (text is null)
            {
                return _mapper.Map<IEnumerable<RoomSearchByNameDto>>(await _roomRepo.GetAllAsync());
            }

            var result = await _roomRepo.FindBy(m => m.Name.Contains(text)).ToListAsync();

            return result.Count == 0 ? throw new NullReferenceException("Data not Found") : _mapper.Map<IEnumerable<RoomSearchByNameDto>>(result);
        }

        public async Task<IEnumerable<RoomDto>> SortByAsync(string text, bool? IsDescending)
        {
            var result = await _roomRepo.SortBy(text, (bool)IsDescending);

            return _mapper.Map<IEnumerable<RoomDto>>(result);
        }
    }
}

