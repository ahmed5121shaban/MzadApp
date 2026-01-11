using Mapster;
using MzadService.Contracts;
using MzadService.Contracts.Mzad;
using MzadService.Data.DTOs.Mzad;
using MzadService.Entities;

namespace MzadService.Services
{
    public class MzadService : IMzadService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MzadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MzadDto> Create(MzadDto mzadDto)
        {
            var result = mzadDto.Adapt<Mzad>();
            await _unitOfWork.SaveAsync();

            return result.Adapt<MzadDto>();
        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.Mzads.Delete(id);
        }

        public async Task<IEnumerable<MzadDto>> GetAll()
        => (await _unitOfWork.Mzads.GetAll()).Adapt<IEnumerable<MzadDto>>();
        

        public async Task<MzadDto> GetById(Guid id)
        => (await _unitOfWork.Mzads.GetById(id)).Adapt<MzadDto>();
        

        public async Task<MzadDto> Update(MzadDto mzadDto)
        {
            var mappedData = mzadDto.Adapt<Mzad>();
            await _unitOfWork.Mzads.Update(mappedData);

            return mzadDto;
        }
    }
}
