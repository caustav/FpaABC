using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class InvoiceIncompleteException : ApplicationLayerException
    {
        public InvoiceIncompleteException(string comments) : base(comments) { }

        public InvoiceIncompleteException() : base ("Unapproved invoice can not be completed.") { }
    }
}
