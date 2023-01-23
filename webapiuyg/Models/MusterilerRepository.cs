using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace webapiuyg.Models
{
    public class MusterilerRepository : IMusterilerRepository
    {
        public IConfiguration Configuration { get; }
        public string connectionString;
        private readonly ILogger<MusterilerRepository> _logger;
        public MusterilerRepository(IConfiguration configuration, ILogger<MusterilerRepository> logger)
        {
            this.Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;
        }
        public Musteriler AddMusteri(Musteriler musteriler)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spInsertIntoMusteriler]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@AdSoyad", musteriler.AdSoyad);
                    cmd.Parameters.AddWithValue("@Adres",musteriler.Adres);
                    cmd.Parameters.AddWithValue("@Telefon", musteriler.Telefon);
                    cmd.Parameters.AddWithValue("@Email",musteriler.Email);
                    
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "hata var addmusteriler methodunda");
                    musteriler = null;
                }

            }
                    
            return musteriler = null;
        }

        public void DeleteMusteri(int? id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spDeleteMusteriler]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                   
                    _logger.LogError(ex, "Hata var DeleteMusteriler() methodunda");

                }

            }
        }

        public IEnumerable<Musteriler> GetAllMusteriler()
        {
            List<Musteriler> musterilers = new List<Musteriler>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectMusteriler]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Musteriler musteri = new Musteriler();
                        musteri.Id = Convert.ToInt32(rdr["Id"]);
                        musteri.AdSoyad = rdr["AdSoyad"].ToString();
                        musteri.Adres = rdr["Adres"].ToString();
                        musteri.Telefon = rdr["Telefon"].ToString();
                        musteri.Email = rdr["Email"].ToString();
                        musterilers.Add(musteri);   
                  

                    }
                    rdr.Close();
                }
                catch (Exception ex)
                {
                   
                    _logger.LogError(ex, "hata var GetAllMusteriler methodunda");
                    musterilers = null;
                }
            }
            return musterilers;
        }

        public Musteriler GetMusteriById(int id)
        {
            Musteriler musteriler = new Musteriler();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectMusterilerById]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        musteriler.Id = id;
                        musteriler.AdSoyad = rdr["AdSoyad"].ToString();
                        musteriler.Adres = rdr["Adres"].ToString();
                        musteriler.Telefon = rdr["Telefon"].ToString();
                        musteriler.Email = rdr["Email"].ToString();
                    }


                    rdr.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "hata var GetMusterilerById() var");
                    musteriler = null;
                }
            }
            return musteriler;
        }

        public Musteriler UpdateMusteri(Musteriler musteriler)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spUpdateMusteriler]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", musteriler.Id);
                    cmd.Parameters.AddWithValue("@AdSoyad", musteriler.AdSoyad);
                    cmd.Parameters.AddWithValue("@Adres", musteriler.Adres);
                    cmd.Parameters.AddWithValue("@Telefon", musteriler.Telefon);
                    cmd.Parameters.AddWithValue("@Email", musteriler.Email);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at UpdateCustomer() :(");
                    musteriler = null;
                }
            }

            return musteriler;
        }
    }
}
