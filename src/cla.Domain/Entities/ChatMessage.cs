using System;
using System.Collections.Generic;
using System.Text;

namespace cla.Domain.Entities;

public class ChatMessage
{
    public int Id{ get; set; }
    public string Message { get; set; }
    public string UserName { get; set; }
}
