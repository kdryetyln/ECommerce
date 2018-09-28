using Eticaret.Common.Models.RoomDtos;
using Eticaret.Core.Services.Interfaces;
using Hotel.Common.Models.General;
using Hotel.Core.Repository;
using Hotel.Core.Services;
using Hotel.Domain.Domains;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ServiceStack;
using ServiceStack.Redis;
using Eticaret.Common.Models.Hotel;
using RoomDto = Eticaret.Common.Models.Hotel.RoomDto;

namespace Eticaret.Core.Services
{
    public class RoomService : IRoomService
    {

        public UnitOfWork UnitOfWork { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ServiceResult Result { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddRoom(Room room, HttpPostedFileBase image)
        {
            using (BaseRepository<Room> _bR = new BaseRepository<Room>())
            {
                _bR.Add(room);

            }
            string _imagename = Path.GetFileName(image.FileName);
            string _path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/images/"), _imagename);
            image.SaveAs(_path);
            using (BaseRepository<RoomImage> _bR = new BaseRepository<RoomImage>())
            {
                RoomImage roomImage = new RoomImage()
                {
                    Title = _imagename,
                    Path = _path,
                    RoomId = room.Id

                };
                _bR.Add(roomImage);
               
            }

        }

        public void AddRoomType(RoomType roomType)
        {
            using (BaseRepository<RoomType> _bR = new BaseRepository<RoomType>())
            {
                _bR.Add(roomType);
            }
        }

        public void DeleteRoom(int id)
        {
            using (BaseRepository<Room> _br= new BaseRepository<Room>())
            {
                var imageList = _br.Query<RoomImage>().Where(k => k.RoomId == id).ToList();
                foreach (var img in imageList)
                {
                    using (BaseRepository<RoomImage> _brI = new BaseRepository<RoomImage>())
                    {
                        _brI.Delete(img);
                    }
                }
                var room = _br.Query<Room>().FirstOrDefault(k => k.Id == id);
                _br.Delete(room);
            }
        }

        public IEnumerable<RoomDtos> GetRoomList()
        {
            using (BaseRepository<Room> _bR = new BaseRepository<Room>())
            {
                var userId = SessionSetting.SessionSetting.SessionSet<Hotel.Domain.Domains.Hotel>.Get("LoginHotel").Id;
                var modelRoom = _bR.Query<Room>().Where(k => k.OtelId == userId&&k.IsDeleted!=true).ToList();
                List<RoomDtos> listRoomDtos = new List<RoomDtos>();
                foreach (var item in modelRoom)
                {
                    var model = new RoomDtos
                    {
                        Room = item,
                        RoomType = _bR.Query<RoomType>().Where(k => k.Id == item.RoomTypeId).FirstOrDefault(),
                        RoomImage = _bR.Query<RoomImage>().Where(k => k.RoomId == item.Id).FirstOrDefault(),

                    };
                    listRoomDtos.Add(model);
                }
                return listRoomDtos;
            }

        }

        public IEnumerable<RoomType> GetRoomTypes()
        {
            using (BaseRepository<RoomType> _bR = new BaseRepository<RoomType>())
            {
                return _bR.Query<RoomType>().ToList();
            }
        }

        public List<HotelRoomDto> hotelRooms()
        {
            using (BaseRepository<Hotel.Domain.Domains.Hotel> _bR=new BaseRepository<Hotel.Domain.Domains.Hotel>())
            {
                var hotelList = _bR.Query<Hotel.Domain.Domains.Hotel>().ToList();
                List<HotelRoomDto> hotelRoomDtos = new List<HotelRoomDto>();
                foreach (var item in hotelList)
                {
                    HotelRoomDto modelList = _bR.Query<Hotel.Domain.Domains.Hotel>().Where(k => k.Id == item.Id).Select(k => new HotelRoomDto
                    {
                        Id=item.Id,
                        hotelId = item.Id,
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
                            OtelId = item.Id,
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
                    hotelRoomDtos.Add(modelList);
                }
                return hotelRoomDtos;
            }
        }

        public void UpdateRoom(Room room, HttpPostedFileBase image)
        {
            using (BaseRepository<Room> _bR=new BaseRepository<Room>())
            {
                var result = _bR.Query<Room>().Where(k => k.Id == room.Id).Any();
                if(result)
                {
                    room.OtelId= Core.SessionSetting.SessionSetting.SessionSet<Hotel.Domain.Domains.Hotel>.Get("LoginHotel").Id;
                    _bR.Update_New(room);
                }
                
            }
            string _imagename = Path.GetFileName(image.FileName);
            string _path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/images/"), _imagename);
            image.SaveAs(_path);
            using (BaseRepository<RoomImage> _bR = new BaseRepository<RoomImage>())
            {
                RoomImage roomImage = new RoomImage()
                {
                    Title = _imagename,
                    Path = _path,
                    RoomId = room.Id

                };
                _bR.Add(roomImage);

            }
        }
    }
}
