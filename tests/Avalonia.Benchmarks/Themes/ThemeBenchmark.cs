﻿using System;

using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.PlatformSupport;
using Avalonia.Styling;
using Avalonia.Themes.Default;
using Avalonia.Themes.Fluent;
using Avalonia.UnitTests;

using BenchmarkDotNet.Attributes;

namespace Avalonia.Benchmarks.Themes
{
    [MemoryDiagnoser]
    public class ThemeBenchmark : IDisposable
    {
        private IDisposable _app;

        public ThemeBenchmark()
        {
            AssetLoader.RegisterResUriParsers();

            _app = UnitTestApplication.Start(TestServices.StyledWindow.With(theme: () => null));
            // Add empty style to override it later
            UnitTestApplication.Current.Styles.Add(new Style());
        }

        [Benchmark]
        public bool InitFluentTheme()
        {
            UnitTestApplication.Current.Styles[0] = new FluentTheme(new Uri("resm:Styles?assembly=Avalonia.Benchmarks"));
            return ((IResourceHost)UnitTestApplication.Current).TryGetResource(ElementTheme.Dark, "SystemAccentColor", out _);
        }

        [Benchmark]
        public bool InitDefaultTheme()
        {
            UnitTestApplication.Current.Styles[0] = new SimpleTheme(new Uri("resm:Styles?assembly=Avalonia.Benchmarks"));
            return ((IResourceHost)UnitTestApplication.Current).TryGetResource(ElementTheme.Dark, "ThemeAccentColor", out _);
        }

        public void Dispose()
        {
            _app.Dispose();
        }
    }
}
