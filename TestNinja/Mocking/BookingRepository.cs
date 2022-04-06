using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId);
        Booking GetOverlappingBooking(Booking booking, IEnumerable<Booking> otherActiveBookings);
    }

    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Status != "Cancelled");

            if (excludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id != excludedBookingId.Value);

            return bookings;
        }

        public Booking GetOverlappingBooking(Booking booking, IEnumerable<Booking> otherActiveBookings)
        {
            var overlappingBooking = otherActiveBookings.FirstOrDefault(
                b =>
                    booking.ArrivalDate >= b.ArrivalDate
                    && booking.ArrivalDate < b.DepartureDate
                    || booking.DepartureDate > b.ArrivalDate
                    && booking.DepartureDate <= b.DepartureDate);

            return overlappingBooking;
        }
    }

    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }
}