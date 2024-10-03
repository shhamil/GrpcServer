using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using System;
using GrpcServer; // Проверьте, что это пространство имен правильно
using System.Threading.Tasks;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var clientConfig = configuration.GetSection("ClientConfig").Get<ClientConfig>();

var channel = GrpcChannel.ForAddress($"http://{clientConfig.GrpcServerAddr}:{clientConfig.GrpcServerPort}");
var client = new GreeterService.GreeterServiceClient(channel);

Random random = new Random();

// Отправляем пакеты
for (int i = 0; i < clientConfig.TotalPackets; i++)
{
    var packet = new Packet
    {
        PacketTimestamp = DateTime.UtcNow.ToString(),
        PacketSeqNum = i,
        NRecords = clientConfig.RecordsInPacket
    };

    for (int j = 0; j < clientConfig.RecordsInPacket; j++)
    {
        packet.PacketData.Add(new Data
        {
            Decimal1 = random.NextDouble(),
            Decimal2 = random.NextDouble(),
            Decimal3 = random.NextDouble(),
            Decimal4 = random.NextDouble(),
            Timestamp = DateTime.UtcNow.ToString()
        });
    }

    await client.SendDataAsync(packet);
    Console.WriteLine($"Packet {i} sent with {clientConfig.RecordsInPacket} records.");
    await Task.Delay(clientConfig.TimeInterval * 1000);
}

Console.WriteLine("Client finished sending packets.");

public class ClientConfig
{
    public int TotalPackets { get; set; }
    public int RecordsInPacket { get; set; }
    public int TimeInterval { get; set; }
    public string GrpcServerAddr { get; set; }
    public int GrpcServerPort { get; set; }
}