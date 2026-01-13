using Mapster;
using MzadService.Application.Contracts;
using MzadService.Application.Contracts.Mzad;
using MzadService.Application.DTOs.Mzad;
using MzadService.Entities;

namespace MzadService.Application.Services.Mzad
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
            var result = mzadDto.Adapt<Entities.Mzad>();
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
        

        public async Task<UpdateMzadDto> Update(UpdateMzadDto mzadDto)
        {
            var mzad = await _unitOfWork.Mzads. GetById(mzadDto.Id);

            if (mzad is null)
                throw new Exception("Mzad not found");

            mzadDto.Adapt(mzad);
            await _unitOfWork.Mzads.SaveChangesAsync();

            return mzadDto;
        }
    }
}
