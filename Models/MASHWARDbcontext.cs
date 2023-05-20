using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASHWAR.Models
{
    public class MASHWARDbcontext: DbContext
    {

        public MASHWARDbcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; }
    }
}
