﻿using LojaVirtual.Libraries.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class NewsletterEmail
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_ERROR001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_ERROR004")]
        public string Email { get; set; }
    }
}
