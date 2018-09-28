using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eticaret.Common.Models.Hotel;
using Eticaret.Core.Services.Interfaces;
using Hotel.Common.Models.General;
using Hotel.Core.Repository;
using Hotel.Core.Services;
using Hotel.Domain.Domains;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Eticaret.Core.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        public UnitOfWork UnitOfWork {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public ServiceResult Result {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public void AddReservation(Reservation reservation)
        {
            using (BaseRepository<Hotel.Domain.Domains.Hotel> _bR = new BaseRepository<Hotel.Domain.Domains.Hotel>())
            {
                var hotelId = _bR.Query<Room>().FirstOrDefault(a => a.Id == reservation.RoomId).OtelId;
                
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (IConnection connection = factory.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: hotelId.ToString(),
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = JsonConvert.SerializeObject(reservation);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: hotelId.ToString(),
                                         basicProperties: null,
                                         body: body);
                }
            }
            BaseRepository<Reservation> res = new BaseRepository<Reservation>();
            res.Add(reservation);

        }

        public void GetReservation(int id)
        {
            using (BaseRepository<Reservation> _bR=new BaseRepository<Reservation>())
            {
                var hoteId = id;
                string i = hoteId.ToString();
                List<Reservation> reservations = new List<Reservation>();
                var factory2 = new ConnectionFactory() { HostName = "localhost" };
                using (IConnection connection = factory2.CreateConnection())
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: i,
                                            durable: false,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

                    var consumer = new QueueingBasicConsumer(channel);

                    uint h = channel.MessageCount(i);

                    channel.BasicConsume(queue: i,
                        autoAck: true,
                        consumer: consumer
                    );

                    for (int j = 0; j < h; j++)
                    {
                        var datas = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var data = Encoding.UTF8.GetString(datas.Body);
                        Reservation mdata = JsonConvert.DeserializeObject<Reservation>(data);
                        reservations.Add(mdata);
                    }
                    foreach (var item in reservations)
                    {
                        var test = item;
                        _bR.Add(test);
                    }
                }
            }
        }

        public List<ReservationDto> GetReservationDb()
        {
            using (BaseRepository<Reservation> _bR=new BaseRepository<Reservation>())
            {
                var hoteId = SessionSetting.SessionSetting.SessionSet<Hotel.Domain.Domains.Hotel>.Get("LoginHotel").Id;
               
                var resList = _bR.Query<Reservation>().Where(k=>k.Status==false&&k.IsDeleted==false).ToList();
                List<ReservationDto> reservations = new List<ReservationDto>();
                foreach (var item in resList)
                {
                    var room = _bR.Query<Room>().FirstOrDefault(k => k.Id == item.RoomId);
                    var img = _bR.Query<RoomImage>().FirstOrDefault(k => k.RoomId == room.Id);
                    var cus = _bR.Query<Customer>().FirstOrDefault(k => k.Id == item.CustomerId);
                    if(room.OtelId==hoteId)
                    {
                        ReservationDto dto = new ReservationDto()
                        {
                            Id = item.Id,
                            CustomerId = cus.Id,
                            RoomId = item.RoomId,
                            BeginDate = item.BeginDate,
                            EndDate = item.EndDate,
                            TotalPrice = item.TotalPrice,
                            Status = item.Status,
                            Email = cus.Email,
                            Name = cus.Name,
                            Title = img.Title
                        };
                        reservations.Add(dto);
                    }
                }
                return reservations;
            }                    
        }

        public void UpdateReservation(int id, bool key)
        {
            using (BaseRepository<Reservation> _bR=new BaseRepository<Reservation>())
            {
                var res = _bR.Query<Reservation>().FirstOrDefault(k => k.Id == id);
                if (key)
                {                    
                    res.Status = true;
                    _bR.Update_New(res);
                }
                else
                {
                    res.IsDeleted = true;
                    _bR.Update_New(res);
                }
            }
        }
    }
}
