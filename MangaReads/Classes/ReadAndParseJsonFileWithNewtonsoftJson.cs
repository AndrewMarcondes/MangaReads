﻿using Newtonsoft.Json;

namespace MangaReads.Classes;

public class ReadAndParseJsonFileWithNewtonsoftJson
{
    private readonly string _sampleJsonFilePath;

    public ReadAndParseJsonFileWithNewtonsoftJson(string sampleJsonFilePath)
    {
        _sampleJsonFilePath = sampleJsonFilePath;
    }
    
    public IEnumerable<object> ReadFromJson()
    {
        using var reader = new StreamReader(_sampleJsonFilePath);
        
        var json = reader.ReadToEnd();
        
        var data = JsonConvert.DeserializeObject<IEnumerable<object>>(json);
        
        return data!;
    }

    public void WriteToJson(IEnumerable<object> jsonData)
    {
        using var writer = File.CreateText(_sampleJsonFilePath);

        var serializer = new JsonSerializer();
        
        serializer.Serialize(writer, jsonData);
    }
}