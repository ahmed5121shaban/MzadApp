using Contracts.Mzads;
using FilterService.Entities;
using MassTransit;
using MongoDB.Entities;

namespace FilterService.Consumer
{
    public class MzadDeletedConsumer : IConsumer<DeletedMzad>
    {
        public async Task Consume(ConsumeContext<DeletedMzad> context)
        {
           
            try 
            {
                var result = await DB.DeleteAsync<Mzad>(context.Message.Id);
                if(!result.IsAcknowledged) throw new Exception("Delete operation was not acknowledged by the database.");
                Console.WriteLine($"Mzad with id {context.Message.Id} has been deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fail to Delete Mzad with Id : {context.Message.Id} \nerror message: {ex.Message}");
            }
            
        }
    }
}
