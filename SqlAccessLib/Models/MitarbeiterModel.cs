﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlAccessLib.Models
{
    public class MitarbeiterModel
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string ChipId { get; set; }

    }
}
