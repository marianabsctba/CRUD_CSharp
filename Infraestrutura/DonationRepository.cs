using DomainDonation;
using System.Collections.Generic;
using System.Linq;

namespace Infra
{
    public class DonationRepository
    {

        private List<Donation> repositoryInMemory = new List<Donation>();

        public List<Donation> Search(string search)
        {
            var searchResult = repositoryInMemory.Where(data => data.GetTypeOfDonation() == search).ToList();
            return searchResult;
        }

        public void Add(Donation donation)
        {
            repositoryInMemory.Add(donation);
        }
    }
}
