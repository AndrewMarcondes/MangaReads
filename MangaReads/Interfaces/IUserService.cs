﻿namespace MangaReads.Interfaces;

public interface IUserService
{
    string GetUser(string userName);
    void UpdateUser(string userName);
    void AddUserManga(string userName, string mangaName);
    void UpdateUserMangaVolumeNumber(string userName, string mangaName, string newVolumeNumber);
    void UpdateUserMangaReadingStatus(string userName, string mangaName);
    void DeleteUserManga(string userName, string mangaName);
}