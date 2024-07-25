using CommerceBuilder.Plugins;
using System;

namespace TPIPlugin
{
    public class TPIPlugin : PluginBase
    {
        public TPIPlugin(PluginManifest manifest)
            : base(manifest)
        {
        }
        public override bool Install()
        {
            return true;
        }

        public override bool UnInstall()
        {
            return true;
        }

    }
}
