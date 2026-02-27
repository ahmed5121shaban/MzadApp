using Contracts.Mzads;
using FilterService.Entities;
using Mapster;
using MassTransit;
using MongoDB.Entities;

namespace FilterService.Consumer
{
    public class MzadCreatedConsumer : IConsumer<CreatedMzad>
    {
        public async Task Consume(ConsumeContext<CreatedMzad> context)
        {
            var message = context.Message;
            Mzad mzad = message.Adapt<Mzad>();

            if (mzad is null) throw new Exception("The Mzad Not Found");
            await mzad.SaveAsync();
        }
    }
}
