using System.Net.Http.Json;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Globalization;
using System.Runtime.CompilerServices;

class Program
{
  static string host = "localhost";
  static string database = "kalis";
  static string user = "root";
  static string password = "Welcome1!";
  static int port = 3306;
  static string connectionString = $"Server={host};Database={database};User={user};Password={password};Port={port};";

  static void Main(string[] args)
  {
    try
    {
      // SP_U_REGISTER 프로시저 호출 예시
      var registerParameters = new Dictionary<string, object>
            {
                {"id", 1021},
                {"name", "kq_test2"},
                {"companyName", "fov2"},
                {"job1", "1"},
                {"job2", "1"},
                {"lang1", "1"},
                {"lang2", "1"},
                {"groupAmount", 1},
                {"personalOrGroup", "1"},
                {"seatNo", 1},
                {"phoneNo", "01012341234"},
                {"email", "test@test.com"}
            };

      // SP_W_USERS 프로시저 호출 예시
      var findWebUserParameters = new Dictionary<string, object>
            {
                {"page", 1},
                {"perPage", 10},
                {"sortKey", "u.id"},
                {"sortDir", true},
                {"personalOrGroup", "personal"},
                {"queries", null}
            };

      // SP_W_USERS_BY 프로시저 호출 예시
      var findWebUserByParameters = new Dictionary<string, object>
      {
        {"userId", 19}
      };

      // SP_C_MISSIONS 프로시저 호출 예시
      var findMissionsParameters = new Dictionary<string, object> { };

      // SP_W_CONTENTS_BY_MTYPE 프로시저 호출 예시
      var findContentsByMtypeParameters = new Dictionary<string, object> {
        {"page", 1},
        {"perPage", 10},
        {"mType", "105"}
      };

      // SP_C_MISSION_START 프로시저 호출 예시
      var findMissionStartParameters = new Dictionary<string, object> {
        {"mType", "105"},
        {"userId", 19},
        {"machineType", "1"}
      };

      // SP_C_MISSION_END 프로시저 호출 예시
      var findMissionEndParameters = new Dictionary<string, object> {
        {"mType", "105"},
        {"logId", 80},
        {"findRisk", 1},
        {"action", 1},
        {"wrongClick", 0},
        {"hint", 0},
        {"totalRisk", 5}
      };

      // SP_M_UPDATE 프로시저 호출 예시
      var missionUpdateParameters = new Dictionary<string, object> {
        {"id", 31},
        {"condition", "on"},
        {"accId", "collapse"}
      };

      var registerResult = ExecuteRegister("SP_U_REGISTER", registerParameters, true);
      Output(registerResult);
      Console.WriteLine("--------------------------------");

      // var findWebUserResult = ExecuteFindWebUser("SP_W_USERS", findWebUserParameters, true);
      // Output(findWebUserResult);
      // Console.WriteLine("--------------------------------");

      // var findWebUserByResult = ExecuteFindWebUserBy("SP_W_USERS_BY", findWebUserByParameters);
      // Output(findWebUserByResult);

      // var findMissionsResult = ExecuteFindMissions("SP_C_MISSIONS", findMissionsParameters);
      // Output(findMissionsResult);

      // var findContentsByMtypeResult = ExecuteFindContentsByMtype("SP_W_CONTENTS_BY_MTYPE", findContentsByMtypeParameters);
      // Output(findContentsByMtypeResult);

      // var findMissionStartResult = ExecuteFindMissionStart("SP_C_MISSION_START", findMissionStartParameters);
      // Output(findMissionStartResult);

      // var findMissionEndResult = ExecuteFindMissionEnd("SP_C_MISSION_END", findMissionEndParameters);
      // Output(findMissionEndResult);

      // var missionUpdateResult = ExecuteMissionUpdate("SP_M_UPDATE", missionUpdateParameters);
      // Output(missionUpdateResult);
    }
    catch (Exception ex)
    {
      Console.WriteLine("오류: " + ex.Message);
    }
  }

  static List<Dictionary<string, object>> ExecuteMissionUpdate(string procedureName, Dictionary<string, object> parameters, bool returnSingleRow = false)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters, returnSingleRow);
  }

  static List<Dictionary<string, object>> ExecuteFindMissionEnd(string procedureName, Dictionary<string, object> parameters, bool returnSingleRow = false)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters, returnSingleRow);
  }

  static List<Dictionary<string, object>> ExecuteFindMissionStart(string procedureName, Dictionary<string, object> parameters, bool returnSingleRow = false)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters, returnSingleRow);
  }

  static List<Dictionary<string, object>> ExecuteFindContentsByMtype(string procedureName, Dictionary<string, object> parameters, bool returnSingleRow = false)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters, returnSingleRow);
  }

  static List<Dictionary<string, object>> ExecuteFindMissions(string procedureName, Dictionary<string, object> parameters, bool returnSingleRow = false)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters, returnSingleRow);
  }

  static void Output(List<Dictionary<string, object>> messages)
  {
    foreach (var message in messages)
    {
      // 객체 프로퍼티-값 출력
      foreach (var property in message.Keys)
      {
        Console.WriteLine($"{property}: {message[property]}");
      }
      Console.WriteLine(">============================<");
    }
  }

  static List<Dictionary<string, object>> ExecuteFindWebUserBy(string procedureName, Dictionary<string, object> parameters, bool returnSingleRow = false)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters, returnSingleRow);
  }

  static List<Dictionary<string, object>> ExecuteFindWebUser(string procedureName, Dictionary<string, object> parameters, bool returnSingleRow = false)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters, returnSingleRow);
  }

  static List<Dictionary<string, object>> ExecuteRegister(string procedureName, Dictionary<string, object> parameters, bool returnSingleRow = false)
  {
    return ExecuteStoredProcedureInternal(procedureName, parameters, returnSingleRow);
  }

  static Dictionary<string, object> ExecuteReaderAndSerializeSingle(MySqlCommand cmd)
  {
    using (MySqlDataReader reader = cmd.ExecuteReader())
    {
      if (reader.Read())
      {
        var row = new Dictionary<string, object>();
        for (int i = 0; i < reader.FieldCount; i++)
        {
          row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
        }
        return row;
      }
      return null;
    }
  }

  static List<Dictionary<string, object>> ExecuteStoredProcedureInternal(string procedureName, Dictionary<string, object> parameters, bool returnSingleRow = false)
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

        if (returnSingleRow)
        {
          var singleRow = ExecuteReaderAndSerializeSingle(cmd);
          return singleRow != null ? new List<Dictionary<string, object>> { singleRow } : new List<Dictionary<string, object>>();
        }
        else
        {
          return ExecuteReaderAndSerialize(cmd);
        }
      }
    }
  }

  static List<Dictionary<string, object>> ExecuteReaderAndSerialize(MySqlCommand cmd)
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
      return result;
    }
  }
}
