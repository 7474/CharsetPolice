using System;
using System.Collections.Generic;
using CharsetPolice.Police;

namespace CharsetPoliceWeb.Repositories
{
    public interface IInvestigationRepository
    {
        CharsetSearchResult Get(Uri uri);
        IEnumerable<CharsetSearchResultPerDomain> GetRanking(int skipCount, int takeCount);
        IEnumerable<CharsetSearchResult> GetRecents(int skipCount, int takeCount);
        CharsetSearchResult Put(CharsetSearchResult result);
    }
}