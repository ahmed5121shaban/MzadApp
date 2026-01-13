using MzadService.Application.Contracts;
using MzadService.Application.DTOs.Hourse;

namespace MzadService.Application.Contracts.Hourse
{
    public interface IHourseService : IBaseService<HourseDto, UpdateHourseDto>
    {
    }
}
