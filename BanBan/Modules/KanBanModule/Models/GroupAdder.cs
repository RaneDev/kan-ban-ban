﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanBanModule.Models
{
    public class GroupAdder :GroupBase, IGroup
    {
        public string Name { get; set; }

    }
}
