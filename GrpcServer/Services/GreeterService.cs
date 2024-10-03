using Grpc.Core;
using GrpcServer;
using GrpcServer.DataContext;
using GrpcServer.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Services
{
    public class GreeterService : GrpcServer.GreeterService.GreeterServiceBase
    {
        private readonly GrpcServer.DataContext.DatabaseContext _dbContext;

        public GreeterService(GrpcServer.DataContext.DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Google.Protobuf.WellKnownTypes.Empty> SendData(Packet request, ServerCallContext context)
        {
            foreach (var record in request.PacketData)
            {
                var dataEntry = new GrpcData
                {
                    PacketSeqNum = request.PacketSeqNum,
                    RecordSeqNum = await _dbContext.grpc_data.CountAsync() + 1, // ”никальный номер записи
                    PacketTimestamp = request.PacketTimestamp,
                    Decimal1 = record.Decimal1,
                    Decimal2 = record.Decimal2,
                    Decimal3 = record.Decimal3,
                    Decimal4 = record.Decimal4,
                    RecordTimestamp = record.Timestamp
                };
                _dbContext.grpc_data.Add(dataEntry);
            }
            await _dbContext.SaveChangesAsync();
            return new Google.Protobuf.WellKnownTypes.Empty();
        }
    }
}
