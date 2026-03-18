using AutoMapper;
using ErrorLibrary.DTOs;
using ErrorLibrary.Entities;
using ErrorLibrary.Helper.Enums;
using ErrorLibrary.Services;
using ErrorLibrary.Services.IServices;
using Microsoft.AspNetCore.Mvc;
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

        private readonly IMapper _mapper;
        protected ResponseDto _responseDto;

        public ReportFinalFactoryDetailLibraryController(ISharedService sharedService, IReportFinalFactoryService reportFinalFactoryService, IReportFinalFactoryDetailService reportFinalFactoryDetailService, IReportFinalFactoryDetailDefectService reportFinalFactoryDetailDefectService, ICustomerService customerService, IStyleService styleService, IDefectService defectService, IFactoryService factoryService, IMapper mapper)
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
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ReportFinalFactoryDetailExcelPreview([FromBody] ReportFinalFactoryDetailGridDto reportFinalFactoryDetailGridDto)
        {
            return PartialView("ReportFinalFactoryDetailExcelPreview", reportFinalFactoryDetailGridDto);
        }

        public async Task<IActionResult> GetByReportFinalFactory(int reportFinalFactoryId)
        {
            var reportFinalFactoryDetails = await _reportFinalFactoryDetailService.GetByReportFinalFactoryId(reportFinalFactoryId);
            var customers = await _customerService.GetAll();
            var styles = await _styleService.GetAll();

            var reportFinalFactoryDetailDefects = await _reportFinalFactoryDetailDefectService.GetAll();
            var result = _mapper.Map<List<ReportFinalFactoryDetailGridDto>>(reportFinalFactoryDetails);
            foreach (var reportFinalFactoryDetail in result)
            {
                reportFinalFactoryDetail.CustomerCode = customers.FirstOrDefault(c => c.Id == reportFinalFactoryDetail.CustomerId)!.Code;
                reportFinalFactoryDetail.StyleCode = styles.FirstOrDefault(c => c.Id == reportFinalFactoryDetail.StyleId)!.Code;
                reportFinalFactoryDetail.ReportFinalFactoryDetailDefects = _mapper.Map<List<ReportFinalFactoryDetailDefectDto>>(reportFinalFactoryDetailDefects.Where(x => x.ReportFinalFactoryDetailId == reportFinalFactoryDetail.Id));
            }
            _responseDto.Result = result;
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
            ExcelPackage.License.SetNonCommercialPersonal("ErrorLibrary");

            var reportFinalFactoryDetailExcel = new List<ReportFinalFactoryDetailGridDto>();
            var defects = new List<Defect>();
            using (var stream = new MemoryStream())
            {
                await importExcelDto.File.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[importExcelDto.WorksheetIndex];
                    int rowCount = worksheet.Dimension.Rows;
                    int columnCount = worksheet.Dimension.Columns;
                    for (int col = 21; col <= columnCount; col++)
                    {
                        defects.Add(new Defect
                        {
                            Name = worksheet.Cells[3, col].Text
                        });
                    }
                    for (int row = 4; row <= rowCount; row++) // Bỏ header
                    {
                        var customerStyle = worksheet.Cells[row, 1].Text;
                        var reportFinalFactoryDetailDefects = new List<ReportFinalFactoryDetailDefectDto>();
                        for(int col = 21; col <= columnCount; col++)
                        {
                            reportFinalFactoryDetailDefects.Add(new ReportFinalFactoryDetailDefectDto
                            {
                                DefectId = 1, // Ví dụ: DefectId = 1, bạn có thể thay đổi theo logic của mình
                                Quantity = int.TryParse(worksheet.Cells[row, col].Text, out int defectQuantity) ? defectQuantity : 0
                            });
                        }
                        reportFinalFactoryDetailExcel.Add(new ReportFinalFactoryDetailGridDto
                        {
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
            //var errorGroupNames = errorExcelDtos.Select(x => x.ErrorGroup).Distinct().ToList();
            //var productCategoryNames = errorExcelDtos.Select(x => x.ProductCategory).Distinct().ToList();
            //var errorCategoryNames = errorExcelDtos.Select(x => x.ErrorCategory).Distinct().ToList();

            //var errorGroups = await _errorGroupService.GetByNames(errorGroupNames);
            //var productCategories = await _productCategoryService.GetByNames(productCategoryNames);
            //var errorCategories = await _errorCategoryService.GetByNames(errorCategoryNames);

            //var errorGroupNamesExcept = errorGroupNames.Except(errorGroups.Select(x => x.Name)).ToList();
            //var productCategoryNamesExcept = productCategoryNames.Except(productCategories.Select(x => x.Name)).ToList();
            //var errorCategoryNamesExcept = errorCategoryNames.Except(errorCategories.Select(x => x.Name)).ToList();

            //var previewErrorExcel = new PreviewErrorExcelDto
            //{
            //    ErrorGroups = _mapper.Map<List<ErrorGroupDto>>(errorGroups),
            //    ProductCategories = _mapper.Map<List<ProductCategoryDto>>(productCategories),
            //    ErrorCategories = _mapper.Map<List<ErrorCategoryDto>>(errorCategories),
            //    ErrorGroupNamesExcept = errorGroupNamesExcept,
            //    ProductCategoryNamesExcept = productCategoryNamesExcept,
            //    ErrorCategoryNamesExcept = errorCategoryNamesExcept,
            //    Excel = errorExcelDtos,
            //};
            _responseDto.Result = reportFinalFactoryDetailExcel;
            return Json(_responseDto);
        }

    }
}
