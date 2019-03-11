using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDolce
{
    interface ICommunityDataProvider
    {

        Task<IEnumerable<Community>> GetCommunity();

        Task<Community> GetCommunity(int CommunityId);

        Task AddCommunity(Community community);

        Task UpdateCommunity(Community community);

        Task DeleteCommunity(int CommunityId);
    }
}
