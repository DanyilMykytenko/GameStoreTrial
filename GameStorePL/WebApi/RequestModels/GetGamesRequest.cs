﻿namespace WebApi.RequestModels;

public class GetGamesRequest
{
    public string MaxPrice { get; set; }

    public string MinPrice { get; set; }

    public string Name { get; set; }

    public string DatePublishing { get; set; }

    public string Sort { get; set; }

    public int Page { get; set; }

    public string PageCount { get; set; }

    public ICollection<string> Genres { get; set; }

    public ICollection<string> Platforms { get; set; }

    public ICollection<string> Publishers { get; set; }
}
