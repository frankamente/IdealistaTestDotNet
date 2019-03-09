﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdealistaTest.Infrastructure.Entities
{
    public class Picture
    {
        public int Id { get; }
        public string Url { get;}
        public string Quality { get;}

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ((Picture)obj).Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}