using cla.Domain.Entities;
using cla.Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace cla.Infrastructure.Hubs;

public class ChatHub(AppDbContext dbContext) : Hub
{

    public async Task sendMessage(string name , string message)
    {
        ChatMessage msg = new ChatMessage {Message=message, UserName=name };
        dbContext.Add(msg);
        dbContext.SaveChanges();
        // save in DB
        await Clients.All.SendAsync("newmessage",name , message);
    }
}
