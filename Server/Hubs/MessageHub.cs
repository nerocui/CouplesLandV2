using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Repositories;
using Server.Dtos;
using Server.Extensions;
using Server.Models;

namespace Server.Hubs
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMessageRepository _messageRepo;
        private readonly DataContext _context;
        private const string _groupName = "couplesland_chat_room";
        public MessageHub(UserManager<AppUser> userManager, IMessageRepository messageRepo, DataContext context)
        {
            _context = context;
            _messageRepo = messageRepo;
            _userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["recipient"].ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, _groupName);
            var sender = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == Context.User.GetUsername());
            var recipient = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == otherUser);
            if (recipient == null) throw new HubException("Recipient not found");
            var messages = await _messageRepo.GetMessages(sender, recipient);
            await Clients.Caller.SendAsync("MessageHistory", messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _groupName);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(MessageCreationDto message)
        {
            var username = Context.User.GetUsername();
            if (username == message.RecipientUserName.ToLower())
            {
                throw new HubException("You cannot send message to yourself");
            }
            var sender = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var recipient = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == message.RecipientUserName.ToLower());
            if (recipient == null) throw new HubException("Recipient not found");
            var newmessage = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUserName = sender.UserName,
                RecipientUserName = recipient.UserName,
                Content = message.Content,
                MessageType = message.MessageType
            };
            _messageRepo.AddMessage(newmessage);
            if (await _context.SaveChangesAsync() > 0)
            {
                await Clients.Group(_groupName).SendAsync("NewMessage", newmessage);
            }
        }
    }
}