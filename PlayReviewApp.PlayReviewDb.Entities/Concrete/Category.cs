﻿using PlayReviewApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Entities.Concrete
{
      public class Category : IEntity
      {
            public int CategoryId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }
      }
}
