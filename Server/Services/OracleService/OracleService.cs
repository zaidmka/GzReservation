using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

namespace GzReservation.Server.Services.OracleService
{
    public class OracleService : IOracleService
    {
        private readonly string _connectionString;

        public OracleService() // Corrected constructor name
        {
            _connectionString = "User Id=cio_ahmed_asp;Password=5555;Data Source=10.10.34.5:1521/orcl";
        }
        public async Task<ServiceResponse<Form>> GetDataAsync(int docNo)
        {
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        await con.OpenAsync();
                        cmd.BindByName = true;

                        // Replace with your actual SQL query
                        cmd.CommandText = "SELECT ZONE_NUMBER, TOWN, STREET_NO, PRIVATE_PHONE_NO, OFFICE_PHONE_NO, NATIONALITY_NAME, NATIONALISM_NAME, NAME1, MOTHER_NAME, MARITAL_STATUS, ID_NUMBER, HOUSE_NO, GOVERNORATE, ENTITY_NAME, DOC_NO, DATE_OF_BIRTH, BAR_CODE, BADGE_COLOUR FROM APPL_VIEWB WHERE DOC_NO=" + docNo + "";
                        using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Assuming 'YourDataType' is the type of your data model
                                var data = new Form
                                {
                                    doc_no = Convert.ToInt32(reader["DOC_NO"]),
                                    name = reader["NAME1"].ToString(),
                                    entity = reader["ENTITY_NAME"].ToString(),
                                    birthdate = DateOnly.FromDateTime(Convert.ToDateTime(reader["DATE_OF_BIRTH"])),
                                    state = reader["MARITAL_STATUS"].ToString(),
                                    nationalism = reader["NATIONALISM_NAME"].ToString(),
                                    religion = reader["NATIONALITY_NAME"].ToString(),
                                    //phone1 = Convert.ToInt32(reader["PRIVATE_PHONE_NO"]),
                                    //phone2 = Convert.ToInt32(reader["OFFICE_PHONE_NO"]),

                                    //Mapping additional fields from the SQL query to the Form model
                                    mother_name = reader["MOTHER_NAME"].ToString(),
                                    // Assuming ID_NUMBER maps to a property in your Form model
                                    //id_number = reader["ID_NUMBER"].ToString(),
                                    // Continue mapping other fields...
                                    place_city = reader["TOWN"].ToString(),
                                    place_zuqaq = reader["STREET_NO"].ToString(),
                                    place_dar = reader["HOUSE_NO"].ToString(),
                                    place_govern = reader["GOVERNORATE"].ToString(),
                                    // ... and so on for the remaining fields
                                    bagde_color = reader["BADGE_COLOUR"].ToString(),
                                    // If you have corresponding fields in your Form model for these:
                                    place_mhala = reader["ZONE_NUMBER"].ToString(),

                                };
                                Console.WriteLine(data);

                                return new ServiceResponse<Form>
                                {
                                    Data = data,
                                    Success = true,
                                    Message = "okay"
                                };
                            }
                        }
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine($"Oracle error code: {ex.Number}, Message: {ex.Message}");
                        return new ServiceResponse<Form>
                        {
                            Success = false,
                            Message = $"Oracle error: {ex.Message}"
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"General error: {ex.Message}");
                        return new ServiceResponse<Form>
                        {
                            Success = false,
                            Message = $"Error: {ex.Message}"
                        };
                    }
                }
            }
            return new ServiceResponse<Form>
            {
                Success = false,
                Message = "No data found"
            };
        }

        public string GetMessage()
        {
            return "Hello from TestService";
        }

        public async Task<ServiceResponse<List<PreReservation>>> GetRecoredsByEntity(int entityCode)
        {
            string formattedCode;

            if (entityCode >= 1 && entityCode <= 9)
            {
                formattedCode = entityCode.ToString("D2"); // Formats the number as a two-digit string with leading zeros if necessary
            }
            else
            {
                formattedCode = entityCode.ToString(); // Just convert to string if it's not between 1 and 9
            }
            Console.WriteLine(formattedCode);
            using (OracleConnection con = new OracleConnection(_connectionString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    List<PreReservation> preReservations = new List<PreReservation>();

                    try
                    {
                        await con.OpenAsync();
                        cmd.BindByName = true;

                        // Replace with your actual SQL query
                        cmd.CommandText = "SELECT DOC_NO, NAME, ENTITY_CODE,MOTHER_NAME,TRANSACTION_DATE FROM APPL_VIEWBZ WHERE ENTITY_CODE = :EntityCode and TRANSACTION_DATE > to_date('01/01/2024','dd/MM/yyyy') order by DOC_NO desc";
                        cmd.Parameters.Add("EntityCode", OracleDbType.Varchar2).Value = formattedCode;
                        //cmd.Parameters.Add("trans", OracleDbType.Varchar2).Value = "مقابلة امنية";
                        //cmd.Parameters.Add("trans1", OracleDbType.Varchar2).Value = "اجتياز مقابلة";

                        using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync()) // Loop through all the records
                            {
                                var preReservation = new PreReservation
                                {
                                    // Assuming you have properties like these in your PreReservation model
                                    full_name = reader["NAME"].ToString(),
                                    mother_name = reader["MOTHER_NAME"].ToString(),
                                    amn_date = DateOnly.FromDateTime((DateTime)reader["TRANSACTION_DATE"]),

                                    doc_no = Convert.ToInt32(reader["DOC_NO"]),
                                    EntityId = Convert.ToInt32(reader["ENTITY_CODE"]),

                                };

                                preReservations.Add(preReservation);
                            }
                        }
                        // Check if any records were added
                        if (preReservations.Count > 0)
                        {
                            return new ServiceResponse<List<PreReservation>>
                            {
                                Data = preReservations,
                                Success = true,
                                Message = "Data retrieved successfully"
                            };
                        }
                        else
                        {
                            return new ServiceResponse<List<PreReservation>>
                            {
                                Data = null,
                                Success = false,
                                Message = "No data found"
                            };
                        }
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine($"Oracle error code: {ex.Number}, Message: {ex.Message}");
                        return new ServiceResponse<List<PreReservation>>
                        {
                            Data = null,
                            Success = false,
                            Message = $"Oracle error: {ex.Message}"
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"General error: {ex.Message}");
                        return new ServiceResponse<List<PreReservation>>
                        {
                            Success = false,
                            Message = $"Error: {ex.Message}"
                        };
                    }
                }
            }
            return new ServiceResponse<List<PreReservation>>
            {
                Success = false,
                Message = "No data found"
            };
        }
    }
}
