using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace webapiuyg.Models
{
    public class HizmetlerRepository : IHizmetlerRepository
    {
        public IConfiguration Configuration { get; }
        public string connectionString;
        private readonly ILogger<HizmetlerRepository> _logger;
        public HizmetlerRepository(IConfiguration configuration, ILogger<HizmetlerRepository> logger)
        {
            this.Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            _logger = logger;
        }


        public IEnumerable<Hizmetler> GetAllHizmet()
        {
            List<Hizmetler> hizmetlers = new List<Hizmetler>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spSelectHizmetler]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Hizmetler hizmetler = new Hizmetler();
                        hizmetler.Id = Convert.ToInt32(rdr["Id"]);
                        hizmetler.MusteriId = Convert.ToInt32(rdr["MusteriId"]);
                        hizmetler.Aciklama = rdr["Aciklama"].ToString();
                        hizmetler.Fiyat = Convert.ToDecimal(rdr["Fiyat"]);
                        hizmetlers.Add(hizmetler);

                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                  
                    _logger.LogError(ex, "Error at GetAllOrders() :(");
                    hizmetlers = null;
                }
            }
            return hizmetlers;
        }

        public Hizmetler AddHizmet(Hizmetler hizmetler)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    _logger.LogInformation("Could break here!!");
                    SqlCommand cmd = new SqlCommand("[dbo].[spInsertIntoHizmetler]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@MusteriId", hizmetler.MusteriId);
                    cmd.Parameters.AddWithValue("@Aciklama", hizmetler.Aciklama);
                    cmd.Parameters.AddWithValue("@Fiyat", hizmetler.Fiyat);

                    // cmd.Parameters.AddWithValue("@ret", ParameterDirection.Output);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Hata  Addhizmetler() methodunda");
                    hizmetler = null;
                }
            }
            return hizmetler;
        }

        public void DeleteHizmet(int? id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spDeleteHizmetler]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Hata var DeleteHizmetler() ");

                }
            }

        }
        public Hizmetler UpdateHizmet(Hizmetler hizmetler)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[spUpdateHizmetler]", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@Id", hizmetler.Id);
                    cmd.Parameters.AddWithValue("@MusteriId", hizmetler.MusteriId);
                    cmd.Parameters.AddWithValue("@Aciklama", hizmetler.Aciklama);
                    cmd.Parameters.AddWithValue("@Fiyat", hizmetler.Fiyat);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Error at UpdateOrder() :(");
                    hizmetler = null;

                }
            }

            return hizmetler;
        }

        public Hizmetler GetHizmetById(int id)
        {
            Hizmetler hizmetler = new Hizmetler();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("dbo.spSelectHizmetlerById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        hizmetler.Id = Convert.ToInt32(rdr["Id"]);
                        hizmetler.MusteriId = Convert.ToInt32(rdr["MusteriId"]);
                        hizmetler.Aciklama = rdr["Aciklama"].ToString();
                        hizmetler.Fiyat = Convert.ToDecimal(rdr["Fiyat"]);
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    //ex.Message.ToString();
                    _logger.LogError(ex, "Hata Var GetHizmetlerById() methodunda ");

                }

            }
            return hizmetler;
        }
    }
}
