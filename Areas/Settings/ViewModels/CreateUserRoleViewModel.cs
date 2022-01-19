﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NestLinkV2.Areas.Settings.ViewModels
{
    public class CreateUserRoleViewModel
    {
        [DisplayName("Role Name")]
        [Required]
        public string RoleName { get; set; }
    }
}
