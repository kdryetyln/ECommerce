using Elasticsearch.Net;
using Eticaret.Common.Models.Hotel;
using Eticaret.Core.Services.Interfaces;
using Hotel.Core.Repository;
using Hotel.Core.Services;
using Nest;
using ServiceStack.Redis;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Core.Services
{
    public class ElasticSearchService : IElasticSearch
    {
       
        public void CreateIndex()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node)
                .DefaultIndex("newhotel");
                
            var client = new ElasticClient(settings);
            var del = client.DeleteIndex("newhotel");
            var newIndex = client.CreateIndex("newhotel", p => p
               .Settings(q => q
                    .NumberOfReplicas(0)
                    .NumberOfShards(1))
                .Mappings(m => m
                    .Map<HotelRoomDto>(k => k
                    .AutoMap())));
            var model = Service.RoomService.hotelRooms();
            foreach (var item in model)
            {
                var indexResponse = client.IndexDocument(item);
            }
        }

        public List<HotelRoomDto> Search(string key)
        {
                var node = new Uri("http://localhost:9200");
                var settings = new ConnectionSettings(node)
                    .DefaultIndex("newhotel");

                var client = new ElasticClient(settings);
                var searchResponse = client.Search<HotelRoomDto>(s => s
                   .Index("newhotel")
                                    .AllTypes()
                                    .Query(q => q
                                        .Prefix(f => f.Field(x => x.HotelName).Value(key))
                                    )
                                );

            return searchResponse.Documents.ToList();           
        }
    }
}
