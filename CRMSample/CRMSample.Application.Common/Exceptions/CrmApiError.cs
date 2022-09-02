namespace CRMSample.Application.Common.Exceptions
{
    public class CrmApiError
    {
        public CrmApiError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public CrmApiError(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

        public string ErrorMessage { get; }

        public string PropertyName { get; }
    }
}
