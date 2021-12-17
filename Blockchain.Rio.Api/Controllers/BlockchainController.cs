using Blockchain.Rio.Domain.AggregateModel.BlockAggregate;
using Blockchain.Rio.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blockchain.Rio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlockchainController : ControllerBase
    {
        private readonly ILogger<BlockchainController> _logger;
        private readonly TransactionPool _transactionPool;

        public BlockchainController(ILogger<BlockchainController> logger, TransactionPool transactionPool)
        {
            _logger = logger;
            _transactionPool = transactionPool;
        }

        [HttpPost]
        public IActionResult Post(Transaction request)
        {
            _transactionPool.AddRaw(request);
            return Created("", request);
        }
    }
}
