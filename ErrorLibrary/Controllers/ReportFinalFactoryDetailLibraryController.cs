using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper.Enums;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using OfficeOpenXml;

namespace ErrorLibrary.Controllers
{
    public class ReportFinalFactoryDetailLibraryController : Controller
    {
        private readonly ISharedService _sharedService;
        private readonly IReportFinalFactoryService _reportFinalFactoryService;
        private readonly IReportFinalFactoryDetailService _reportFinalFactoryDetailService;
        private readonly IReportFinalFactoryDetailDefectService _reportFinalFactoryDetailDefectService;
        private readonly ICustomerService _customerService;
        private readonly IStyleService _styleService;
        private readonly IDefectService _defectService;
        private readonly IFactoryService _factoryService;
        private readonly IUnitService _unitService;

        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ReportFinalFactoryDetailLibraryController(ISharedService sharedService, IReportFinalFactoryService reportFinalFactoryService, IReportFinalFactoryDetailService reportFinalFactoryDetailService, IReportFinalFactoryDetailDefectService reportFinalFactoryDetailDefectService, ICustomerService customerService, IStyleService styleService, IDefectService defectService, IFactoryService factoryService, IMapper mapper, IUnitService unitService)
        {
            _sharedService = sharedService;
            _reportFinalFactoryService = reportFinalFactoryService;
            _reportFinalFactoryDetailService = reportFinalFactoryDetailService;
            _reportFinalFactoryDetailDefectService = reportFinalFactoryDetailDefectService;
            _customerService = customerService;
            _styleService = styleService;
            _defectService = defectService;
            _factoryService = factoryService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
            _unitService = unitService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReportFinalFactoryDetailExcelPreview([FromBody] PreviewReportFinalFactoryDetailExcelDto previewReportFinalFactoryDetailExcelDto)
        {
            return PartialView("ReportFinalFactoryDetailExcelPreview", previewReportFinalFactoryDetailExcelDto);
        }

        public async Task<IActionResult> GetByReportFinalFactory(int reportFinalFactoryId)
        {
            var reportFinalFactoryDetails = await _reportFinalFactoryDetailService.GetByReportFinalFactoryId(reportFinalFactoryId);
            var reportFinalFactoryDetailIds = reportFinalFactoryDetails.Select(rff => rff.Id).ToList();
            var customers = await _customerService.GetAll();
            var styles = await _styleService.GetAll();

            var reportFinalFactoryDetailDefects = await _reportFinalFactoryDetailDefectService.GetByReportFinalFactoryDetailIds(reportFinalFactoryDetailIds);
            var result = _mapper.Map<List<ReportFinalFactoryDetailGridDto>>(reportFinalFactoryDetails);
            foreach (var reportFinalFactoryDetail in result)
            {
                reportFinalFactoryDetail.CustomerCode = customers.FirstOrDefault(c => c.Id == reportFinalFactoryDetail.CustomerId)!.Code;
                reportFinalFactoryDetail.StyleCode = styles.FirstOrDefault(s => s.Id == reportFinalFactoryDetail.StyleId)!.Code;
                reportFinalFactoryDetail.ReportFinalFactoryDetailDefects = _mapper.Map<List<ReportFinalFactoryDetailDefectDto>>(reportFinalFactoryDetailDefects.Where(x => x.ReportFinalFactoryDetailId == reportFinalFactoryDetail.Id));
            }

            var numberOfPO = result.Count;
            var totalNumberOfChecks = result.Sum(x => x.Quantity);
            var totalNumberOfChecksOfPreFinal = result.Sum(x => (x.PreFinalResult1 == Result.Pass ? 1 : 0) + (x.PreFinalResult2 == Result.Pass ? 1 : 0) + (x.PreFinalResult3 == Result.Pass ? 1 : 0));
            var totalNumberOfChecksOfFinal = result.Sum(x => (x.FinalResult1 == Result.Pass ? 1 : 0) + (x.FinalResult2 == Result.Pass ? 1 : 0) + (x.FinalResult3 == Result.Pass ? 1 : 0));
            var totalNumberOfRecyclingOfPreFinal = result.Sum(x => (x.PreFinalResult1 == Result.Fail ? 1 : 0) + (x.PreFinalResult2 == Result.Fail ? 1 : 0) + (x.PreFinalResult3 == Result.Fail ? 1 : 0));
            var totalNumberOfRecyclingOfFinal = result.Sum(x => (x.FinalResult1 == Result.Fail ? 1 : 0) + (x.FinalResult2 == Result.Fail ? 1 : 0) + (x.FinalResult3 == Result.Fail ? 1 : 0));
            var percentageOfRecyclingOfPreFinal = totalNumberOfChecksOfPreFinal > 0 ? Math.Round((double)totalNumberOfRecyclingOfPreFinal / totalNumberOfChecksOfPreFinal * 100, 2) : 0;
            var percentageOfRecyclingOfFinal = totalNumberOfChecksOfFinal > 0 ? Math.Round((double)totalNumberOfRecyclingOfFinal / totalNumberOfChecksOfFinal * 100, 2) : 0;

            _responseDto.Result = new
            {
                data = result,
                numberOfPO,
                totalNumberOfChecks,
                totalNumberOfChecksOfPreFinal,
                totalNumberOfChecksOfFinal,
                totalNumberOfRecyclingOfPreFinal,
                totalNumberOfRecyclingOfFinal,
                percentageOfRecyclingOfPreFinal,
                percentageOfRecyclingOfFinal
            };
            return Json(_responseDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetByDate(DateOnly date)
        {
            var reportFinalFactories = await _reportFinalFactoryService.GetAllByCreateDate(date);
            var reportFinalFactoryIds = reportFinalFactories.Select(rff => rff.Id).ToList();
            var reportFinalFactoryDetails = await _reportFinalFactoryDetailService.GetByReportFinalFactoryIds(reportFinalFactoryIds);
            var reportFinalFactoryDetailIds = reportFinalFactoryDetails.Select(rffd => rffd.Id).ToList();
            var customers = await _customerService.GetAll();
            var styles = await _styleService.GetAll();

            var reportFinalFactoryDetailDefects = await _reportFinalFactoryDetailDefectService.GetByReportFinalFactoryDetailIds(reportFinalFactoryDetailIds);
            var result = _mapper.Map<List<ReportFinalFactoryDetailGridDto>>(reportFinalFactoryDetails);

            var units = await _unitService.GetAll();
            var factories = await _factoryService.GetAll();



            foreach (var reportFinalFactoryDetail in result)
            {
                var reportFinalFactory = reportFinalFactories.FirstOrDefault(rff => rff.Id == reportFinalFactoryDetail.ReportFinalFactoryId);
                var factory = factories.FirstOrDefault(f => f.Id == reportFinalFactory!.FactoryId);
                var unit = units.FirstOrDefault(u => u.Id == factory!.UnitId);

                reportFinalFactoryDetail.FactoryName = factory!.Name;
                reportFinalFactoryDetail.UnitName = unit!.Name;
                reportFinalFactoryDetail.CustomerCode = customers.FirstOrDefault(c => c.Id == reportFinalFactoryDetail.CustomerId)!.Code;
                reportFinalFactoryDetail.StyleCode = styles.FirstOrDefault(s => s.Id == reportFinalFactoryDetail.StyleId)!.Code;
                reportFinalFactoryDetail.ReportFinalFactoryDetailDefects = _mapper.Map<List<ReportFinalFactoryDetailDefectDto>>(reportFinalFactoryDetailDefects.Where(x => x.ReportFinalFactoryDetailId == reportFinalFactoryDetail.Id));
            }

            var numberOfPO = result.Count;
            var totalNumberOfChecks = result.Sum(x => x.Quantity);
            var totalNumberOfChecksOfPreFinal = result.Sum(x => (x.PreFinalResult1 == Result.Pass ? 1 : 0) + (x.PreFinalResult2 == Result.Pass ? 1 : 0) + (x.PreFinalResult3 == Result.Pass ? 1 : 0));
            var totalNumberOfChecksOfFinal = result.Sum(x => (x.FinalResult1 == Result.Pass ? 1 : 0) + (x.FinalResult2 == Result.Pass ? 1 : 0) + (x.FinalResult3 == Result.Pass ? 1 : 0));
            var totalNumberOfRecyclingOfPreFinal = result.Sum(x => (x.PreFinalResult1 == Result.Fail ? 1 : 0) + (x.PreFinalResult2 == Result.Fail ? 1 : 0) + (x.PreFinalResult3 == Result.Fail ? 1 : 0));
            var totalNumberOfRecyclingOfFinal = result.Sum(x => (x.FinalResult1 == Result.Fail ? 1 : 0) + (x.FinalResult2 == Result.Fail ? 1 : 0) + (x.FinalResult3 == Result.Fail ? 1 : 0));
            var percentageOfRecyclingOfPreFinal = totalNumberOfChecksOfPreFinal > 0 ? Math.Round((double)totalNumberOfRecyclingOfPreFinal / totalNumberOfChecksOfPreFinal * 100, 2) : 0;
            var percentageOfRecyclingOfFinal = totalNumberOfChecksOfFinal > 0 ? Math.Round((double)totalNumberOfRecyclingOfFinal / totalNumberOfChecksOfFinal * 100, 2) : 0;

            _responseDto.Result = new
            {
                data = result.OrderBy(x => x.UnitName).ThenBy(x => x.FactoryName),
                numberOfPO,
                totalNumberOfChecks,
                totalNumberOfChecksOfPreFinal,
                totalNumberOfChecksOfFinal,
                totalNumberOfRecyclingOfPreFinal,
                totalNumberOfRecyclingOfFinal,
                percentageOfRecyclingOfPreFinal,
                percentageOfRecyclingOfFinal
            };
            _responseDto.IsSuccess = true;
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReportFinalFactoryDetail([FromBody] ReportFinalFactoryDetailDto reportFinalFactoryDetailDto)
        {
            var reportFinalFactory = await _reportFinalFactoryService.GetById(reportFinalFactoryDetailDto.ReportFinalFactoryId);
            if (reportFinalFactory == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "ReportFinalFactory not found.";
                return Json(_responseDto);
            }
            var customer = await _customerService.GetByCode(reportFinalFactoryDetailDto.CustomerCode);
            var style = await _styleService.GetByCode(reportFinalFactoryDetailDto.StyleCode);
            var reportFinalFactoryDetailDefects = await InitialReportFinalFactoryDetailDefects();

            var reportFinalFactoryDetail = new ReportFinalFactoryDetail
            {
                ReportFinalFactoryId = reportFinalFactoryDetailDto.ReportFinalFactoryId,
                PO = reportFinalFactoryDetailDto.PO,
                Quantity = reportFinalFactoryDetailDto.Quantity,
                ReportFinalFactoryDetailDefects = reportFinalFactoryDetailDefects,
                Customer = customer,
                Style = style
            };
            
            _reportFinalFactoryDetailService.Add(reportFinalFactoryDetail);

            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "ReportFinalFactoryDetail created successfully.";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Failed to create ReportFinalFactoryDetail.";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReportFinalFactoryDetail([FromBody] ReportFinalFactoryDetailDto reportFinalFactoryDetailDto)
        {
            var reportFinalFactoryDetail = await _reportFinalFactoryDetailService.GetById(reportFinalFactoryDetailDto.Id);
            if (reportFinalFactoryDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "ReportFinalFactoryDetail not found.";
                return Json(_responseDto);
            }
            var customer = await _customerService.GetByCode(reportFinalFactoryDetailDto.CustomerCode);
            var style = await _styleService.GetByCode(reportFinalFactoryDetailDto.StyleCode);
            _mapper.Map(reportFinalFactoryDetailDto, reportFinalFactoryDetail);
            reportFinalFactoryDetail.Customer = customer;
            reportFinalFactoryDetail.Style = style;
            _reportFinalFactoryDetailService.Update(reportFinalFactoryDetail);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "ReportFinalFactoryDetail update successfully.";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Failed to update ReportFinalFactoryDetail.";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReportFinalFactoryDetail([FromBody] int id)
        {
            var reportFinalFactoryDetail = await _reportFinalFactoryDetailService.GetById(id);
            if(reportFinalFactoryDetail == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "ReportFinalFactoryDetail not found.";
                return Json(_responseDto);
            }
            _reportFinalFactoryDetailService.Delete(reportFinalFactoryDetail);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.IsSuccess = true;
                _responseDto.Message = "ReportFinalFactoryDetail deleted successfully.";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Failed to delete ReportFinalFactoryDetail.";
            return Json(_responseDto);
        }

        private async Task<List<ReportFinalFactoryDetailDefect>> InitialReportFinalFactoryDetailDefects()
        {
            var defects = await _defectService.GetAll();
            var reportFinalFactoryDetailDefects = new List<ReportFinalFactoryDetailDefect>();
            foreach (var defect in defects)
            {
                reportFinalFactoryDetailDefects.Add(new ReportFinalFactoryDetailDefect
                {
                    DefectId = defect.Id,
                    Quantity = 0
                });
            }
            return reportFinalFactoryDetailDefects;
        }

        [HttpPost]
        public async Task<IActionResult> ImportReportFinalFactoryToExcel([FromForm] ImportExcelDto importExcelDto)
        {
            ExcelPackage.License.SetNonCommercialPersonal("ImportReportFinalFactoryLibrary");

            var reportFinalFactoryDetailExcel = new List<ReportFinalFactoryDetailGridDto>();
            var defects = await _defectService.GetAll();
            using (var stream = new MemoryStream())
            {
                await importExcelDto.File.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[importExcelDto.WorksheetIndex];
                    int rowCount = worksheet.Dimension.Rows;
                    int columnCount = worksheet.Dimension.Columns;
                   
                    for (int row = 4; row <= rowCount; row++) // Bỏ header
                    {
                        var customerStyle = worksheet.Cells[row, 1].Text;
                        var reportFinalFactoryDetailDefects = new List<ReportFinalFactoryDetailDefectDto>();

                        for(int col = 21; col <= columnCount; col++)
                        {
                            var defectName = worksheet.Cells[3, col].Text;
                            var defectCode = worksheet.Cells[2, col].Text;
                            var defect = defects.FirstOrDefault(d => d.Name == defectName && d.Code == defectCode);
                            if (defect == null) continue;
                            reportFinalFactoryDetailDefects.Add(new ReportFinalFactoryDetailDefectDto
                            {
                                DefectId = defect.Id,
                                Quantity = int.TryParse(worksheet.Cells[row, col].Text, out int defectQuantity) ? defectQuantity : 0
                            });
                        }
                        reportFinalFactoryDetailExcel.Add(new ReportFinalFactoryDetailGridDto
                        {
                            Id = row,
                            CustomerCode = customerStyle.Split('/')[0],
                            StyleCode = customerStyle.Substring(customerStyle.IndexOf('/') + 1),
                            PO = worksheet.Cells[row, 2].Text,
                            Quantity = int.TryParse(worksheet.Cells[row, 3].Text, out int quantity) ? quantity : 0,
                            PreFinalMajor = int.TryParse(worksheet.Cells[row, 4].Text, out int preFinalMajor) ? preFinalMajor : 0,
                            PreFinalMinor = int.TryParse(worksheet.Cells[row, 5].Text, out int preFinalMinor) ? preFinalMinor : 0,
                            PreFinalDate1 = DateTime.TryParse(worksheet.Cells[row, 6].Text, out DateTime preFinalDate1) ? preFinalDate1 : (DateTime?)null,
                            PreFinalResult1 = Enum.TryParse(worksheet.Cells[row, 7].Text, out Result preFinalResult1) ? preFinalResult1 : (Result?)null,
                            PreFinalDate2 = DateTime.TryParse(worksheet.Cells[row, 8].Text, out DateTime preFinalDate2) ? preFinalDate2 : (DateTime?)null,
                            PreFinalResult2 = Enum.TryParse(worksheet.Cells[row, 9].Text, out Result preFinalResult2) ? preFinalResult2 : (Result?)null,
                            PreFinalDate3 = DateTime.TryParse(worksheet.Cells[row, 10].Text, out DateTime preFinalDate3) ? preFinalDate3 : (DateTime?)null,
                            PreFinalResult3 = Enum.TryParse(worksheet.Cells[row, 11].Text, out Result preFinalResult3) ? preFinalResult3 : (Result?)null,
                            FinalMajor = int.TryParse(worksheet.Cells[row, 12].Text, out int finalMajor) ? finalMajor : 0,
                            FinalMinor = int.TryParse(worksheet.Cells[row, 13].Text, out int finalMinor) ? finalMinor : 0,
                            FinalDate1 = DateTime.TryParse(worksheet.Cells[row, 14].Text, out DateTime finalDate1) ? finalDate1 : (DateTime?)null,
                            FinalResult1 = Enum.TryParse(worksheet.Cells[row, 15].Text, out Result finalResult1) ? finalResult1 : (Result?)null,
                            FinalDate2 = DateTime.TryParse(worksheet.Cells[row, 16].Text, out DateTime finalDate2) ? finalDate2 : (DateTime?)null,
                            FinalResult2 = Enum.TryParse(worksheet.Cells[row, 17].Text, out Result finalResult2) ? finalResult2 : (Result?)null,
                            FinalDate3 = DateTime.TryParse(worksheet.Cells[row, 18].Text, out DateTime finalDate3) ? finalDate3 : (DateTime?)null,
                            FinalResult3 = Enum.TryParse(worksheet.Cells[row, 19].Text, out Result finalResult3) ? finalResult3 : (Result?)null,
                            Remark = worksheet.Cells[row, 20].Text,
                            ReportFinalFactoryDetailDefects = reportFinalFactoryDetailDefects
                        });
                    }
                }
            }
            var customerCodes = reportFinalFactoryDetailExcel.Select(x => x.CustomerCode).Distinct().ToList();
            var styleCodes = reportFinalFactoryDetailExcel.Select(x => x.StyleCode).Distinct().ToList();

            var customers = await _customerService.GetByCodes(customerCodes);
            var styles = await _styleService.GetByCodes(styleCodes);

            var customerCodesExcept = customerCodes.Except(customers.Select(x => x.Code)).ToList();
            var styleCodesExcept = styleCodes.Except(styles.Select(x => x.Code)).ToList();

            var previewReportFinalFactoryDetailExcel = new PreviewReportFinalFactoryDetailExcelDto
            {
                Customers = _mapper.Map<List<CustomerDto>>(customers),
                Styles = _mapper.Map<List<StyleDto>>(styles),
                CustomerCodesExcept = customerCodesExcept,
                StyleCodesExcept = styleCodesExcept,
                Excel = reportFinalFactoryDetailExcel
            };
            _responseDto.Result = previewReportFinalFactoryDetailExcel;
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> ExportReportFinalFactoryToExcel([FromBody] int factoryId)
        {
            ExcelPackage.License.SetNonCommercialPersonal("ExportReportFinalFactoryLibrary");

            var sourceFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "import-form", "ReportFinalFactoryTemplate.xlsx");
            var sourceFile = new FileInfo(sourceFilePath);
            var factory = await _factoryService.GetById(factoryId);
            var defects = await _defectService.GetAll();

            using (var package = new ExcelPackage(sourceFile))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Sheet1 của template

                var startRow = worksheet.Dimension.Start.Row;
                var endRow = worksheet.Dimension.End.Row;

                var newPackage = new ExcelPackage();
                var newSheet = newPackage.Workbook.Worksheets.Add("Sheet1");

                // Copy giá trị từ cột A đến T
                worksheet.Cells[startRow, 1, endRow, 20].Copy(newSheet.Cells[startRow, 1, endRow, 20]);

                // Gán giá trị cho ô merge
                int dynamicStartCol = 21;
                int dynamicEndCol = defects.Count + 20;
                newSheet.Cells[1, dynamicStartCol, 1, dynamicEndCol].Merge = true;
                newSheet.Cells[1, dynamicStartCol].Value = "Defects";
                newSheet.Cells[1, dynamicStartCol].Style.Font.Bold = true;
                newSheet.Cells[1, dynamicStartCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                newSheet.Cells[1, dynamicStartCol].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                for (int col = dynamicStartCol; col <= dynamicEndCol; col++)
                {
                    var defect = defects[col - 21];
                    newSheet.Cells[2, col].Value = defect.Code;
                    newSheet.Cells[3, col].Value = defect.Name;

                    newSheet.Cells[2, col].Style.Font.Bold = true;
                    newSheet.Cells[2, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    newSheet.Cells[2, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                    newSheet.Cells[3, col].Style.Font.Bold = true;
                    newSheet.Cells[3, col].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    newSheet.Cells[3, col].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }

                var dynamicRange = newSheet.Cells[1, dynamicStartCol, endRow, dynamicEndCol];
                dynamicRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                dynamicRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                dynamicRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                dynamicRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                var stream = new MemoryStream();
                newPackage.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"ReportFinalFactoryTemplate.xlsx";
                Response.Headers["Content-Disposition"] = $"attachment; filename={excelName}";
                return File(stream,
                 "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReportFinalFactoryDetailsFromExcel([FromBody] List<ReportFinalFactoryDetailGridDto> reportFinalFactoryDetailGridDtos)
        {
            var reportFinalFactoryDetails = new List<ReportFinalFactoryDetail>();
            foreach(var reportFinalFactoryDetailGridDto in reportFinalFactoryDetailGridDtos)
            {
                var customer = await _customerService.GetByCode(reportFinalFactoryDetailGridDto.CustomerCode);
                var style = await _styleService.GetByCode(reportFinalFactoryDetailGridDto.StyleCode);
                var reportFinalFactoryDetail = new ReportFinalFactoryDetail();
                _mapper.Map(reportFinalFactoryDetailGridDto, reportFinalFactoryDetail);
                reportFinalFactoryDetail.Customer = customer;
                reportFinalFactoryDetail.Style = style;
                reportFinalFactoryDetails.Add(reportFinalFactoryDetail);
            }
            _reportFinalFactoryDetailService.AddRange(reportFinalFactoryDetails);
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Result = 
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Report Final Factory Details added successfully.";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Failed to added Report Final Factory Details.";
            return Json(_responseDto);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReportFinalFactoryDetailsFromExcel([FromBody] int reportFinalFactoryId)
        {
            var reportFinalFactoryDetails = await _reportFinalFactoryDetailService.GetByReportFinalFactoryId(reportFinalFactoryId);

            foreach(var reportFinalFactoryDetail in reportFinalFactoryDetails)
            {
                var reportFinalFactoryDetailDefects = await _reportFinalFactoryDetailDefectService.GetByReportFinalFactoryDetailId(reportFinalFactoryDetail.Id);
                _reportFinalFactoryDetailDefectService.DeleteRange(reportFinalFactoryDetailDefects);
                _reportFinalFactoryDetailService.Delete(reportFinalFactoryDetail);
            }
            if (await _sharedService.SaveAllChanges())
            {
                _responseDto.Result =
                _responseDto.IsSuccess = true;
                _responseDto.Message = "Report Final Factory Details added successfully.";
                return Json(_responseDto);
            }
            _responseDto.IsSuccess = false;
            _responseDto.Message = "Failed to added Report Final Factory Details.";
            return Json(_responseDto);
        }
    }
}
