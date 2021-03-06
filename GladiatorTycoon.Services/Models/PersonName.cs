﻿using GladiatorTycoon.Enums;
using System.Collections.Generic;
using System.Text;

namespace GladiatorTycoon.Services.Models
{
    public class PersonName
    {
        public int Id { get; set; }

        public string Text { get; set; }
        public bool IsFirstName { get; set; }
        public Gender Gender { get; set; }
    }
}
