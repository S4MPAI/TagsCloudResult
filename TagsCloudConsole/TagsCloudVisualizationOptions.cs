using CommandLine;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace TagsCloudConsole;

// ReSharper disable once ClassNeverInstantiated.Global
public class TagsCloudVisualizationOptions
{
    [Option('s', "inputFilePath", Required = true)]
    public string InputFilePath { get; set; }
    
    [Option('l', "outputFilePath", Required = true)]
    public string OutputFilePath { get; set; }
    
    [Option("fontFamily", Required = true)]
    public string FontFamily { get; set; }
    
    [Option('c', "colorName", Required = false, Default = "Black")]
    public string ColorName { get; set; }
    
    [Option('r', "radius", Required = false, Default = 1)]
    public double LayoutRadius { get; set; }
    
    [Option('a', "angleOffset", Required = false, Default = 0.05)]
    public double LayoutAngleOffset { get; set; }
    
    [Option("fontMinSize", Required = false, Default = 14)]
    public int MinFontSize { get; set; }
    
    [Option("fontMaxSize", Required = false, Default = 28)]
    public int MaxFontSize { get; set; }
    
    [Option("width", Required = false, Default = 2000)]
    public int ImageWidth { get; set; }
    
    [Option("height", Required = false, Default = 2000)]
    public int ImageHeight { get; set; }
}