using CharsetPolice.Police;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharsetPoliceWeb.Repositories
{
    public class InvestigationRepository : IInvestigationRepository
    {
        // XXX 仮実装
        private IDictionary<string, CharsetSearchResult> store = new Dictionary<string, CharsetSearchResult>();

        public CharsetSearchResult Put(CharsetSearchResult result)
        {
            store[result.Uri.ToString()] = result;
            return result;
        }

        public CharsetSearchResult Get(Uri uri)
        {
            return store[uri.ToString()];
        }

        public IEnumerable<CharsetSearchResult> GetRecents(int skipCount, int takeCount)
        {
            return store.Values.OrderByDescending(x => x.Timestamp)
                .Skip(skipCount)
                .Take(takeCount);
        }

        public IEnumerable<CharsetSearchResultPerDomain> GetRanking(int skipCount, int takeCount)
        {
            var policeMan = new CharsetPoliceMan();
            var ranking = policeMan.BuildRanking(store.Values);
            return ranking.Skip(skipCount)
                .Take(takeCount);
        }

    }
}
