﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Chiara.Brichot.FeatureMatching.Tests;
public class FeatureMatchingUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath();
        var imageScenesData = new List<byte[]>();
        foreach (var imagePath in
                 Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            imageScenesData.Add(imageBytes);
        }
        var objectImageData = await
            File.ReadAllBytesAsync(Path.Combine(executingPath, "BRICHOT-Chiara-object.jpg"));
        var detectObjectInScenesResults = new ObjectDetection().DetectObjectInScenes(objectImageData, imageScenesData);
            
        Assert.Equal("[{\"X\":116,\"Y\":158},{\"X\":87,\"Y\":272},{\"X\":263,\"Y\":294},{\"X\":276,\"Y\":179}]",JsonSerializer.Serialize(detectObjectInScenesResults[0].Points));
        Assert.Equal("[{\"X\":116,\"Y\":158},{\"X\":87,\"Y\":272},{\"X\":263,\"Y\":294},{\"X\":276,\"Y\":179}]",JsonSerializer.Serialize(detectObjectInScenesResults[1].Points));
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath = Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}