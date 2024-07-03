using System;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.DTOs.Admin.Educations;
using Service.DTOs.Admin.Groups;
using Service.DTOs.Admin.Students;
using Service.Services.Interfaces;

namespace Service.Services
{
	public class EducationService:IEducationService
	{
        private readonly IEducationRepository _educationRepo;
        private readonly IMapper _mapper;

        public EducationService(IEducationRepository educationRepo,
                            IMapper mapper)
        {
            _educationRepo = educationRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(EducationCreateDto model)
        {
            if (model is null) throw new ArgumentNullException();

            await _educationRepo.CreateAsync(_mapper.Map<Education>(model));
        }

        public async Task DeleteAysnc(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existEducation = await _educationRepo.GetById((int)id);

            if (existEducation is null) throw new NullReferenceException();

             await _educationRepo.DeleteAsync(existEducation);
        }

        public async Task<EducationDetailDto> DetailAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var data = await _educationRepo.FindBy(m => m.Id == id, source => source.Include(m => m.Groups)).FirstOrDefaultAsync();

            return _mapper.Map<EducationDetailDto>(data);
        }

        public async Task EditAysnc(EducationEditDto model, int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existEducation = await _educationRepo.GetById((int)id);

            if (existEducation is null) throw new NullReferenceException();

            await _educationRepo.EditAsync(_mapper.Map(model, existEducation));
        }

        public async Task<IEnumerable<EducationDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<EducationDto>>(await _educationRepo.GetAllAsync());
        }

        public async Task<EducationDto> GetByIdAsync(int? id)
        {
            if (id is null) throw new ArgumentNullException();

            var existEducation = await _educationRepo.GetById((int)id);

            if (existEducation is null) throw new NullReferenceException();

            return _mapper.Map<EducationDto>(existEducation);
        }

        public async Task<IEnumerable<EducationSearchByNameDto>> SearchByNameAsync(string text)
        {
            if(text is null)
            {
                return _mapper.Map<IEnumerable<EducationSearchByNameDto>>(await _educationRepo.GetAllAsync());
            }

            var result = await _educationRepo.FindBy(m => m.Name.Contains(text)).ToListAsync();

            return result.Count == 0 ? throw new NullReferenceException("Data not Found") : _mapper.Map<IEnumerable<EducationSearchByNameDto>>(result);
        }

        public async Task<IEnumerable<EducationDto>> SortByAsync(string text, bool? IsDescending)
        {
            var result = await _educationRepo.SortBy(text, (bool)IsDescending);

            return _mapper.Map<IEnumerable<EducationDto>>(result);
        }
    }
}

