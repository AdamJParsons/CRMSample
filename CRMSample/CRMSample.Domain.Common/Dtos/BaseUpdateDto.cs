namespace CRMSample.Domain.Common.Dtos
{
    public record BaseUpdateDto
    {
        public long Id { get; init; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}
