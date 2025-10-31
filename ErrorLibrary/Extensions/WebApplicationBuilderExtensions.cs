using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ErrorLibrary.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthetication(this WebApplicationBuilder builder)
        {
            var secret = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Secret");
            var issuer = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Issuer");
            var audience = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Audience");

            var key = Encoding.ASCII.GetBytes(secret!);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<Entities.ApplicationUser>>();

                        var userId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
                        var tokenStamp = context.Principal?.FindFirstValue("security_stamp");

                        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(tokenStamp))
                        {
                            context.Fail("Token không chứa thông tin cần thiết.");
                            return;
                        }

                        var user = await userManager.FindByIdAsync(userId);
                        if (user == null || user.SecurityStamp != tokenStamp)
                        {
                            context.Fail("Token không hợp lệ do SecurityStamp thay đổi.");
                        }
                    }
                };
            });


            return builder;
        }
    }
}