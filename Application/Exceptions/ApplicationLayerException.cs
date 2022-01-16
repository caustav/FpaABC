using System;

namespace Application
{
    public class ApplicationLayerException : Exception
    {
        public ApplicationLayerException(string comments) : base(comments)
        {

        }
    }
}