using GzReservation.Server.Data;
using GzReservation.Shared;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GzReservation.Server.Services.FormService
{
    public class FormService : IFormService
    {
        private readonly DataContext _dataContext;

        public FormService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<Form>> AddNewForm(Form form)
        {
            try
            {
                _dataContext.forms.Add(form);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Form> { Data = form, Message = "success", Success = true };
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions, e.g., unique constraint violations, etc.
                // You can log the exception details for debugging purposes.
                // Log the error using a logging framework like Serilog or write to a log file.

                return new ServiceResponse<Form> { Data = null, Message = "Database error" + ex, Success = false };
            }
            catch (Exception ex)
            {
                // Handle other generic exceptions that might occur during the operation.
                // You can log the exception details for debugging purposes.
                // Log the error using a logging framework like Serilog or write to a log file.

                return new ServiceResponse<Form> { Data = null, Message = "An error occurred", Success = false };
            }
        }

        public async Task<ServiceResponse<Form>> DeleteForm(int formId)
        {
            var dbForm = await _dataContext.forms.FirstOrDefaultAsync(f => f.id == formId);
            if(dbForm == null)
            {
                return new ServiceResponse<Form>
                {
                    Data = null,
                    Success = false,
                    Message = "Form not found!"
                };
            }
            dbForm.deleted = true;
            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<Form> { Message = "form deleted!", Success = true };
        }

        public async Task<ServiceResponse<Form>> GetForm(int formId)
        {
            var form = await _dataContext.forms
                .Where(f => f.deleted == false && f.id == formId)
                .FirstOrDefaultAsync();

            if (form != null)
            {
                var response = new ServiceResponse<Form>
                {
                    Data = form,
                    Message = "OK",
                    Success = true
                };

                return response;
            }
            else
            {
                
                var response = new ServiceResponse<Form>
                {
                    Data = null,
                    Message = "Form not found",
                    Success = false
                };

                return response;
            }
        }

        public async Task<ServiceResponse<List<Form>>> GetFormsAsync()
        {
            var response = new ServiceResponse<List<Form>>();

            var forms = await _dataContext.forms
                .Where(f => !f.deleted)
                .Take(50) // Take only the top 50 forms
                .ToListAsync();

            response.Data = forms;
            response.Message = "okay";
            response.Success = true;

            return response;
        }


        public async Task<ServiceResponse<List<Form>>> SearchForm(string searchText)
        {
            var response = new ServiceResponse<List<Form>>();

            int? docNo = int.TryParse(searchText, out int parsedDocNo) ? (int?)parsedDocNo : null;

            var forms = await _dataContext.forms
                .Where(f => !f.deleted && (f.name.Contains(searchText) || f.doc_no == docNo))
                .ToListAsync();

            response.Data = forms;
            response.Message = "okay";
            response.Success = true;

            return response;
        }



        public async Task<ServiceResponse<Form>> UpdateForm(Form form)
        {
            var dbForm = await _dataContext.forms.FirstOrDefaultAsync(f => f.id == form.id);
            if (dbForm == null)
            {
                return new ServiceResponse<Form> { Success = false, Message = "Form not found!" };
            }
            dbForm.did = form.did;
            dbForm.doc_no = form.doc_no;
            dbForm.name = form.name;
            dbForm.entity = form.entity;
            dbForm.father_work = form.father_work;
            dbForm.mother_name = form.mother_name;
            dbForm.mother_work = form.mother_work;
            dbForm.wife_name = form.wife_name;
            dbForm.wife_work = form.wife_work;
            dbForm.bagde_color = form.bagde_color;
            dbForm.actiondate = form.actiondate;
            dbForm.username = form.username;
            dbForm.info_book = form.info_book;
            dbForm.seq = form.seq;
            dbForm.review_date = form.review_date;
            dbForm.birthdate = form.birthdate;
            dbForm.state = form.state;
            dbForm.nationalism = form.nationalism;
            dbForm.religion = form.religion;
            dbForm.place_govern = form.place_govern;
            dbForm.place_city = form.place_city;
            dbForm.place_mhala = form.place_mhala;
            dbForm.place_zuqaq = form.place_zuqaq;
            dbForm.place_dar = form.place_dar;
            dbForm.place_point = form.place_point;
            dbForm.phone1 = form.phone1;
            dbForm.phone2 = form.phone2;
            dbForm.work_place = form.work_place;
            dbForm.work_place2 = form.work_place2;
            dbForm.study = form.study;
            dbForm.grad_year = form.grad_year;
            dbForm.passport_no = form.passport_no;
            dbForm.old_place = form.old_place;
            dbForm.a1 = form.a1;
            dbForm.a2 = form.a2;
            dbForm.a3 = form.a3;
            dbForm.a4 = form.a4;
            dbForm.a5 = form.a5;
            dbForm.a6 = form.a6;
            dbForm.a7 = form.a7;
            dbForm.a8 = form.a8;
            dbForm.a9 = form.a9;
            dbForm.a10 = form.a10;
            dbForm.a11 = form.a11;
            dbForm.a12 = form.a12;
            dbForm.a13 = form.a13;
            dbForm.a14 = form.a14;
            dbForm.a15 = form.a15;
            dbForm.a16 = form.a16;
            dbForm.a17 = form.a17;
            dbForm.deleted = form.deleted;

            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<Form>
            {
                Data = dbForm = await _dataContext.forms.FirstOrDefaultAsync(f => f.id == form.id),
                Message = "OKAY",
                Success = true
            };
        }
        }
    }
