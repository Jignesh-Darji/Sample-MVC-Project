using SampleMVCMasterDetails.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SampleMVCMasterDetails.DataAccess.Concrete
{
    public class EmployeeDataAccess :DataAccessBase
    {
        #region Public
        /// <summary>
        /// Add/ Update Employee 
        /// </summary>
        /// <param name="Employees">The Employee model.</param>
        /// <returns>Employee Object</returns>
        public Employees AddUpdateEmployee(Employees employeeModel)
        {
            var employee = new Employees();

            try
            {
                using (var dbCommand = MyDatabase.GetStoredProcCommand("AddUpdateEmployee"))
                {
                    
                    MyDatabase.AddInParameter(dbCommand, "@EmpId", DbType.Int64, employeeModel.EmpId);
                    MyDatabase.AddInParameter(dbCommand, "@Name", DbType.String, employeeModel.Name);
                    MyDatabase.AddInParameter(dbCommand, "@Designation", DbType.String, employeeModel.Designation);
                    MyDatabase.AddInParameter(dbCommand, "@Email", DbType.String, employeeModel.Email);
                    MyDatabase.AddInParameter(dbCommand, "@Phone", DbType.String, employeeModel.Phone);
                    MyDatabase.AddInParameter(dbCommand, "@Address", DbType.String, employeeModel.Address);
                    MyDatabase.AddInParameter(dbCommand, "@UserName", DbType.String, employeeModel.UserName);
                    MyDatabase.AddInParameter(dbCommand, "@Password", DbType.String, employeeModel.Password);
                    MyDatabase.AddInParameter(dbCommand, "@Age", DbType.Int32, employeeModel.Age);


                    MyDatabase.AddOutParameter(dbCommand, "@p_Error", DbType.Int32, sizeof(Int32));

                    var dataSet = MyDatabase.ExecuteDataSet(dbCommand);

                    // 50008--Error while updating Employee
                    if (MyDatabase.GetParameterValue(dbCommand, "@p_Error").ToString()!="0")
                    {
                        var errorId = Convert.ToInt32(MyDatabase.GetParameterValue(dbCommand, "@p_Error"));
                        employee.ErrorCode = errorId == 50008 ? "Error while updating Employee" : string.Empty;
                    }
                    else if (null != dataSet && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        var row = dataSet.Tables[0].Rows[0];
                        GetEmployeeInfo(employee, row);
                    }
                }
            }
            catch (Exception ex)
            {

                employee.ErrorCode = ex.Message.ToString();
                
            }
            return employee;


        }
        public Employees GetEmployeeById(long EmpId)
        {
            var employee = new Employees();

            try
            {
                using (var dbCommand = MyDatabase.GetStoredProcCommand("GetEmployeeById"))
                {

                    MyDatabase.AddInParameter(dbCommand, "@EmpId", DbType.Int64, EmpId);
                   

                    var dataSet = MyDatabase.ExecuteDataSet(dbCommand);

                     if (null != dataSet && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        var row = dataSet.Tables[0].Rows[0];
                        GetEmployeeInfo(employee, row);
                    }
                }
            }
            catch (Exception ex)
            {

                employee.ErrorCode = ex.Message.ToString();

            }
            return employee;


        }
        public Employees DeleteEmployeeById(long EmpId)
        {
            var employee = new Employees();

            try
            {
                using (var dbCommand = MyDatabase.GetStoredProcCommand("DeleteEmployeeById"))
                {

                    MyDatabase.AddInParameter(dbCommand, "@EmpId", DbType.Int64, EmpId);


                    var dataSet = MyDatabase.ExecuteDataSet(dbCommand);

                    if (null != dataSet && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        var row = dataSet.Tables[0].Rows[0];
                        GetEmployeeInfo(employee, row);
                    }
                }
            }
            catch (Exception ex)
            {

                employee.ErrorCode = ex.Message.ToString();

            }
            return employee;


        }
        public IEnumerable<Employees> GetAllEmployees()
        {
            List<Employees> lstemployee = new List<Employees>();

            try
            {
                using (var dbCommand = MyDatabase.GetStoredProcCommand("GetAllEmployees"))
                {

                   
                    var dataSet = MyDatabase.ExecuteDataSet(dbCommand);

                    if (null != dataSet && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                    {
                        lstemployee = GetAllEmployeeList(dataSet.Tables[0]);
                    }
                }
            }
            catch (Exception ex)
            {

                

            }
            return lstemployee;


        }
        #endregion
        #region Private
        private static void GetEmployeeInfo(Employees employee, DataRow row)
        {
            employee.EmpId = Convert.ToInt64(row["EmpId"].ToString());
            employee.Name = row["Name"].ToString();
            employee.Designation = row["Designation"].ToString();
            employee.Email = row["Email"].ToString();
            employee.Phone = row["Phone"].ToString();
            employee.Address = row["EmpAddress"].ToString();
            employee.UserName = row["UserName"].ToString();
            employee.Password = row["UPassword"].ToString();
            employee.Age = Convert.ToInt32(row["Age"].ToString());
        }
        private static List<Employees> GetAllEmployeeList(DataTable dataTable)
        {
            var lstEmployees = new List<Employees>();
            lstEmployees = dataTable.AsEnumerable().Select(result => new Employees
            {
                EmpId = result.Field<long>("EmpId"),
                Name = result.Field<string>("Name"),
                Designation = result.Field<string>("Designation"),
                Email = result.Field<string>("Email"),
                Phone = result.Field<string>("Phone"),
                Address = result.Field<string>("EmpAddress"),
                UserName = result.Field<string>("UserName"),
                Password = result.Field<string>("UPassword"),
                Age = result.Field<Int32>("Age")
            }).ToList();
            return lstEmployees;
        }
        #endregion
    }
}