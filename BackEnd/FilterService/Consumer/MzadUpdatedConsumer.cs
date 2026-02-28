using Contracts.Mzads;
using FilterService.Entities;
using Mapster;
using MassTransit;
using MongoDB.Entities;

namespace FilterService.Consumer
{
    public class MzadUpdatedConsumer : IConsumer<UpdatedMzad>
    {
        public async Task Consume(ConsumeContext<UpdatedMzad> context)
        {
            try
            {
                var mzad = context.Message.Adapt<Mzad>();
                var result = await DB.Update<Mzad>()
                    .Match(m => m.ID == mzad.ID)
                    .ModifyOnly(m => new
                    {
                        m.Seller,
                        m.Winner,
                        m.SoldAmount,
                        m.CurrentHighTender,
                        m.UpdatedAt,
                        m.MzadEnd,
                        m.Status,
                        m.ReservePrice
                    }, mzad).ExecuteAsync();
                if (!result.IsAcknowledged) throw new Exception("Failed to update mzad in the database.");
                Console.WriteLine($"Mzad with ID {mzad.ID} updated successfully.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error updating mzad: {ex.Message}");
            }
        }
    }
}
