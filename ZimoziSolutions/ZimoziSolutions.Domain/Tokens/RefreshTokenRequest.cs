﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimoziSolutions.Domain.Tokens
{
    public class RefreshTokenRequest
    {
        public Guid UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
