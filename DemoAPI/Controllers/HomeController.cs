using Demo.DataAccess.Models;
using Demo.DataAccess.Models.Mapping;
using Demo.DataAccess.SPModels;
using DemoAPI.ViewModels;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DemoAPI.Controllers
{
    public class HomeController : Controller
    {
        private DemoContext _dbContext = new DemoContext();

        public ActionResult Index()
        {
            DataModel model = new DataModel();
            model.ProductList = new SelectList(_dbContext.Products.ToList(), "id", "name");
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    try
                    {
                        var dt = new DataTable();

                        // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                        // to get started. This is how we avoid dependencies on ACE or Interop:
                        Stream stream = upload.InputStream;

                        // We return the interface, so that
                        IExcelDataReader reader = null;


                        if (upload.FileName.EndsWith(".xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (upload.FileName.EndsWith(".xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else
                        {
                            ModelState.AddModelError("File", "This file format is not supported");
                            TempData["message"] = "File format incorrect";
                            return RedirectToAction("Index");
                        }
                        var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });
                        dt.Columns.Add("orderid");
                        dt.Columns.Add("pinTypeId");
                        dt.Columns.Add("paymenttype");
                        dt.Columns.Add("customerName");
                        dt.Columns.Add("fullAddress");
                        dt.Columns.Add("orderdate");
                        dt.Columns.Add("price");
                        dt.Columns.Add("quantity");
                        dt.Columns.Add("productname");



                        for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                        {


                            var exceldata = new ExcelDataViewModel();
                            var date = new DateTime();
                            try
                            {
                                exceldata.orderid = result.Tables[0].Rows[i][0] is DBNull ? 0 : Convert.ToInt32(result.Tables[0].Rows[i][0]);
                                exceldata.pinTypeId = result.Tables[0].Rows[i][1] is DBNull ? 0 : Convert.ToInt32(result.Tables[0].Rows[i][1]);
                                exceldata.paymenttype = result.Tables[0].Rows[i][2] is DBNull ? 0 : Convert.ToInt32(result.Tables[0].Rows[i][2]);
                                exceldata.customerName = result.Tables[0].Rows[i][3] is DBNull ? null : Convert.ToString(result.Tables[0].Rows[i][3]);
                                exceldata.fullAddress = result.Tables[0].Rows[i][4] is DBNull ? null : Convert.ToString(result.Tables[0].Rows[i][4]);
                                exceldata.orderdate = result.Tables[0].Rows[i][5] is DBNull ? date : Convert.ToDateTime(result.Tables[0].Rows[i][5]);
                                exceldata.price = result.Tables[0].Rows[i][6] is DBNull ? 0 : Convert.ToInt32(result.Tables[0].Rows[i][6]);
                                exceldata.quantity = result.Tables[0].Rows[i][7] is DBNull ? 0 : Convert.ToInt32(result.Tables[0].Rows[i][7]);
                                exceldata.productname = result.Tables[0].Rows[i][8] is DBNull ? null : Convert.ToString(result.Tables[0].Rows[i][8]);

                                Customer customers = new Customer();
                                customers.name = exceldata.customerName;
                                customers.address = exceldata.fullAddress;
                                _dbContext.Customers.Add(customers);
                                _dbContext.SaveChanges();

                                Products products = new Products();
                                products.name = exceldata.productname;
                                _dbContext.Products.Add(products);
                                _dbContext.SaveChanges();




                                Order order = new Order();

                                switch (exceldata.paymenttype)
                                {
                                    case (int)PaymentTypes.cash:
                                        order.paymenttype = PaymentTypes.cash.ToString();
                                        break;
                                    case (int)PaymentTypes.cheque:
                                        order.paymenttype = PaymentTypes.cheque.ToString();
                                        break;
                                    case (int)PaymentTypes.online:
                                        order.paymenttype = PaymentTypes.online.ToString();
                                        break;
                                    case (int)PaymentTypes.none:
                                        order.paymenttype = PaymentTypes.none.ToString();
                                        break;
                                    case (int)PaymentTypes.paymentDue:
                                        order.paymenttype = PaymentTypes.paymentDue.ToString();
                                        break;
                                    case (int)PaymentTypes.webPayment:
                                        order.paymenttype = PaymentTypes.webPayment.ToString();
                                        break;
                                }

                                switch (exceldata.pinTypeId)
                                {
                                    case (int)SalePinTypes.sale:
                                        order.salepintype = SalePinTypes.sale.ToString();
                                        break;
                                    case (int)SalePinTypes.already_bought:
                                        order.salepintype = SalePinTypes.already_bought.ToString();
                                        break;
                                    case (int)SalePinTypes.delivery_pending:
                                        order.salepintype = SalePinTypes.delivery_pending.ToString();
                                        break;
                                    case (int)SalePinTypes.no:
                                        order.salepintype = SalePinTypes.no.ToString();
                                        break;
                                    case (int)SalePinTypes.not_home:
                                        order.salepintype = SalePinTypes.not_home.ToString();
                                        break;

                                }

                                order.customerid = customers.id;
                                order.orderdate = exceldata.orderdate;
                                order.orderid = exceldata.orderid;
                                order.price = exceldata.price;
                                order.productid = products.id;
                                order.quantity = exceldata.quantity;

                                _dbContext.Orders.Add(order);

                                _dbContext.SaveChanges();



                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }


                        //DataSet result = reader.AsDataSet();
                        reader.Close();

                        return RedirectToAction("Index");


                    }
                    catch (Exception ex)
                    {
                        throw ex;

                    }
                }
                else
                {
                    TempData["message"] = "Please upload excel file";
                    ModelState.AddModelError("File", "Please Upload Your file");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["message"] = "File not valid.";
                ModelState.AddModelError("File", "File not valid.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult GetOrderList(int? draw, int? start, int? length, int[] productids, string startdate, string enddate)
        {
            try
            {

                start = start.HasValue ? start.Value / length : 0;
                start = start == 0 ? 1 : start + 1;
                length = length.HasValue ? length : 10;

                DateTime StartDate = Convert.ToDateTime(startdate);
                DateTime EndDate = Convert.ToDateTime(enddate);

                var pIds = string.Empty;
                if (productids != null)
                {
                    pIds = string.Join(",", productids);
                }

                var SortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
                var SortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();


                var list = SQLQuery<usp_GetOrderList_Result>("exec usp_GetOrderList @PageIndex, @PageSize,@SortColumnName,@SortOrder, @StartDate,@EndDate,@ProductIDs",
                    new SqlParameter("PageIndex", SqlDbType.Int) { Value = start },
                                               new SqlParameter("PageSize", SqlDbType.Int) { Value = length },
                                               new SqlParameter("SortColumnName", SqlDbType.VarChar) { Value = SortColumn },
                                               new SqlParameter("SortOrder", SqlDbType.VarChar) { Value = SortColumnDir },
                    new SqlParameter("StartDate", SqlDbType.DateTime) { Value = StartDate },
                                               new SqlParameter("EndDate", SqlDbType.DateTime) { Value = EndDate },
                                               new SqlParameter("ProductIDs", SqlDbType.VarChar) { Value = pIds }
                                               ).Result;

                var totalcount = list.Count() > 0 ? Convert.ToInt32(list.FirstOrDefault().TotalCount) : 0;
                var filtercount = list.Count() > 0 ? Convert.ToInt32(list.FirstOrDefault().FilterCount) : 0;
                return Json(new
                {
                    draw = draw.Value,
                    recordsTotal = totalcount,
                    recordsFiltered = filtercount,
                    aaData = list
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public enum PaymentTypes
        {
            cash, cheque, online, none, paymentDue, webPayment
        }

        public enum SalePinTypes
        {
            sale, not_home, no, already_bought, delivery_pending
        }

        public Task<List<T>> SQLQuery<T>(string sql, params object[] parameters)
        {
            return _dbContext.Database.SqlQuery<T>(sql, parameters).ToListAsync();
        }

    }
}
