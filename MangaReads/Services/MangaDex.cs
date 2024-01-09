using MangaReads.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using MangaReads.Classes;
using MangaReads.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Volume = MangaReads.DTOs.Volume;

namespace MangaReads.Services;

public class MangaDex : IMangaService
{
    
    // Swagger for MangaDex
    //https://api.mangadex.org/docs/swagger.html#/
    
    // Docs
    //https://api.mangadex.org/docs/
    
    
    HttpClient _client = new();
    
    private const string MangaDexUrl = "https://api.mangadex.org/";
    
    public async Task<List<Manga>> MangaSearch(string mangaName)
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        
        using var response =
            await _client.GetAsync(MangaDexUrl + "manga?" + "title=" + mangaName.ToLower());
        
        var heck = response.Content.ReadFromJsonAsync<SearchResult>();
        
        var mangaList = new List<Manga> {};
        
        foreach (var mangaSearch in heck.Result.data)
        {
            mangaList.Add(MangaSearchToMangaObject(mangaSearch));
        }

        return mangaList;
    }

    public async Task<Manga> GetMangaInformation(string mangaId)
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        
        using var response =
            await _client.GetAsync(MangaDexUrl + "manga/" + mangaId );
        
        var heck = response.Content.ReadFromJsonAsync<InformationResult>();

        return MangaSearchToMangaObject(heck.Result.data);
    }
    
    public async Task<List<Classes.Volume>> GetMangaVolume(string mangaId)
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        _client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        
        var getMangaData = 
            await _client.GetAsync(MangaDexUrl + "manga/" + mangaId + "/aggregate");
            
        var heck = getMangaData.Content.ReadFromJsonAsync<VolumeResult>();

        var listOfVolumes = new List<Classes.Volume>();
        
        foreach (var volume in heck.Result.volumes)
        {
            var v = JsonConvert.DeserializeObject<Volume>(volume.Value.ToString());

            if (v is null || v.volume.Equals("none")) 
                continue;
            
            var newVolume = new Classes.Volume
            {
                volumeNumber = int.Parse(v.volume),
            };
            
            listOfVolumes.Add(newVolume);
        }
        
        return listOfVolumes;
    }

    // public async Task<MangaSearch> GetMangaFromSearch(string mangaName)
    // {
    //     mangaName = mangaName.ToLower();
    //     
    //     var mangaSearch = await MangaSearch(mangaName);
    //
    //     if (mangaSearch.Count == 0)
    //     {
    //         return new MangaSearch();
    //     }
    //         
    //     var bestMatch = 
    //         mangaSearch.Find(manga => mangaName.Equals(manga.attributes.title.en.ToLower()));
    //         
    //     return bestMatch!;
    // }
    
    private static Manga MangaSearchToMangaObject(MangaSearch mangaSearch)
    {
        return new Manga
        {
            title = mangaSearch.attributes.title.en,
            description = mangaSearch.attributes.description.en,
            thirdPartyId = mangaSearch.id,
        };
    }
    
    
    
}