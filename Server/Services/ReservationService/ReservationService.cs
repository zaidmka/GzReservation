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
                    const int maxReservationsPerDayGlobal = 100;
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

                    var today = DateTime.Today;

                    // Adjusting to calculate the start of the current active week
                    var startOfCurrentWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Sunday);
                    if (today.DayOfWeek == DayOfWeek.Sunday)
                    {
                        startOfCurrentWeek = today; // If today is Sunday, it's the start of the current active week
                    }

                    // Calculate the end of the current active week, assuming the week ends on Thursday
                    var endOfCurrentWeek = startOfCurrentWeek.AddDays(4); // Sunday to Thursday is 4 days

                    // Convert DateTime to DateOnly for comparison
                    var startOfCurrentWeekDateOnly = DateOnly.FromDateTime(startOfCurrentWeek);
                    var endOfCurrentWeekDateOnly = DateOnly.FromDateTime(endOfCurrentWeek);


                    // Check if reservation date is within the current active week and not in the past
                    if (reservationDto.reservation_date < DateOnly.FromDateTime(today) || // Check if reservation date is before today
                        reservationDto.reservation_date < startOfCurrentWeekDateOnly ||  // Check if reservation date is before the start of the week
                        reservationDto.reservation_date > endOfCurrentWeekDateOnly)      // Check if reservation date is after the end of the week
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "الحجز مفتوح فقط خلال الاسبوع الحالي ولا يمكن إجراء الحجز لأيام مضت.",
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
                    //check the doc_no is not repeated for the next week reservation
                    int DocNoCount = await _dataContext.reservations
                        .Where(r => r.EntityId == entity.id && r.reservation_date >= startOfCurrentWeekDateOnly && r.reservation_date <= endOfCurrentWeekDateOnly && r.doc_no == reservationDto.doc_no)
                        .CountAsync();
                    if (DocNoCount >= 1)
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "القيد موجود في حجز الاسبوع الحالي",
                            Success = false
                        };
                    }
                    // Retrieve the count of reservations for the same EntityId for the next active week
                    int reservationsCountForWeek = await _dataContext.reservations
                        .Where(r => r.EntityId == entity.id && r.reservation_date >= startOfCurrentWeekDateOnly && r.reservation_date <= endOfCurrentWeekDateOnly)
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
                    //check the active hours
                    var activehour = await _dataContext.activehours
                        .Where(a => a.hour == reservationDto.reservation_hour)
                        .FirstOrDefaultAsync();
                    if (activehour == null)
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "ساعة الحجز غير صحيحة.",
                            Success = false
                        };

                    }
                    int reservationHourCount = await _dataContext.reservations
                        .Where(r => r.reservation_date == reservationDto.reservation_date && r.reservation_hour==reservationDto.reservation_hour)
                        .CountAsync();
                    if (reservationHourCount >= activehour.max)
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "تم ملئ جميع مواعيد الحجز في الساعة المطلوبة.",
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
                            reservation_date = reservationDto.reservation_date,
                            state = reservationDto.state,
                            uuser = reservationDto.uuser,
                            reservation_hour = reservationDto.reservation_hour
                        };

                        _dataContext.reservations.Add(newReservation);
                        await _dataContext.SaveChangesAsync();

                        return new ServiceResponse<Reservation>
                        {
                            Data = newReservation,
                            Message = "تم الحجز بنجاح",
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

        public async Task<ServiceResponse<Reservation>> AddNewReservationNextWeek(ReservationDto reservationDto)
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
                    const int maxReservationsPerDayGlobal = 100;
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
                    //check the doc_no is not repeated for the next week reservation
                    int DocNoCount = await _dataContext.reservations
                        .Where(r => r.EntityId == entity.id && r.reservation_date >= startOfNextWeekDateOnly && r.reservation_date <= endOfNextWeekDateOnly && r.doc_no == reservationDto.doc_no)
                        .CountAsync();
                    if (DocNoCount >= 1)
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "القيد موجود في حجز الاسبوع القادم",
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
                    //check the active hours
                    var activehour = await _dataContext.activehours
                        .Where(a => a.hour == reservationDto.reservation_hour)
                        .FirstOrDefaultAsync();
                    if (activehour == null)
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "ساعة الحجز غير صحيحة.",
                            Success = false
                        };

                    }
                    int reservationHourCount = await _dataContext.reservations
                        .Where(r => r.reservation_date == reservationDto.reservation_date && r.reservation_hour == reservationDto.reservation_hour)
                        .CountAsync();
                    if (reservationHourCount >= activehour.max)
                    {
                        return new ServiceResponse<Reservation>
                        {
                            Data = null,
                            Message = "تم ملئ جميع مواعيد الحجز في الساعة المطلوبة.",
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
                            reservation_date = reservationDto.reservation_date,
                            state = reservationDto.state,
                            uuser = reservationDto.uuser,
                            reservation_hour = reservationDto.reservation_hour
                        };

                        _dataContext.reservations.Add(newReservation);
                        await _dataContext.SaveChangesAsync();

                        return new ServiceResponse<Reservation>
                        {
                            Data = newReservation,
                            Message = "تم الحجز بنجاح",
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
                const int maxReservationsPerDayGlobal = 100;

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
                const int maxReservationsPerDayGlobal = 100;

                // Calculate the start and end of the current week
                var today = DateTime.Today;
                // Adjust to find the start of the current week (most recent Sunday)
                var startOfCurrentWeek = today.AddDays(-(int)today.DayOfWeek);
                var endOfCurrentWeek = startOfCurrentWeek.AddDays(4); // Ends on Thursday

                // Convert DateTime to DateOnly for comparison
                var startOfCurrentWeekDateOnly = DateOnly.FromDateTime(startOfCurrentWeek);
                var endOfCurrentWeekDateOnly = DateOnly.FromDateTime(endOfCurrentWeek);
                // Initialize list to hold free spots for each day
                List<int> freeEntitySpotsList = new List<int>();
                int totalWeekPerEntity = 0;
                // Loop through each day of the next active week
                bool exceededMaxWeekLimit = false;

                for (var date = startOfCurrentWeekDateOnly; date <= endOfCurrentWeekDateOnly; date = date.AddDays(1))
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

        public async Task<ServiceResponse<List<int>>> GetFreeSpotsByEntityNextWeek(int entityId)
        {
            Entity entity = await _dataContext.entities.FindAsync(entityId);

            if (entity == null) { }

            try
            {
                // Define valid weekdays and maximum global reservations per day
                var validWeekdays = new DayOfWeek[] { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday };
                const int maxReservationsPerDayGlobal = 100;

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
            try
            {
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
                        .Include(e => e.Entity)
                        .ToListAsync();


                return new ServiceResponse<List<Reservation>> { Data = reservations, Message = "okay", Success = true };
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

        public async Task<ServiceResponse<List<HourAvailability>>> GetReservationHourByDay(DateOnly reservationDate)
        {


            // Retrieve all active hours
            var activeHours = await _dataContext.activehours
                .OrderBy(ah=>ah.id)
                .ToListAsync();

            // List to store availability for each hour
            var availabilityList = new List<HourAvailability>();
            foreach (var activeHour in activeHours)
            {
                // Find reservations for this hour on the given day
                var reservationsCount = await _dataContext.reservations
                    .CountAsync(r => r.reservation_date == reservationDate &&
                                     r.reservation_hour == activeHour.hour); 

                // Calculate available spots
                int availableSpots = activeHour.max - reservationsCount;

                // Add to availability list
                availabilityList.Add(new HourAvailability
                {
                    Hour = activeHour.hour, // Now using string
                    AvailableSpots = availableSpots
                });
            }

            if(availabilityList.Count > 0)
            {
                return new ServiceResponse<List<HourAvailability>>
                {
                    Data = availabilityList,
                    Success = true,
                    Message = "okay"
                };

            }
            else
            {
                return new ServiceResponse<List<HourAvailability>> { Data = null, Success = false, Message = "Error" };
            }




        }

        public Task<ServiceResponse<List<HourAvailability>>> GetReservationHourByDayNextWeek(DateOnly reservationDate)
        {
            throw new NotImplementedException();
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
