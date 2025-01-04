﻿using Autofac;
using TagsCloudConsole.Validators;
using TagsCloudVisualization.TagsCloudImageCreators;

namespace TagsCloudConsole;

public static class Program
{
    public static void Main(string[] args)
    {
        var options = CommandLine.Parser.Default
            .ParseArguments<TagsCloudVisualizationOptions>(args)
            .Value;
        var validator = new OptionsValidator();
        var validatedParams = validator.Validate(options);

        if (!validatedParams.IsSuccess)
        {
            Console.WriteLine(validatedParams.Error);
            return;
        }

        var container = new TagsCloudContainerBuilder(options).Build();

        var creator = container.Resolve<ITagsCloudImageCreator>();
        var result = creator.CreateImageWithTags(options.InputFilePath);

        Console.WriteLine(result.IsSuccess
            ? $"Application successfully create image on path: {options.OutputFilePath}"
            : $"Application failed with error: {result.Error}");
    }
}