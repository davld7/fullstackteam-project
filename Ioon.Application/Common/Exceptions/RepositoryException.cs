using Ioon.Application.Common.DTO;

namespace Ioon.Application.Common.Exceptions
{
    [Serializable]
    public sealed class RepositoryException : Exception
    {
        public Dictionary<string, string[]> Errors { get; set; }
        public CodeException _objError { get; set; }

        public RepositoryException() : base()
        {

        }

        public RepositoryException(Dictionary<string, string[]> errors) : base()
        {
            Errors = errors;
        }

        public RepositoryException(string message) : base(message)
        {

        }

        public RepositoryException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public RepositoryException(CodeException objError)
        {
            _objError = objError;
        }


    }



}
