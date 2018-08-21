using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CharsetPolice.Police
{
    public class CharsetPoliceMan
    {
        private HttpClient httpClient = new HttpClient();

        public async Task<CharsetSearchResult> SearchAsync(Uri uri)
        {
            var body = await FetchAsync(uri);
            var position = SearchCharset(body);
            return new CharsetSearchResult()
            {
                Uri = uri,
                Timestamp = DateTimeOffset.UtcNow,
                IsExist = position >= 0,
                Position = position >= 0 ? position : int.MaxValue
            };
        }

        public async Task<byte[]> FetchAsync(Uri uri)
        {
            var body = await httpClient.GetByteArrayAsync(uri.ToString());
            return body;
        }

        public int SearchCharset(byte[] body)
        {
            // XXX 大文字小文字を区別しないとか面倒くさいね
            return SearchBin(body, Encoding.ASCII.GetBytes("charset"));
        }

        public int SearchBin(byte[] bin, byte[] need)
        {
            for (var i = 0; i <= bin.Length - need.Length; i++)
            {
                if (bin.Skip(i).Take(need.Length).SequenceEqual(need))
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public class CharsetSearchResult
    {
        public Uri Uri { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public bool IsExist { get; set; }
        public int Position { get; set; }
    }
}
