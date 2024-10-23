using System.Net.Http.Json;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Globalization;

class Program
{
  static string connectionString = "Server=localhost;Database=kalis;User=root;Password=1234;";

  static void Main(string[] args)
  {
    try
    {
      // SP_U_REGISTER 프로시저 호출 예시
      var registerParameters = new Dictionary<string, object>
            {
                {"name", "kq_test2"},
                {"company_name", "fov2"},
                {"job1", "1"},
                {"job2", "1"},
                {"lang1", "1"},
                {"lang2", "1"},
                {"group_amount", 1},
                {"personal_or_group", "1"},
                {"seat_no", 1},
                {"phone_no", "01012341234"},
                {"email", "test@test.com"}
            };

      var findWebUserParameters = new Dictionary<string, object>
            {
                {"page", 1},
                {"perPage",10},
                {"sortKey","u.id"},
                {"sortDir",true},
                {"personalOrGroup","personal"},
                {"queries",null}
            };

      var findWebUserByParameters = new Dictionary<string, object>
      {
        {"userId", 19}
      };

      // var registerResult = ExecuteStoredProcedure("SP_U_REGISTER", registerParameters);
      // Console.WriteLine(registerResult);

      // var findWebUserResult = ExecuteFindWebUser("SP_W_USERS", findWebUserParameters);
      // Console.WriteLine(findWebUserResult);

      var findWebUserByResult = ExecuteFindWebUserBy("SP_W_USERS_BY", findWebUserByParameters);
      Console.WriteLine(findWebUserByResult);
    }
    catch (Exception ex)
    {
      Console.WriteLine("오류: " + ex.Message);
    }
  }

  static string ExecuteFindWebUserBy(string procedureName, Dictionary<string, object> parameters)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters);
  }

  static string ExecuteFindWebUser(string procedureName, Dictionary<string, object> parameters)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters);
  }

  static string ExecuteStoredProcedure(string procedureName, Dictionary<string, object> parameters)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters);
  }

  static string ExecuteStoredProcedureInternal(string procedureName, Dictionary<string, object> parameters)
  {
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
      connection.Open();
      Console.WriteLine("MySQL 연결이 성공적으로 열렸습니다.");

      using (MySqlCommand cmd = new MySqlCommand(procedureName, connection))
      {
        cmd.CommandType = CommandType.StoredProcedure;

        foreach (var param in parameters)
        {
          cmd.Parameters.AddWithValue(param.Key, param.Value);
        }

        return ExecuteReaderAndSerialize(cmd);
      }
    }
  }

  static string ExecuteReaderAndSerialize(MySqlCommand cmd)
  {
    using (MySqlDataReader reader = cmd.ExecuteReader())
    {
      var result = new List<Dictionary<string, object>>();
      while (reader.Read())
      {
        var row = new Dictionary<string, object>();
        for (int i = 0; i < reader.FieldCount; i++)
        {
          row[reader.GetName(i)] = reader.IsDBNull(i) ? (object)null : reader.GetValue(i);
        }
        result.Add(row);
      }
      return JsonSerializer.Serialize(result);
    }
  }
}
