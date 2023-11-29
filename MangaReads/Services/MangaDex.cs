using MangaReads.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MangaReads.Classes;
using MangaReads.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MangaReads.Services;

public class MangaDex : IMangaService
{
    
    // Swagger for MangaDex
    //https://api.mangadex.org/docs/swagger.html#/
    
    // Docs
    //https://api.mangadex.org/docs/
    
    
    HttpClient _client = new();
    
    private const string MangaDexUrl = "https://api.mangadex.org/";
    
    public async Task<List<MangaSearch>> MangaSearch(string mangaName)
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        
        using var response =
            await _client.GetAsync(MangaDexUrl + "manga?" + "title=" + mangaName.ToLower());

        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        
        
        return jsonResponse;
    }

    public async Task<Manga> GetMangaInformation(string mangaName)
    {
        mangaName = mangaName.ToLower();

        try
        {
            var mangaJson = new ReadAndParseJsonFileWithNewtonsoftJson("mangaData.json");

            var mangaData = mangaJson.ReadFromJson();
            
            var mangaSaved = mangaData.Find(manga => manga.title == mangaName);

            if (mangaSaved != null)
                return mangaSaved;

            var mangaSearch = await MangaSearch(mangaName);
            
            Manga bestMatch;
            
            // TODO Iterate through the search results and find the best match
            
            
            // TODO The API request is going to be using the Manga ID
            var getMangaData = await _client.GetAsync(MangaDexUrl + mangaName);

            
            return new Manga();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);

            return new Manga();
        }
    }
    
    

    public async Task<string> GetMangaVolume(string mangaId)
    {
        // the endpoint is /manga/{id}/aggregate
        
        throw new NotImplementedException();
    }
    
}