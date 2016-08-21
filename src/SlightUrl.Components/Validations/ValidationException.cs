namespace SlightUrl.Components.Validations
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class ValidationException : Exception
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public ValidationException(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        protected ValidationException(
            SerializationInfo info,
            StreamingContext context, string propertyName, string errorMessage) : base(info, context)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}