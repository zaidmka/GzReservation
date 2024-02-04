using GzReservation.Server.Data;
using GzReservation.Server.DTOs;
using GzReservation.Shared;

namespace GzReservation.Server.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly DataContext _dataContext;

        public ReservationService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<ServiceResponse<Reservation>> AddNewReservation(ReservationDto reservationDto)
        {
            try
            {
                Entity entity = await _dataContext.entities.FindAsync(reservationDto.EntityId);

                if (entity != null)
                {
                    // Define valid weekdays
                    var validWeekdays = new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday };

                    // Check if reservation date is on a valid weekday
                    if (!validWeekdays.Contains(reservationDto.reservation_date.DayOfWeek))
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "Reservations can only be made from Sunday to Thursday.",
                            Success = false
                        };
                    }
                    // Check global daily limit
                    const int maxReservationsPerDayGlobal = 25;
                    int totalReservationsForDay = await _dataContext.reservations
                        .Where(r => r.reservation_date == reservationDto.reservation_date)
                        .CountAsync();

                    if (totalReservationsForDay >= maxReservationsPerDayGlobal)
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "Global maximum daily reservation limit exceeded.",
                            Success = false
                        };
                    }

                    // Calculate the start and end of the next active week
                    var today = DateTime.Today;
                    var startOfNextWeek = today.AddDays((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7);
                    var endOfNextWeek = startOfNextWeek.AddDays(4);

                    // Convert DateTime to DateOnly for comparison
                    var startOfNextWeekDateOnly = DateOnly.FromDateTime(startOfNextWeek);
                    var endOfNextWeekDateOnly = DateOnly.FromDateTime(endOfNextWeek);

                    // Check if reservation date is within the next active week
                    if (reservationDto.reservation_date < startOfNextWeekDateOnly || reservationDto.reservation_date > endOfNextWeekDateOnly)
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "Reservations can only be made for the next active week (Sunday to Thursday).",
                            Success = false
                        };
                    }

                    // Retrieve and count reservations for the same EntityId and date
                    int reservationsCountForDay = await _dataContext.reservations
                        .Where(r => r.EntityId == entity.id && r.reservation_date == reservationDto.reservation_date)
                        .CountAsync();

                    // Compare the count to max_day
                    if (reservationsCountForDay >= entity.max_day)
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "Maximum daily reservation limit exceeded.",
                            Success = false
                        };
                    }

                    // Retrieve the count of reservations for the same EntityId for the next active week
                    int reservationsCountForWeek = await _dataContext.reservations
                        .Where(r => r.EntityId == entity.id && r.reservation_date >= startOfNextWeekDateOnly && r.reservation_date <= endOfNextWeekDateOnly)
                        .CountAsync();

                    // Compare the count to max_week
                    if (reservationsCountForWeek >= entity.max_week)
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "Maximum weekly reservation limit exceeded.",
                            Success = false
                        };
                    }
                    else
                    {
                        // Create and add the new reservation
                        var newReservation = new Reservation
                        {
                            id = reservationDto.id,
                            action_date = reservationDto.action_date.ToLocalTime(),
                            doc_no = reservationDto.doc_no,
                            EntityId = reservationDto.EntityId,
                            full_name = reservationDto.full_name,
                            mother_name = reservationDto.mother_name,
                            reservation_date = reservationDto.reservation_date
                        };

                        _dataContext.reservations.Add(newReservation);
                        await _dataContext.SaveChangesAsync();

                        return new ServiceResponse<Reservation>
                        {
                            Data = newReservation,
                            Message = "Reservation added successfully.",
                            Success = true
                        };
                    }
                }
                else
                {
                    // Handle the case where the Entity with the specified EntityId was not found
                    return new ServiceResponse<Reservation>
                    {
                        Data = null,
                        Message = "Entity with the specified Id not found.",
                        Success = false
                    };
                }

            }
            catch (DbUpdateException ex)
            {
                return new ServiceResponse<Reservation> { Data = null, Message = "Database error: " + ex, Success = false };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Reservation> { Data = null, Message = "An error occurred: " + ex, Success = false };
            }
        }



        public Task<ServiceResponse<Reservation>> DeleteReservation(int reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<int>>> GetFreeSpots()
        {
            try
            {
                // Define valid weekdays and maximum global reservations per day
                var validWeekdays = new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday };
                const int maxReservationsPerDayGlobal = 25;

                // Calculate the start and end of the next active week
                var today = DateTime.Today;
                var startOfNextWeek = today.AddDays((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7);
                var endOfNextWeek = startOfNextWeek.AddDays(4);

                // Convert DateTime to DateOnly for comparison
                var startOfNextWeekDateOnly = DateOnly.FromDateTime(startOfNextWeek);
                var endOfNextWeekDateOnly = DateOnly.FromDateTime(endOfNextWeek);
                // Initialize list to hold free spots for each day
                List<int> freeSpotsList = new List<int>();

                // Loop through each day of the next active week
                for (var date = startOfNextWeekDateOnly; date <= endOfNextWeekDateOnly; date = date.AddDays(1))
                {
                    if (validWeekdays.Contains(date.DayOfWeek))
                    {
                        // Count the reservations for the day
                        int reservationsCountForDay = await _dataContext.reservations
                            .Where(r => r.reservation_date == date)
                            .CountAsync();

                        // Calculate free spots (considering the global limit)
                        int freeSpots = maxReservationsPerDayGlobal - reservationsCountForDay;

                        // Add the number of free spots for the day to the list
                        freeSpotsList.Add(freeSpots);
                    }
                }

                // Return the list of free spots for each day
                return new ServiceResponse<List<int>>
                {
                    Data = freeSpotsList,
                    Message = "Free spots retrieved successfully.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<int>>
                {
                    Data = null,
                    Message = $"An error occurred: {ex.Message}",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<List<int>>> GetFreeSpotsByEntity(int entityId)
        {
            Entity entity = await _dataContext.entities.FindAsync(entityId);

            if (entity == null) { }

            try
            {
                // Define valid weekdays and maximum global reservations per day
                var validWeekdays = new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday };
                const int maxReservationsPerDayGlobal = 25;

                // Calculate the start and end of the next active week
                var today = DateTime.Today;
                var startOfNextWeek = today.AddDays((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7);
                var endOfNextWeek = startOfNextWeek.AddDays(4);

                // Convert DateTime to DateOnly for comparison
                var startOfNextWeekDateOnly = DateOnly.FromDateTime(startOfNextWeek);
                var endOfNextWeekDateOnly = DateOnly.FromDateTime(endOfNextWeek);
                // Initialize list to hold free spots for each day
                List<int> freeEntitySpotsList = new List<int>();
                int totalWeekPerEntity = 0;
                // Loop through each day of the next active week
                bool exceededMaxWeekLimit = false;

                for (var date = startOfNextWeekDateOnly; date <= endOfNextWeekDateOnly; date = date.AddDays(1))
                {
                    if (validWeekdays.Contains(date.DayOfWeek))
                    {
                        // Count the reservations for the day
                        int reservationsCountForDay = await _dataContext.reservations
                            .Where(r => r.reservation_date == date && r.EntityId == entityId)
                            .CountAsync();

                        // Update the total reservations for the week
                        totalWeekPerEntity += reservationsCountForDay;

                        // Check if the weekly limit has been reached or exceeded
                        if (totalWeekPerEntity >= entity.max_week)
                        {
                            freeEntitySpotsList.Add(0);
                            Console.WriteLine($"Date: {date.ToShortDateString()}: Weekly reservation limit reached. No free spots available.");
                        }
                        else
                        {
                            // Calculate the free spots for the day
                            int dailyFreeSpots = entity.max_day - reservationsCountForDay;

                            // Ensure daily free spots do not exceed the remaining weekly limit
                            int weeklyFreeSpots = entity.max_week - totalWeekPerEntity;
                            int actualFreeSpots = Math.Min(dailyFreeSpots, weeklyFreeSpots);

                            // Add the calculated free spots for the day to the list
                            freeEntitySpotsList.Add(actualFreeSpots);

                            // Log the details
                            Console.WriteLine($"Date: {date.ToShortDateString()}, Free Spots: {actualFreeSpots}, Total Reservations for Week (So Far): {totalWeekPerEntity}");
                        }
                    }
                }



                totalWeekPerEntity = entity.max_week - totalWeekPerEntity;
                freeEntitySpotsList.Add(totalWeekPerEntity);


                // Return the list of free spots for each day
                return new ServiceResponse<List<int>>
                {
                    Data = freeEntitySpotsList,
                    Message = "Free spots retrieved successfully.",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<int>>
                {
                    Data = null,
                    Message = $"An error occurred: {ex.Message}",
                    Success = false
                };
            }
        }


        public Task<ServiceResponse<Reservation>> GetReservation(int reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<Reservation>>> GetReservationByEntity(int entityId)
        {
            try { 
            // Calculate the start and end of the next active week
            var today = DateTime.Today;
            var startOfNextWeek = today.AddDays((int)DayOfWeek.Sunday - (int)today.DayOfWeek + 7);
            var endOfNextWeek = startOfNextWeek.AddDays(4);

            // Convert DateTime to DateOnly for comparison
            var startOfNextWeekDateOnly = DateOnly.FromDateTime(startOfNextWeek);
            var endOfNextWeekDateOnly = DateOnly.FromDateTime(endOfNextWeek);

            var reservations = await _dataContext.reservations
                    .Where(r => r.reservation_date >= startOfNextWeekDateOnly
                                && r.reservation_date <= endOfNextWeekDateOnly
                                && r.EntityId == entityId)
                    .Include(e=>e.Entity)
                    .ToListAsync();


            return new ServiceResponse<List<Reservation>> { Data = reservations, Message="okay",Success=true };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Reservation>>
                {
                    Data = null,
                    Message = $"An error occurred: {ex.Message}",
                    Success = false
                };
            }
        }



        public Task<ServiceResponse<List<Reservation>>> GetReservationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<Reservation>>> SearchReservation(string searchText)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<Reservation>> UpdateReservation(Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
