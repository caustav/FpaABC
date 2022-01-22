using Application.Common;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Query
{
    public class InvoiceQuery : IRequest<InvoiceDTO>
    {
        public string InvoiceNumber { get; set; }  = default!;
    }

    public class AllInvoicesQuery : IRequest<IEnumerable<InvoiceDTO>>
    {
    }

    public class InvoiceQueryHandler : IRequestHandler<InvoiceQuery, InvoiceDTO>, 
                                        IRequestHandler<AllInvoicesQuery, IEnumerable<InvoiceDTO>>
    {
        public IRepository<InvoiceDTO> Repository { get; private set; }

        public InvoiceQueryHandler(IRepository<InvoiceDTO> repository)
        {
            Repository = repository;
        }

        public async Task<InvoiceDTO> Handle(InvoiceQuery request, CancellationToken cancellationToken)
        {
            return await Repository.Get(request.InvoiceNumber);
        }

        public Task<IEnumerable<InvoiceDTO>> Handle(AllInvoicesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Repository.GetAll());
        }
    }

}
