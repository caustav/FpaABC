using Application.Command;
using Application.Common;
using Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FpaABC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        IMediator Mediator { get; }
        public InvoiceController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpGet("home"), ]
        public ContentResult GetHome() => base.Content("<h1>FpaABC is running</h1>", "text/html");

        [HttpGet]
        public async Task<IEnumerable<InvoiceDTO>> Get()
            => await Mediator.Send(new AllInvoicesQuery());


        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<InvoiceDTO>> Get(string id)
            => await Mediator.Send(new InvoiceQuery { InvoiceNumber = id});
        

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] CreateInvoiceCmd createInvoiceCmd)
            => await Mediator.Send(createInvoiceCmd);

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Put(string id)
            => await Mediator.Send(new ApproveInvoiceCmd { InvoiceNumber = id } );

        [HttpPut("complete/{id}")]
        public async Task<ActionResult<string>> PutCompleteInvoice(string id)
            => await Mediator.Send(new CompleteInvoiceCmd { InvoiceNumber = id });

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
