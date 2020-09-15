using SRTPluginBase;
using System;

namespace SRTPluginUIRECVXWPF
{
    public class PluginInfo : IPluginInfo
    {
        public string Name => "WPF UI (Resident Evil: Code: Veronica)";

        public string Description => "A WPF-based User Interface for displaying Resident Evil: Code: Veronica game memory values.";

        public string Author => "Kapdap";

        public Uri MoreInfoURL => new Uri("https://github.com/kapdap/re-cvx-srt-ui-wpf");

        public int VersionMajor => assemblyFileVersion.ProductMajorPart;

        public int VersionMinor => assemblyFileVersion.ProductMinorPart;

        public int VersionBuild => assemblyFileVersion.ProductBuildPart;

        public int VersionRevision => assemblyFileVersion.ProductPrivatePart;

        private System.Diagnostics.FileVersionInfo assemblyFileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
    }
}
