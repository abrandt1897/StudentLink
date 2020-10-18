using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebApplication_mc_02.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.Owin;
using System.Net.WebSockets;
using System.Threading;
using System.Text.RegularExpressions;
using WebApplication_mc_02.Controllers;

namespace WebApplication_mc_02
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time
            });
            
            //Provide a secret key to Encrypt and Decrypt the Token
            var SecretKey = Encoding.ASCII.GetBytes("YuurKey-2374-OFFKDI94LMAO:56753253-yeet-6969-0420-kfirox29zoxv");

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //Same Secret key will be used while creating the token
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                    ValidateIssuer = true,
                    //Usually, this is your application base URL
                    ValidIssuer = "http://localhost:5001/",
                    ValidateAudience = true,
                    //Here, we are creating and using JWT within the same application.
                    //In this case, base URL is fine.
                    //If the JWT is created using a web service, then this would be the consumer URL.
                    ValidAudience = "http://localhost:5001/",
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddMvc();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();//////////////////////////////////////redirection 

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromMinutes(60),
                ReceiveBufferSize = 4 * 1024
            };

            app.UseWebSockets(webSocketOptions);

            app.UseSession();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                if (Regex.Matches(context.Request.Path, @"\/ws\/\d+").Count > 0)
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await Echo(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private async Task Echo(HttpContext context, WebSocket webSocket)
        {
            int PATH_FORWARDSLASH_OFFSET = 1;
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                int number = context.Request.Path.Value.LastIndexOf('/') + PATH_FORWARDSLASH_OFFSET;
                string value = context.Request.Path.Value.Substring(number);
                int numbererer = Convert.ToInt32(value);
                List<dynamic> notifications = await ChatsController.GetNotifications(numbererer);
                byte[] bytes2send = Encoding.UTF8.GetBytes(notifications[0].Data);
                await webSocket.SendAsync(new ArraySegment<byte>(bytes2send), WebSocketMessageType.Text, true, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(bytes2send), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
