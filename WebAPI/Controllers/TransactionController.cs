using BAL.Services.Interfaces;
using BAL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IConfiguration _connfig;

        public TransactionController(ITransactionService transactionService, IConfiguration connfig)
        {
            _transactionService = transactionService;
            _connfig = connfig;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionById([FromRoute] int id)
        {
            try
            {
                var result = await _transactionService.GetTransactionById(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        status = false,
                        errorMessage = "Transaction Not Found!"
                    });
                }
                return Ok(new
                {
                    status = true,
                    result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = false,
                    errorMessage = ex.Message
                });
            }
        }
    }
}
