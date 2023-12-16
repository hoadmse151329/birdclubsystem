
using BAL.Services.Implements;
using BAL.Services.Interfaces;

namespace WebAPI.Middlewares
{
    public class JWTMiddleWare
    {
        private readonly RequestDelegate _next;
        private IJWTService jWTService;
        public JWTMiddleWare(RequestDelegate next)
        {
            _next = next;
            jWTService = new JWTService();
        }
        /*public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                AttachUserToContext(context, token);
            }
            await _next(context);
        }
        private void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var objectToken = jWTService.ExtractToken(token);
                // Lưu trữ thông tin xác thực trong HttpContext để sử dụng trong middleware tiếp theo hoặc trong controllers
                context.Items["Username"] = objectToken.Username;
                context.Items["Role"] = objectToken.Role;
                context.Items["UserId"] = objectToken.UserId;
            }
            catch
            {
                // Xử lý khi xác thực JWT token không hợp lệ
                List<ErrorDetail> errors = new List<ErrorDetail>();
                ErrorDetail error = new ErrorDetail()
                {
                    FieldNameError = "Exception",
                    DescriptionError = new List<string>() { "JWT Token is invalid." }
                };
                errors.Add(error);
                var message = JsonConvert.SerializeObject(errors);
                throw new ForBiddenException(message.ToString());
            }
        }*/
    }
}
