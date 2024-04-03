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
        private readonly IUserService _userService;
        private readonly IConfiguration _connfig;

        public TransactionController(ITransactionService transactionService, IUserService userService, IConfiguration connfig)
        {
            _transactionService = transactionService;
            _userService = userService;
            _connfig = connfig;
        }

        [HttpGet("{id:int}")]
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
                        Status = false,
                        ErrorMessage = "Transaction Not Found!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        [HttpGet("AllTransactions/{id}")]
        [ProducesResponseType(typeof(List<TransactionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllTransactionsByUserId([FromRoute] int id)
        {
            try
            {
                var usr = await _userService.GetBoolById(id);
                if (!usr) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "User Not Found!"
                    
                });
                var result = await _transactionService.GetAllTransactionsByUserId(id);
                if (result == null)
                {
                    return NotFound(new
                    {
                        Status = false,
                        ErrorMessage = "No Transactions Found!"
                    });
                }
                return Ok(new
                {
                    Status = true,
                    result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
