﻿using SRTPluginBase;
using SRTPluginProviderRECVX;
using System.Windows.Threading;

namespace SRTPluginUIRECVXWPF
{
    public class PluginUI : IPluginUI
    {
        public string RequiredProvider => "SRTPluginProviderRECVX";

        internal static PluginInfo _info = new PluginInfo();
        public IPluginInfo Info => _info;

        public IPluginHostDelegates HostDelegates { get; private set; }

        public int Startup(IPluginHostDelegates hostDelegates)
        {
            HostDelegates = hostDelegates;
            Plugin.Initialize(this);
            return 0;
        }

        public int Shutdown()
        {
            Plugin.Exit();
            Dispatcher.CurrentDispatcher.InvokeShutdown();
            return 0;
        }

        public int ReceiveData(object gameMemory)
        {
            Plugin.Models.AppView.Initalize((GameMemoryRECVX)gameMemory);
            return 0;
        }
    }
}