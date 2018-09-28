using Eticaret.Common.Models.Hotel;
using Eticaret.Core.SessionSetting;
using Hotel.Common.Models.General;
using Hotel.Core.Repository;
using Hotel.Core.Services.Interfaces;
using Hotel.Domain.Domains;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Services
{
    public class LoginService : ILoginService
    {
        public UnitOfWork UnitOfWork { get; set; }
        public ServiceResult Result { get; set; }

        public void AddRedis()
        {
            using (BaseRepository<Room> _bR=new BaseRepository<Room>())
            {
                using (var redisManager = new PooledRedisClientManager())
                {
                    using (var redis = redisManager.GetClient())
                    {
                        var myId = SessionSetting.SessionSet<Domain.Domains.Hotel>.Get("LoginHotel").Id;
  
                            HotelRoomDto modelList = _bR.Query<Hotel.Domain.Domains.Hotel>().Where(k => k.Id == myId).Select(k => new HotelRoomDto
                            {
                                hotelId=myId,
                                Address = k.Address,
                                AreaId = k.AreaId,
                                Email = k.Email,
                                Gym = k.Gym,
                                HotelBar = k.HotelBar,
                                HotelName = k.HotelName,
                                Park = k.Park,
                                Pool = k.Pool,
                                Restaurant = k.Restaurant,
                                RoomService = k.RoomService,
                                Spa = k.Spa,
                                TelNum = k.TelNum,
                                Terrace = k.Terrace,
                                WashingMachine = k.WashingMachine,
                                roomDtos = k.rooms.Where(x => x.OtelId == k.Id).Select(x => new RoomDto
                                {

                                    airConditioning = x.RoomType.airConditioning,
                                    Bathroom = x.RoomType.Bathroom,
                                    Fund = x.RoomType.Fund,
                                    Jakuzi = x.RoomType.Fund,
                                    Telephone = x.RoomType.Telephone,
                                    MiniBar = x.RoomType.MiniBar,
                                    TV = x.RoomType.TV,
                                    Wifi = x.RoomType.Wifi,
                                    OtelId = myId,
                                    RoomId = x.Id,
                                    Name = x.RoomType.Name,
                                    PersonCount = x.RoomType.PersonCount,
                                    Price = x.Price,
                                    Stock = x.Stock,
                                    roomImageDtos = x.roomImages.Where(a => a.RoomId == x.Id).Select(a => new RoomImageDto
                                    {
                                        IsActive = a.IsActive,
                                        Path = a.Path,
                                        Title = a.Title
                                    }).ToList()
                                }).ToList()
                            }).FirstOrDefault();


                            var data = redis.Get<HotelRoomDto>("otel_cache_" + myId);
                            if (data != null)
                            {
                                redis.Set<HotelRoomDto>("otel_cache_" + myId, modelList);
                            }
                            else
                            {
                                redis.Add("otel_cache_" + myId, modelList);
                            }

                    }
                }

            }
        }

        public Customer CustomerLogin(Customer custom)
        {
            using (BaseRepository<Customer> _bR = new BaseRepository<Customer>())
            {
                
                var email = custom.Email;
                var pass = custom.Password;
                var thisCustom = _bR.Query<Customer>().Where(c => c.Email.Equals(email) && c.Password.Equals(pass)).FirstOrDefault();
                custom.Id = thisCustom.Id;

                SessionSetting.SessionSet<Customer>.Set(custom, "LoginCustomer");
                return thisCustom;
            }
                
        }

        public string CustomerRegister(Customer custom)
        {
            using (BaseRepository<Customer> _bR = new BaseRepository<Customer>())
            {
                var email = custom.Email;
                var pass = custom.Password;
                var result = _bR.Query<Customer>().Where(c => c.Email.Equals(email)).Any();
                if(!result)
                {                   
                    _bR.Add(custom);
                    return "Success";
                }
                else
                {
                    return "No Success!!";
                }
            }
        }

        public IEnumerable<Area> GetAreas()
        {
            using (BaseRepository<Area> _bR = new BaseRepository<Area>())
            {
                return _bR.Query<Area>().ToList();
            }


        }

        public Domain.Domains.Hotel HotelLogin(Domain.Domains.Hotel hotel)
        {
            using (BaseRepository<Domain.Domains.Hotel> _bR = new BaseRepository<Domain.Domains.Hotel>())
            {
                var email = hotel.Email;
                var pass = hotel.Password;
                var thisHotel= _bR.Query<Domain.Domains.Hotel>().Where(c => c.Email.Equals(email) && c.Password.Equals(pass)).FirstOrDefault();


                return thisHotel;
            }
        }

        public string HotelRegister(Hotel.Domain.Domains.Hotel hotel)
        {
            using (BaseRepository<Domain.Domains.Hotel> _bR = new BaseRepository<Domain.Domains.Hotel>())
            {
                var email = hotel.Email;
                var pass = hotel.Password;
                var result = _bR.Query<Domain.Domains.Hotel>().Where(c => c.Email.Equals(email)).Any();
                if (!result)
                {
                    _bR.Add(hotel);
                    return "Success";
                }
                else
                {
                    return "No Success!!";
                }
            }
        }       

        Customer ILoginService.GetCustomer()
        {
            var customer = SessionSetting.SessionSet<Customer>.Get("LoginCustomer");
            return customer;
        }
    }
}
