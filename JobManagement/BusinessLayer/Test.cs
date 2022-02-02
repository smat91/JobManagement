using System;
using System.Runtime.CompilerServices;
using DataAccessLayer.Repositories;

namespace BusinessLayer.Helper
{
    public class Test
    {
        private readonly ItemGroupRepository itemGroupRepository;

        public Test(string connectionString)
        {
            itemGroupRepository = new ItemGroupRepository(connectionString);
        }

        public void GetTestData()
        {
            var test = itemGroupRepository.GetItemsWithLevel();
        }

    }
}
