
using Eticaret.Common.Models.Hotel;
using Eticaret.Core;
using Eticaret.Core.SessionSetting;
using Hotel.Core.Database;
using Hotel.Domain.Domains;
using Hotel.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Repository
{
    public class BaseRepository<T> : IDisposable where T : class, IBaseEntity
    {
        internal EticaretDbContext context = null;

        public DbSet<T> Entity { get { return context.Set<T>(); } }

        public BaseRepository()
        {
            context = new EticaretDbContext();
        }

        public virtual bool Add(T entity)
        {
            entity.CreateTime = DateTime.Now;
            Entity.Add(entity);

            return context.SaveChanges() > 0;
        }

        public virtual T Find(int Id)
        {
            return Entity.FirstOrDefault(x => x.Id == Id);
        }

        public virtual bool Delete(T entity)
        {
            if (entity != null && entity.Id != default(int))
            {
                var record = Find(entity.Id);
                if (record == null)
                {
                    throw new NullReferenceException(nameof(entity.Id));
                }
                record.IsDeleted = true;
                return context.SaveChanges() > 0;
            }
            throw new ArgumentOutOfRangeException(nameof(entity.Id));
        }

        public virtual bool Update(T entity)
        {
            if (entity != null && entity == null)
            {
                throw new ArgumentOutOfRangeException(nameof(entity.Id));
            }

            var record = Find(entity.Id);
            if (record == null)
            {
                throw new NullReferenceException(nameof(entity.Id));
            }

            record = entity;

            return context.SaveChanges() > 0;
        }

        public virtual bool Update_New(T entity)
        {
            context.Set<T>().AddOrUpdate(entity);
            return context.SaveChanges() > 0;
        }

        public virtual IQueryable<IBaseEntity> ListAll()
        {
            return Entity;
        }


        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
        public IQueryable<E> Query<E>() where E : class
        {
            return context.Set<E>();
        }

        readonly string _connString = ConfigurationManager.ConnectionStrings["EticaretConnStr"].ConnectionString;

        public IEnumerable<ReservationDto> GetAllReservation()
        {
            var reservations = new List<Reservation>();
            var reservationDto = new List<ReservationDto>();

            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();


                using (var command = new SqlCommand(@"SELECT [Id], [CustomerId], [RoomId], [BeginDate],[EndDate],[TotalPrice],[Status],[IsDeleted] FROM [dbo].[Reservations]", connection))
                {

                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(Dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();



                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        reservations.Add(item: new Reservation
                        {
                            Id = (int)reader["Id"],
                            CustomerId = (int)reader["CustomerId"],
                            RoomId = (int)reader["RoomId"],                            
                            BeginDate = Convert.ToDateTime(reader["BeginDate"]),
                            EndDate = Convert.ToDateTime(reader["EndDate"]),
                            TotalPrice = (decimal)reader["TotalPrice"],
                            Status = (bool)reader["Status"],
                            IsDeleted = (bool)reader["IsDeleted"]
                        });

                    }
                    var hoteId = SessionSetting.SessionSet<Hotel.Domain.Domains.Hotel>.Get("LoginHotel").Id;
                    foreach (var item in reservations.Where(k => k.Status == false && k.IsDeleted == false).ToList())
                    {
                        var room = Query<Room>().FirstOrDefault(k => k.Id == item.RoomId);
                        var img = Query<RoomImage>().FirstOrDefault(k => k.RoomId == room.Id);
                        var cus = Query<Customer>().FirstOrDefault(k => k.Id == item.CustomerId);
                        if (room.OtelId == hoteId)
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
                            reservationDto.Add(dto);
                        }
                    }
                }

                return reservationDto;
            }

        }
        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                ReservationHub.GetReservation();
            }
        }

    }

}
