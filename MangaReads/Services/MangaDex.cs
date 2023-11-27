using MangaReads.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MangaReads.Classes;

public class MangaDex : IMangaService
{
    
    // Swagger for MangaDex
    //https://api.mangadex.org/docs/swagger.html#/
    
    // Docs
    //https://api.mangadex.org/docs/
    
    
    HttpClient _client = new();
    
    private const string MangaDexUrl = "https://api.mangadex.org/";

    
    public async Task<string> MangaSearch(string mangaName)
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        
        using var response =
            await _client.GetAsync(MangaDexUrl + "manga?" + "title=" + mangaName.ToLower());

        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        // TODO We have a response but for now, this will be where the code is left. It's unformatted from mangadex
        return jsonResponse;
    }

    public string GetMangaInformation(string mangaName)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetMangaVolume(string mangaId)
    {
        // the endpoint is /manga/{id}/aggregate
        
        throw new NotImplementedException();
    }
    
}