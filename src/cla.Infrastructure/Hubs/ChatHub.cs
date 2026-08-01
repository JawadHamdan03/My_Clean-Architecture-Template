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
      
        // save in DB
        ChatMessage msg = new ChatMessage {Message=message, UserName=name };
        await dbContext.AddAsync(msg);
        await dbContext.SaveChangesAsync();
        


        await Clients.All.SendAsync("newmessage",name , message);
        
    }

}
