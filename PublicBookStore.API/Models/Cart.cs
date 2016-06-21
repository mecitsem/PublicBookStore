﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PublicBookStore.API.Models
{
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Book Book { get; set; }
    }
}
