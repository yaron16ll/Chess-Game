﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class ServerContext : DbContext
    {
        public ServerContext (DbContextOptions<ServerContext> options)
            : base(options)
        {
        }

        public DbSet<Server.Models.PlayersTable> PlayersTable { get; set; } = default!;
        public DbSet<Server.Models.GamesTable> GamesTable { get; set; } = default!;

    }
}
