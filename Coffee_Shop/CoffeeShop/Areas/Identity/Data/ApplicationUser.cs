﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CoffeeShop.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    public string? FullName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(50)")]
    public string? Address { get; set; }

    [PersonalData]
    [Column(TypeName = "varchar(10)")]
    public string? PhoneNumber { get; set; }

    [PersonalData]
    [Column(TypeName = "varchar(50)")]
    public string? Email { get; set; }
}

