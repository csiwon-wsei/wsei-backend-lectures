﻿using System.Runtime.Serialization;

namespace soap_app.Models;
[DataContract(Namespace = "http://wsei.edu.pl/")]
public class AppUser
{
    [DataMember] public string Name { get; set; }

    [DataMember] public string Email { get; set; }
}