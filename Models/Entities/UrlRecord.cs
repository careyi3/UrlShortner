using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortner.Models.Entities
{
    public class UrlRecord : BaseEntity
    {
        public string Url { get; set; }

        [NotMapped]
        public Uri Uri
        {
            get
            {
                return Url == null ? null : new Uri(Url);
            }
            set
            {
                Url = value.AbsoluteUri;
            }
        }

        [NotMapped]
        public string Code
        {
            get
            {
                var base64Guid = Convert.ToBase64String(Id.ToByteArray());
                base64Guid = base64Guid.Replace("/", "_");
                base64Guid = base64Guid.Replace("+", "-");
                return base64Guid.Substring(0, 22);
            }
        }

        public UrlRecord() { }

        public UrlRecord(Uri uri)
        {
            Uri = uri;
        }
    }
}