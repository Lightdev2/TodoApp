using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoApp.BusinessLogic.Bus;
using TodoApp.Core.Infrastructure;
using EvtBus = TodoApp.BusinessLogic.Bus.EvtBus;
using IObserver = TodoApp.BusinessLogic.Bus.IObserver;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("/api")]
    public class NotificationsController : ControllerBase
    {
        private readonly EvtBus _evtBus;
        public NotificationsController(EvtBus eveBus)
        {
            _evtBus = eveBus;
        }

        [HttpGet("/ws")]
        public async Task Get()
        {
            var req = Request.Cookies;
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using WebSocket webSocket = await
                    HttpContext.WebSockets.AcceptWebSocketAsync();

                var buffer = new byte[1024 * 4];
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var str = System.Text.Encoding.ASCII.GetString(new ArraySegment<byte>(buffer, 0, buffer.Length));
                var token = str.Replace("\0", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidIssuer = AuthOptions.Issuer,
                        ValidAudience = AuthOptions.Audience,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    }, out SecurityToken newToken);
                    var claims = (JwtSecurityToken)newToken;
                    string email = claims.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                    var observer = new Sender(webSocket, email);
                    _evtBus.RegisterObserver(observer);
                }
                catch
                {
                    return;
                }
                while (!result.CloseStatus.HasValue)
                {
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }

    public class Sender : IObserver
    {
        public WebSocket webSocket;
        public string _email;

        public Sender(WebSocket ws, string email)
        {
            webSocket = ws;
            _email = email;
        }
        public async void Update(Message msg)
        {
            if (msg.email !=  _email)
            {
                return;
            }

            var buffer = Encoding.ASCII.GetBytes(msg.email + msg.msg);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length), WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
