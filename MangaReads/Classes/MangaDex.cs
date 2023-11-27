using MangaReads.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MangaReads.Classes;

public class MangaDex : IMangaApi
{
    
    HttpClient _client = new();
    
    private const string MangaDexUrl = "https://api.mangadex.org/";

    
    public async Task<string> MangaSearch(string mangaName)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, MangaDexUrl + "/manga/");
        
        // req.Headers.Add("Content-Type", "\"application/vnd.github.v3+json\"");
        // req.Headers.Add("User-Agent", "\"application/vnd.github.v3+json\"");
        
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        
        mangaName = mangaName.ToLower();


        req.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "title", mangaName },
        });

        
        
        
        var response = await _client.SendAsync(req);

        
        // getting a response but missing what it's returning

        var content = response.Content;
        
        
        return response.ToString();
    }

    public string GetMangaInformation(string mangaName)
    {
        throw new NotImplementedException();
    }
    
}