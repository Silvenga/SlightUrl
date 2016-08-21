namespace SlightUrl.Components.Validations
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class SlightValidationException : Exception
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public SlightValidationException(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        protected SlightValidationException(
            SerializationInfo info,
            StreamingContext context, string propertyName, string errorMessage) : base(info, context)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }
    }
}