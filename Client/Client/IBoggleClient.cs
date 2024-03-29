﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Interface implemented by BoggleClient.cs
/// </summary>
namespace PS8
{
    interface IBoggleClient
    {
        event Action<string, string> RegisterPressed;
        event Action RegisterCancelPressed;
        event Action JoinGameCancelPressed;
        event Action<int> JoinGamePressed;
        event Action HelpPressed;
        bool IsInActiveGame { get; set; }

        bool IsUserRegistered { get; set; }
        void SetControlState(bool state);
        void SetJoinGameControlState(bool state);
    }
}
