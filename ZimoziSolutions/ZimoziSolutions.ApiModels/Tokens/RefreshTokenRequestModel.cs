﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimoziSolutions.ApiModels.Tokens
{
    public class RefreshTokenRequestModel
    {
        public Guid UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
