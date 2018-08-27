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

        public IEnumerable<CharsetSearchResultPerDomain> BuildRanking(IEnumerable<CharsetSearchResult> results)
        {
            return results.GroupBy(x => x.Uri.Host).Select(x =>
            {
                return new CharsetSearchResultPerDomain()
                {
                    Domain = x.Key,
                    Uri = x.First().Uri,
                    Timestamp = DateTimeOffset.UtcNow,
                    IsExist = x.Any(y => y.IsExist),
                    //Position = x.Where(y => y.IsExist).Max(y => y.Position)
                    Position = x.Max(y => y.Position)
                };
            }).OrderByDescending(x => x.Position);
        }
    }

    public class CharsetSearchResult
    {
        private const int CHARSET_POSITION_THREASHOLD = 1024;

        public Uri Uri { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public bool IsExist { get; set; }
        public int Position { get; set; }

        public CharsetSearchResultStatus Status
        {
            get
            {
                if (IsExist)
                {
                    if (Position <= CHARSET_POSITION_THREASHOLD)
                    {
                        return CharsetSearchResultStatus.Ok;
                    }
                    else
                    {
                        return CharsetSearchResultStatus.Error;
                    }
                }
                else
                {
                    return CharsetSearchResultStatus.Critical;
                }
            }
        }
        public enum CharsetSearchResultStatus
        {
            Ok,
            Error,
            Critical
        }
    }

    public class CharsetSearchResultPerDomain : CharsetSearchResult
    {
        public string Domain { get; set; }
        // とりあえずいっしょ
    }
}
