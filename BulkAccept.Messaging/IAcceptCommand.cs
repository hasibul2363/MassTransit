namespace BulkAccept.Messaging
{
    public interface ITitleCreateCommand
    {
        int RefNo { get; set; }
        string VinNo { get; set; }
    }
}