using CarCaseTest.Business.Search.IndexModels;
using CarCaseTest.Domain.Enums;
using CarCaseTest.Domain.Models.Adverts;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCaseTest.Business.Search
{
    public class ElasticSearchManager
    {
        private string ServerUrls { get; set; }

        private string IndexName => "advert-list";

        public ElasticSearchManager(IConfiguration configuration)
        {
            this.ServerUrls = configuration.GetConnectionString("ElasticSearch");
        }

        public ElasticClient ElasticClient
        {
            get
            {
                return this.CreateElasticClient(this.ServerUrls, this.IndexName);
            }
        }

        private ElasticClient CreateElasticClient(string serverUrl, string indexName)
        {
            var settings = new ConnectionSettings(new Uri(serverUrl));
            settings.DefaultIndex(indexName);
            settings.DefaultMappingFor<AdvertListIndex>(x => x.IndexName(indexName));
            settings.EnableDebugMode(); //If you want show ES query
            var client = new ElasticClient(settings);
            //var existIndex = client.Indices.Exists("advert");           
            return client;
        }

        public bool CreateIndex()
        {
            var indexSettings = new IndexSettings();
            indexSettings.NumberOfReplicas = 1;
            indexSettings.NumberOfShards = 1;

            var createIndexDescriptor = new CreateIndexDescriptor(this.IndexName)
            .Map<AdvertListIndex>(x => x.AutoMap())
            .InitializeUsing(new IndexState() { Settings = indexSettings });

            var response = this.ElasticClient.Indices.Create(createIndexDescriptor);
            var result = this.ElasticClient.Indices.Exists(this.IndexName).Exists || response.Acknowledged;
            return result;
        }

        public bool Bulk(BulkDescriptor descriptor)
        {
            return this.ElasticClient.Bulk(descriptor).IsValid;
        }

        public bool Index<T>(T data) where T : class
        {
            var indexResponse = this.ElasticClient.Index(data, i => i.Index(this.IndexName));
            return indexResponse.IsValid;
        }

        public bool IndexMany<T>(IEnumerable<T> data) where T : class
        {
            var indexResponse = this.ElasticClient.IndexMany(data);
            return indexResponse.IsValid;
        }

        public bool Delete<T>(int id) where T : class
        {
            var result = this.ElasticClient.Delete<T>(long.Parse(id.ToString()), descriptor => descriptor.Index(this.IndexName));
            return result.Result == Result.Deleted;
        }

        public long GetAllCount<T>() where T : class
        {
            var count = this.ElasticClient.Count<T>();
            return count.Count;
        }

        public List<T> GetAllData<T>(int pageIndex, int pageSize) where T : class
        {
            var sortDescriptor = new Func<SortDescriptor<T>, IPromise<IList<ISort>>>(s => s.Descending("_score"));
            var list = this.ElasticClient.Search<T>(x => x.From(pageIndex).Take(pageSize).MatchAll().Sort(sortDescriptor));
            return list.Documents.ToList();
        }

        public T GetById<T>(int id) where T : class
        {
            var result = ElasticClient.Get(new DocumentPath<T>(id), descriptor => descriptor.Index(this.IndexName));
            return result.Source;
        }

        public AdvertSearchResponse SearchAdvert(AdvertSearchFilterModel filter)
        {
            var searchRequest = new SearchRequest();
            var filters = new List<QueryContainer>();

            searchRequest.From = filter.Page;
            searchRequest.Size = 10;
            searchRequest.Sort = GetSort2(filter.SortType);
            searchRequest.TrackTotalHits = true;

            PreparePostFilters(filters, filter);

            var boolQuery = new BoolQuery { Filter = filters };
            searchRequest.PostFilter = boolQuery;

            var result = this.ElasticClient.Search<AdvertListIndex>(searchRequest);

            var result2 = this.ElasticClient.Search<AdvertListIndex>(x => x.From(filter.Page).Take(10).TrackTotalHits(true).PostFilter(x => boolQuery).Sort(GetSort(filter.SortType)));

            return new AdvertSearchResponse { Total = result.Total, Documents = result.Documents};
        }

        private void PreparePostFilters(List<QueryContainer> filters, AdvertSearchFilterModel filter)
        {
            if (filter.CategoryId.HasValue)
            {
                var categoryFilter = new QueryContainer(new TermQuery { Field = "categoryId", Value = filter.CategoryId });
                filters.Add(categoryFilter);
            }

            if (filter.MinPrice.HasValue)
            {
                var minPriceFilter = new QueryContainer(new NumericRangeQuery { Field = "price", GreaterThanOrEqualTo = (double)filter.MinPrice });
                filters.Add(minPriceFilter);
            }

            if (filter.MaxPrice.HasValue)
            {
                var maxPriceFilter = new QueryContainer(new NumericRangeQuery { Field = "price", LessThanOrEqualTo = (double)filter.MaxPrice });
                filters.Add(maxPriceFilter);
            }

            if (filter.Gear.HasValue)
            {
                var gearFilter = new QueryContainer(new TermQuery { Field = "gearId", Value = (int)filter.Gear });
                filters.Add(gearFilter);
            }

            if (filter.Fuel.HasValue)
            {
                var fuelFilter = new QueryContainer(new TermQuery { Field = "fuelId", Value = (int)filter.Fuel });
                filters.Add(fuelFilter);
            }
        }

        private Func<SortDescriptor<AdvertListIndex>, IPromise<IList<ISort>>> GetSort(SortType? sortType)
        {
            return sortType switch
            {
                SortType.PriceAsc => new Func<SortDescriptor<AdvertListIndex>, IPromise<IList<ISort>>>(s => s.Ascending("price")),
                SortType.PriceDesc => new Func<SortDescriptor<AdvertListIndex>, IPromise<IList<ISort>>>(s => s.Descending("price")),
                SortType.KmAsc => new Func<SortDescriptor<AdvertListIndex>, IPromise<IList<ISort>>>(s => s.Ascending("km")),
                SortType.KmDesc => new Func<SortDescriptor<AdvertListIndex>, IPromise<IList<ISort>>>(s => s.Descending("km")),
                SortType.YearAsc => new Func<SortDescriptor<AdvertListIndex>, IPromise<IList<ISort>>>(s => s.Ascending("year")),
                SortType.YearDesc => new Func<SortDescriptor<AdvertListIndex>, IPromise<IList<ISort>>>(s => s.Descending("year")),
                _ => new Func<SortDescriptor<AdvertListIndex>, IPromise<IList<ISort>>>(s => s.Descending("year")),
            };
        }

        private IList<ISort> GetSort2(SortType? sortType)
        {
            return sortType switch
            {
                SortType.PriceAsc => new List<ISort> { new FieldSort { Field = "price", Order = SortOrder.Ascending } },
                SortType.PriceDesc => new List<ISort> { new FieldSort { Field = "price", Order = SortOrder.Descending } },
                SortType.KmAsc => new List<ISort> { new FieldSort { Field = "km", Order = SortOrder.Ascending } },
                SortType.KmDesc => new List<ISort> { new FieldSort { Field = "km", Order = SortOrder.Descending } },
                SortType.YearAsc => new List<ISort> { new FieldSort { Field = "year", Order = SortOrder.Ascending } },
                SortType.YearDesc => new List<ISort> { new FieldSort { Field = "year", Order = SortOrder.Descending } },
                _ => new List<ISort> { new FieldSort { Field = "year", Order = SortOrder.Descending } },
            };
        }
    }
}
