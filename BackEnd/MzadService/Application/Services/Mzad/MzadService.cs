using Contracts.Mzads;
using Mapster;
using MassTransit;
using MzadService.Application.Contracts;
using MzadService.Application.Contracts.Mzad;
using MzadService.Application.DTOs.Mzad;
using MzadService.Entities;

namespace MzadService.Application.Services.Mzad
{
    public class MzadService : IMzadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublishEndpoint _publishEndPoint;
        public MzadService(IUnitOfWork unitOfWork, IPublishEndpoint publishEndPoint)
        {
            _unitOfWork = unitOfWork;
            _publishEndPoint = publishEndPoint;
        }
        public async Task<MzadDto> Create(MzadDto mzadDto)
        {
            var result = mzadDto.Adapt<Entities.Mzad>();
            await _unitOfWork.SaveAsync();
            var mzad = result.Adapt<MzadDto>();
            await _publishEndPoint.Publish(mzad.Adapt<CreatedMzad>());
            return mzad;
        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.Mzads.Delete(id);
        }

        public async Task<IEnumerable<MzadDto>> GetAll()
        => (await _unitOfWork.Mzads.GetAll()).Adapt<IEnumerable<MzadDto>>();

        public async Task<IEnumerable<MzadDto>> GetAllWithLastUpdatedDate(string lastUpdateDate)
        {
            var query = (await _unitOfWork.Mzads.GetAllAsyncAsQueryable())
                .Where(m => m.UpdatedAt > DateTime.Parse(lastUpdateDate).ToUniversalTime());

            return query.Adapt<IEnumerable<MzadDto>>();
        }

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
