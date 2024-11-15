﻿using PlayReviewApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PlayReviewApp.PlayReviewDB.Entities.Concrete
{
      public class User : IEntity
      {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string FullName { get; set; }
            public DateTime CreatedAt { get; set; }
      }
}
