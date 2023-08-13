using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ProjetVideoGameV2.POCO;

namespace ProjetVideoGameV2
{
    
    public partial class App : Application
    {
        public int UserIdWithNewCopy { get; set; } = -1;

        public List<Player> PlayerList = new List<Player>();  
    }
}
