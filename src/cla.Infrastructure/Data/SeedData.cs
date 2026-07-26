using cla.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace cla.Infrastructure.Data;


public static class SeedData
{
    public static async Task SeedUserData(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetService<AppDbContext>();
        if(!await dbContext.Users.AnyAsync())
        {
            User[] users=
            [
                 new User(){Name="ahmad",Password="123456",Role=Role.Admin},
                 new User(){Name="jawad",Password="123456",Role=Role.Customer},
                 new User(){Name="saed",Password="123456",Role=Role.Customer},
                 new User(){Name="mousa",Password="123456",Role=Role.Customer}
            ];
            await dbContext.Users.AddRangeAsync(users);
            await dbContext.SaveChangesAsync();
        }
        
       return ;
    }
}