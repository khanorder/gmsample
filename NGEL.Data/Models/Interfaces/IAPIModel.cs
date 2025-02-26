using System;
using System.Text;
using System.Collections.Generic;
using Lobby;

namespace NGEL.Data.Models
{
    public interface IAPIResponse
    {
        abstract public bool result { get; set; }
        abstract public Errors error { get; set; }
        abstract public SignInUser? user { get; set; }
    }
}
