using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionsManager
{
    public class Users
    {
        public static string _connectionString;

        public Users(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(SharedModels.Models.UserModel user)
        {            
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
	            try
	            {
                    int insertedID = 0;
                    using (SqlCommand command = new SqlCommand("INSERT INTO USERS OUTPUT INSERTED.ID VALUES(@name, @email, @password, @dob, @country, @avatar)", sqlConnection))
	                {
                        command.Parameters.Add(new SqlParameter("name", user.Name));
                        command.Parameters.Add(new SqlParameter("email", user.Email));
                        command.Parameters.Add(new SqlParameter("password", user.Password));
                        command.Parameters.Add(new SqlParameter("dob", user.BirthDay));
                        command.Parameters.Add(new SqlParameter("country", user.Country));
                        command.Parameters.Add(new SqlParameter("avatar", DBNull.Value));
                        insertedID = (int)command.ExecuteScalar();
	                }
                    /*if (insertedID > 0)
                    {
                        AddRegister(insertedID, sessionID, user.IP);
                    }*/
                    
	            }
                catch (Exception e)
	            {
	                Console.WriteLine("Can't insert de new User");
                    throw new Exception(@"Can't insert a new User", e);
	            }                
            }
        }

        public void AddRegister(int idUser, String sessionID, string userIP)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
	            try
	            {
                    using (SqlCommand command = new SqlCommand("INSERT INTO REGISTER VALUES(@sessionID, @idUser, @dLogin, @dLogout, @ip, @connect)", sqlConnection))
                    {
                        command.Parameters.Add(new SqlParameter("sessionID", sessionID));
                        command.Parameters.Add(new SqlParameter("idUser", idUser));
                        command.Parameters.Add(new SqlParameter("dLogin", DateTime.Now));
                        command.Parameters.Add(new SqlParameter("dLogout", DBNull.Value));
                        command.Parameters.Add(new SqlParameter("ip", userIP));
                        command.Parameters.Add(new SqlParameter("connect", 1));
                        command.ExecuteNonQuery();                        
                    }
                }
                catch (Exception e)
	            {
	                Console.WriteLine("Can't insert a new Register");
                    throw new Exception(@"Can't insert a new Register", e);
	            }                
            }
        }

        public void updateRegister(string sessionID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("UPDATE REGISTER SET DLOGOUT=@dLogout, CONNECT=@connect WHERE SESSIONID=@sessionID", sqlConnection))
                    {
                        command.Parameters.Add(new SqlParameter("dLogout", DateTime.Now));
                        command.Parameters.Add(new SqlParameter("connect", 1));
                        command.Parameters.Add(new SqlParameter("sessionID", sessionID));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Can't insert a new Register");
                    throw new Exception(@"Can't insert a new Register", e);
                }                
            }
        }

        public int existUser(String email, String password)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = "SELECT ID FROM USERS WHERE EMAIL='" + email + "' AND PASSWORD='" + password + "'";
                try
                {
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            int id = 0;
                            if (reader.Read())
                            {
                                id = (int)reader["ID"];
                            }
                            reader.Close();
                            return id;
                        }                        
                        return 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Problem access to DB");
                    throw new Exception(@"Problem access to DB", e);
                }
            }
        }

        public List<SharedModels.Models.ListUsers> getListUsers() 
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query =  "SELECT u.ID, NAME, COUNTRY, CONNECT, DLOGIN, IP, TotalCount " +
                                "FROM " + 
                                "(SELECT COUNT(IDUSER) TotalCount, IDUSER FROM REGISTER GROUP BY IDUSER) c," +
                                "(SELECT IDUSER, MAX(DLOGIN) as maxDlogin FROM REGISTER GROUP BY IDUSER) maxr, " +
                                "USERS u INNER JOIN REGISTER r ON u.ID=r.IDUSER " +
                                "WHERE c.IDUSER = u.ID AND u.ID = r.IDUSER AND maxr.IDUSER = r.IDUSER AND maxr.maxDlogin = r.DLOGIN";
                try
                {
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var model = new List<SharedModels.Models.ListUsers>();
                        if (reader != null && reader.HasRows)
                        {
                            
                            while(reader.Read())
                            {
                                var listUsers = new SharedModels.Models.ListUsers();
                                listUsers.Name = (String) reader["NAME"];
                                listUsers.Country = (String) reader["COUNTRY"];
                                listUsers.Online = (Boolean) reader["CONNECT"];
                                listUsers.Dlogin = (DateTime) reader["DLOGIN"];
                                listUsers.IP = (reader["IP"] == DBNull.Value) ? string.Empty : (String)reader["IP"];
                                listUsers.TotalLogins = (Int32) reader["TotalCount"];
                                model.Add(listUsers);
                            }                        
                        }
                        reader.Close();                        
                        return model;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Problem access to DB");
                    throw new Exception(@"Problem access to DB", e);
                }                
            }
        }
    }

}
