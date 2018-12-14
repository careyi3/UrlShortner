using System;
using System.Threading.Tasks;
using UrlShortner.Models.Entities;
using UrlShortner.Data;

namespace UrlShortner.Services
{
    public class UrlShortnerService : IUrlShortnerService
    {
        private IGenericRepository<UrlRecord> _urlRecordRepository;
        private IConfigService _configService;

        public UrlShortnerService(IGenericRepository<UrlRecord> urlRecordRepository, IConfigService configService)
        {
            _urlRecordRepository = urlRecordRepository;
            _configService = configService;
        }

        public async Task<Uri> FetchUrlFromCodeAsync(string code)
        {
            var id = GuidFromCode(code);
            var record = await _urlRecordRepository.FindAsync(id);
            return record.Uri;
        }

        public async Task<Uri> StoreUrlAsync(Uri uri)
        {
            var record = new UrlRecord(uri);
            record = await _urlRecordRepository.CreateAsync(record);
            return BuildShortUriForUrlRecord(record);
        }

        private string RootUrl()
        {
            return _configService.GetConfiguration("rootUrl");
        }

        private Uri BuildShortUriForUrlRecord(UrlRecord record)
        {
            var urlString = RootUrl() + record.Code;
            return new Uri(urlString);
        }

        private Guid GuidFromCode(string code)
        {
            code = code.Replace("_", "/");
            code = code.Replace("-", "+");
            byte[] guidBytes = Convert.FromBase64String($"{code}==");
            return new Guid(guidBytes);
        }
    }
}