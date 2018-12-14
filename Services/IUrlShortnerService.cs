using System;
using System.Net;
using UrlShortner.Models.Entities;
using System.Threading.Tasks;

namespace UrlShortner.Services
{
    public interface IUrlShortnerService
    {
        Task<Uri> FetchUrlFromCodeAsync(string code);

        Task<Uri> StoreUrlAsync(Uri uri);
    }
}