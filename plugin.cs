using System;
using System.Collections.Generic;
using Exiled.API.Features;
using hats.Components;
using UnityEngine;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

namespace hats
{
    public class Plugin : Plugin<Config>
    {
        public override string Author { get; } = "Rowpann SCP";
        public override string Name { get; } = "hats";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 0);

        public static Plugin Singleton;
        public EventHandler Handler { get; private set; }
        public Dictionary<string, HatComponent> hats;

        public override void OnEnabled()
        {
            hats = new Dictionary<string, HatComponent>();
            Singleton = this;
            Handler = new EventHandler(Config);

            Server.WaitingForPlayers += Handler.WaitingForPlayers;
            Server.EndingRound += Handler.EndingRound;
            Player.Left += Handler.OnLeave;
            
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Server.WaitingForPlayers -= Handler.WaitingForPlayers;
            Server.EndingRound -= Handler.EndingRound;
            Player.Left -= Handler.OnLeave;
            
            Singleton = null;
            Handler = null;
            hats = null;
            base.OnDisabled();
        }
    }
};
