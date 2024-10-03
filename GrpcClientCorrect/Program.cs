using Grpc.Net.Client;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using System;
using GrpcServer;
using System.Threading.Tasks;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var clientConfig = configuration.GetSection("ClientConfig").Get<ClientConfig>();

var handler = new HttpClientHandler();

var httpClient = new HttpClient(handler);

var grpcAddress = clientConfig.GrpcServerAddr ?? "grpc_server";
var grpcPort = clientConfig.GrpcServerPort > 0 ? clientConfig.GrpcServerPort : 8080;

var channel = GrpcChannel.ForAddress($"http://{grpcAddress}:{grpcPort}", new GrpcChannelOptions
{
    HttpClient = httpClient
});

var client = new GreeterService.GreeterServiceClient(channel);
// Добавляем задержку перед отправкой первого запроса
Console.WriteLine("Ждем 10сек ----------------------------------------------");
await Task.Delay(10000);
Random random = new Random();

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
            Timestamp = DateTime.UtcNow.ToString(),
            RecordSeqNum = j
        });
    }

    await client.SendDataAsync(packet);//пока ошибка тут
    Console.WriteLine($"Packet {i} sent with {clientConfig.RecordsInPacket} records.");
    await Task.Delay(clientConfig.TimeInterval * 1000);
}


public class ClientConfig
{
    public int TotalPackets { get; set; }
    public int RecordsInPacket { get; set; }
    public int TimeInterval { get; set; }
    public string? GrpcServerAddr { get; set; }
    public int GrpcServerPort { get; set; }
}