﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.DTOs
{
    public class NewUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Sex { get; set; }
    }
}
