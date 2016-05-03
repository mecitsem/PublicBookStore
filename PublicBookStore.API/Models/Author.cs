﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicBookStore.API.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
    }
}