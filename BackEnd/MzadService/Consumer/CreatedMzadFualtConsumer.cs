using Contracts.Mzads;
using MassTransit;

namespace MzadService.Consumer
{
    public class CreatedMzadFualtConsumer : IConsumer<Fault<CreatedMzad>>
    {
        public async Task Consume(ConsumeContext<Fault<CreatedMzad>> context)
        {
            Console.WriteLine($"Fault occurred while processing CreatedMzad message:{context.Message.Message}");

            var mzad = new CreatedMzad
            {
                ReservePrice = 100,
                Seller = "Ahmed Shaban",
                Winner = "Ahmed Shaban",
                SoldAmount = 100,
                CurrentHighTender = 100,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                MzadEnd = DateTime.Now.AddDays(30),
                Status = Status.ReserveNotMet,
            };
            await context.Publish<CreatedMzad>(mzad);
        }
    }
}
