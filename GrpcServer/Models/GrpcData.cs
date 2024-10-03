namespace GrpcServer.Models
{
    public class GrpcData
    {
        public int Id { get; set; }
        public int PacketSeqNum { get; set; }
        public int RecordSeqNum { get; set; }
        public string? PacketTimestamp { get; set; }
        public double Decimal1 { get; set; }
        public double Decimal2 { get; set; }
        public double Decimal3 { get; set; }
        public double Decimal4 { get; set; }
        public string? RecordTimestamp { get; set; }
    }
}
