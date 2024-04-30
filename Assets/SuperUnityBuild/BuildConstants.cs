using System;

// This file is auto-generated. Do not modify or move this file.

namespace SuperUnityBuild.Generated
{
    public enum ReleaseType
    {
        None,
        Dev,
        Release,
    }

    public enum Platform
    {
        None,
        WebGL,
        PC,
    }

    public enum ScriptingBackend
    {
        None,
        IL2CPP,
        Mono,
    }

    public enum Architecture
    {
        None,
        WebGL,
        Windows_x86,
    }

    public enum Distribution
    {
        None,
    }

    public static class BuildConstants
    {
        public static readonly DateTime buildDate = new DateTime(638500669955254354);
        public const string version = "1.0.3";
        public const ReleaseType releaseType = ReleaseType.Release;
        public const Platform platform = Platform.WebGL;
        public const ScriptingBackend scriptingBackend = ScriptingBackend.IL2CPP;
        public const Architecture architecture = Architecture.WebGL;
        public const Distribution distribution = Distribution.None;
    }
}

