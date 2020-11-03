namespace Inventory.QueryHandler
{
    using Common.Core;
    using Inventory.Domain;
    using Inventory.DTO;
    using Inventory.Query;
    using Inventory.Repository;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class StoreQueryHandler : IQueryHandler<StoreQuery, QueryResult>
    {
        public readonly IStoreRepository storeRepository;

        public StoreQueryHandler(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public async Task<QueryResult> Handle(StoreQuery query)
        {
            IEnumerable<Store> stores = await storeRepository.GetAll();

           return new QueryResult
            {
                Result = Map(stores)
            };
        }

        private IEnumerable<StoreDTO> Map(IEnumerable<Store> stores)
        {
            var dtoList = new List<StoreDTO>();

            foreach(Store store in stores)
            {
                var storeDto = new StoreDTO
                {
                    StoreId = store.Id,
                    ManagerName = store.Manager,
                    CreatedDate = store.CreatedOn
                };

                dtoList.Add(storeDto);
            }

            return dtoList;
        }
    }
}
