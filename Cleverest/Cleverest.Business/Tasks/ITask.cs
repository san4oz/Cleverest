﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest.Business.Tasks
{
    public interface ITask
    {
        string Name { get; }

        void Run();      
    }
}
