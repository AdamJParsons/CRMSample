using CRMSample.Domain.Common.Dtos;

namespace CRMSample.Domain.Admin.Dtos.User
{
    public record UpdateUserDto : BaseUpdateDto
    {
        public string EmailAddress { get; init; }
        public string UserName { get; init; }
        public string JobTitle { get; init; }

        #region Person

        public long? PersonTitleId { get; set; }
        public long? PersonGenderId { get; set; }
        public string PersonForename { get; set; }
        public string PersonOtherNames { get; set; }
        public string PersonSurname { get; set; }

        #endregion
    }
}
