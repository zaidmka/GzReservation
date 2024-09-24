namespace GzReservation.Server.Services.AnnouncementService
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly DataContext _context;

        public AnnouncementService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Dbmessage>> AddDbMessagesAsync(Dbmessage dbmessage)
        {
            var response = new ServiceResponse<Dbmessage>();

            try
            {
                _context.messages.Add(dbmessage);
                await _context.SaveChangesAsync();
                response.Data = dbmessage;
                response.Message = "Message added successfully!";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<Dbmessage>> DeleteDbMessageAsync(int dbmessageId)
        {
            var response = new ServiceResponse<Dbmessage>();

            var dbmessage = await _context.messages
                .Where(m => m.id == dbmessageId)
                .FirstOrDefaultAsync();
            if (dbmessage == null)
            {
                return new ServiceResponse<Dbmessage> { Data = null, Message = "Message not found", Success = false };
            }
            dbmessage.visible = false;
            await _context.SaveChangesAsync();
            response.Data= dbmessage;
            response.Success= true;
            response.Message = "Message deleted Successfully";
            return response;



        }

        public async Task<ServiceResponse<List<Dbmessage>>> GetActiveDbMessagesListAsync()
        {
            var response = new ServiceResponse<List<Dbmessage>>();

            try
            {
                var messages = await _context.messages.Where(m=>m.visible == true).ToListAsync();
                response.Data = messages;
                response.Success = true;
                response.Message = "Messages retrieved successfully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<List<Dbmessage>>> GetDbMessagesListAsync()
        {
            var response = new ServiceResponse<List<Dbmessage>>();

            try
            {
                var messages = await _context.messages.ToListAsync();
                response.Data = messages;
                response.Success = true;
                response.Message = "Messages retrieved successfully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<Dbmessage>> UpdateDbMessageAsync(Dbmessage dbmessage)
        {
            var response = new ServiceResponse<Dbmessage>();

            if (dbmessage == null)
            {
                response.Success = false;
                response.Message = "Message not found.";
                return response;
            }

            try
            {
                _context.messages.Update(dbmessage);
                await _context.SaveChangesAsync();
                response.Data = dbmessage;
                response.Success = true;
                response.Message = "Message updated successfully!";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }

}
