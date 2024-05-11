using BAL.Services.Implements;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        private readonly IMemberService _memberService;
        private readonly IConfiguration _connfig;

        public TransactionController(ITransactionService transactionService, IUserService userService, IMemberService memberService, IConfiguration connfig)
        {
            _transactionService = transactionService;
            _userService = userService;
            _memberService = memberService;
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
                if (ex.InnerException != null)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = ex.Message,
                        InnerExceptionMessage = ex.InnerException.Message
                    });
                }
                // Log the exception if needed
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }

		[HttpPost("Create")]
		[Authorize(Roles = "Member,TempMember")]
		[ProducesResponseType(typeof(OkObjectResult), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateTransaction(
            [Required][FromBody] TransactionViewModel newTransaction
            )
		{
			try
			{
				_transactionService.Create(newTransaction);

				var result = await _transactionService.GetTransactionByVnPayId(newTransaction.VnPayId);
				if (result == null)
				{
					return NotFound(new
					{
						Status = false,
						ErrorMessage = "Transaction Create Failed!"
					});
				}
				return Ok(new
				{
					Status = true,
					Message = "Transaction Create successfully !",
					Data = result
				});
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					return BadRequest(new
					{
						Status = false,
						ErrorMessage = ex.Message,
						InnerExceptionMessage = ex.InnerException.Message
					});
				}
				// Log the exception if needed
				return BadRequest(new
				{
					Status = false,
					ErrorMessage = ex.Message
				});
			}
		}

		/*[HttpGet("AllTransactions/{id}")]
		[Authorize(Roles = "Member")]
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
                    Data = result
                });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = ex.Message,
                        InnerExceptionMessage = ex.InnerException.Message
                    });
                }
                // Log the exception if needed
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        }*/
        [HttpPost("AllTransactions")]
        [Authorize(Roles = "Member")]
        [ProducesResponseType(typeof(List<TransactionViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllTransactionsByMemberId([FromBody] string memberId)
        {
            try
            {
                var mem = await _memberService.GetBoolById(memberId);
                if (!mem) return NotFound(new
                {
                    Status = false,
                    ErrorMessage = "Member Not Found!"

                });
                var result = await _transactionService.GetAllTransactionsByMemberId(memberId);
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
                    Data = result
                });
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return BadRequest(new
                    {
                        Status = false,
                        ErrorMessage = ex.Message,
                        InnerExceptionMessage = ex.InnerException.Message
                    });
                }
                // Log the exception if needed
                return BadRequest(new
                {
                    Status = false,
                    ErrorMessage = ex.Message
                });
            }
        } 
        [Authorize(Roles = "Manager")]
		[HttpPut("Update/{id}")]
		[ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Update(
			[Required][FromRoute] int id,
			[Required][FromBody] TransactionViewModel tran)
		{
			try
			{
				var result = _transactionService.GetTransactionById(id).Result;
				if (result == null)
				{
					return NotFound(new
					{
						Status = false,
						ErrorMessage = "Transaction does not exist!"
					});
				}
				tran.TransactionId = id;
				_transactionService.Update(tran);
				result = await _transactionService.GetTransactionById(tran.TransactionId.Value);
				return Ok(new
				{
					Status = true,
					Data = result
				});
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					return BadRequest(new
					{
						Status = false,
						ErrorMessage = ex.Message,
						InnerExceptionMessage = ex.InnerException.Message
					});
				}
				// Log the exception if needed
				return BadRequest(new
				{
					Status = false,
					ErrorMessage = ex.Message
				});
			}
		}
		[Authorize(Roles = "Member,TempMember")]
		[HttpPut("UpdateUser")]
		[ProducesResponseType(typeof(TransactionViewModel), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateRegisterTransaction(
			[Required][FromBody] UpdateTransactionRequest tran)
		{
			try
			{
				var result = await _transactionService.GetTransactionById(tran.TransactionId.Value);
				if (result == null)
				{
					return NotFound(new
					{
						Status = false,
						ErrorMessage = "Transaction does not exist!"
					});
				}
				var usr = await _userService.GetByMemberId(tran.MemberId);
				if (usr == null)
				{
					return NotFound(new
					{
						Status = false,
						ErrorMessage = "User does not exist!"
					});
				}
				bool isSuccess = await _transactionService.UpdateUserId(result.TransactionId.Value, usr.UserId.Value);

				if (isSuccess)
				{
                    result = await _transactionService.GetTransactionById(tran.TransactionId.Value);
                    return Ok(new
					{
						Status = true,
						Data = result
					});
				}
				return StatusCode(StatusCodes.Status500InternalServerError,new
				{
					Status = false
				});
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					return BadRequest(new
					{
						Status = false,
						ErrorMessage = ex.Message,
						InnerExceptionMessage = ex.InnerException.Message
					});
				}
				// Log the exception if needed
				return BadRequest(new
				{
					Status = false,
					ErrorMessage = ex.Message
				});
			}
		}
	}
}
