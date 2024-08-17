
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Utility
    {
        private static Utility _instance;
        public static Utility GetInstance()
        {
            if (_instance == null)
                _instance = new Utility();
            return _instance;
        }

        public Utility()
        {

        }

        // ********************
        #region Constants

        public const string Key_UserId = System.Security.Claims.ClaimTypes.NameIdentifier;
        public const string Key_UserName = "UserName";
        public const string Key_Token = "ApiToken";
        public const string Key_AuthenticationType = "jwt";
        public const string Key_AuthenticationHeaderScheme = "bearer";

        public const string SortAscending = "asc";
        public const string SortDescending = "desc";

        public const int DataGridFilterDelay = 2200;

        public const int PageNumber = 1;

        public const int PageSize = 10;
        public const int PageSize1 = 1;
        public const int PageSize3 = 3;
        public const int PageSize5 = 5;
        public const int PageSize100 = 100;
        public const int PageSize200 = 200;
        public const int PageSize500 = 500;
        public const int PageSize1000 = 1000;
        public const int PageSizeCombo = 200;
        public const int PageSizeMax = int.MaxValue;

        public const string lineCategoryChart = "lineCategoryChart";
        public const string barCategoryChart = "barCategoryChart";
        public const string barStackedCategoryChart = "barStackedCategoryChart";
        public const string barStackedHorizontalCategoryChart = "barStackedHorizontalCategoryChart";

        #endregion

        // ********************
        #region Methods

        public static string getBaseUrl(IConfiguration configuration)
        {
            string BaseUrl =
                configuration["ServerSettings:BaseUrl"];

            return BaseUrl;
        }

        public static TimeSpan getTimeoutHttpClient(IConfiguration configuration)
        {
            TimeSpan result;
            int hours, min, sec;
            int.TryParse(configuration["TimeoutHttpClient:hours"], out hours);
            int.TryParse(configuration["TimeoutHttpClient:min"], out min);
            int.TryParse(configuration["TimeoutHttpClient:sec"], out sec);
            
            if(hours == 0 && min == 0 && sec == 0) 
            {
                result = new TimeSpan(0, 1, 40);
            }
            else
            {
                result = new TimeSpan(hours, min, sec);
            }

            return result;
        }

        public static string setServiceUri<T>()
        {
            var strTypeof = typeof(T);

            string strResult = strTypeof.Name;
            strResult = $"api/{strResult.Substring(0, strResult.IndexOf("Service"))}/";

            return strResult;
        }

        public static string setUri<T>()
        {
            string strResult = null;
            string strTypeof = typeof(T).ToString();
            //var strNameof = nameof(T).ToString();

            string[] lstSplit = strTypeof.Split('.');
            bool isSetUri = false;
            foreach (var item in lstSplit)
            {
                if (isSetUri)
                {
                    strResult += $"/{item}";
                }
                if (item.ToLower() == "pages")
                {
                    isSetUri = true;
                }
            }

            return strResult;
        }

        public async Task<string> getNameSpaceFullOfViewModelAsync(string strModelName, string strMiddle = null)
        {
            string strViewModel = strModelName;
            if (strModelName.EndsWith("ies"))
                strViewModel = strModelName.Replace("ies", "y");
            else if (strModelName.EndsWith("s"))
                strViewModel = strModelName.TrimEnd('s');
            else if (strViewModel == strModelName)
                strModelName = $"{strModelName}s";

            string strResult = $"ViewModels.{strModelName}.{strViewModel}";
            if (strMiddle == null)
                strResult = $"ViewModels.{strModelName}.{strViewModel}ViewModel";
            else
                strResult = $"ViewModels.{strModelName}.{strViewModel}{strMiddle}ViewModel";

            return await Task.FromResult(strResult);
        }

        public string getNameOfResource(string propertyName)
        {
            //string resourceValue = Resources.DataDictionary.ResourceManager.GetString(propertyName); 

            ResourceManager resourceManager =
                new ResourceManager("Resources.DataDictionary", typeof(Resources.DataDictionary).Assembly);

            string resourceValue = resourceManager.GetString(propertyName);

            return resourceValue;
        }

        public async Task<string> getAvatarAsync(string strGender)
        {
            string strResult = $"./content/Themes/MainTheme/images/avatar/avatar{strGender}.png";

            return await Task.FromResult(strResult);
        }
        
        public async Task<bool> CompareDateAsync(DateTime dtFirst, DateTime dtSecond, bool isEqual = false)
        {
            bool result = false;

            int intCompare = DateTime.Compare(dtFirst.Date, dtSecond.Date);
            if (intCompare > 0)
            {
                result = false;
            }
            else if (isEqual && intCompare <= 0)
            {
                result = true;
            }
            else 
            {
                result = true;
            }

            return await Task.FromResult(result);
        }

        public string getIsActiveTitle(string isActive)
        {
            string strResult = (isActive.Trim().ToLower() == "true") ? Resources.DataDictionary.Active : Resources.DataDictionary.InActive;

            return strResult;
        }

        public string getYesNoTitle(string value)
        {
            string strResult = (value.Trim().ToLower() == "true") ? Resources.DataDictionary.Yes : Resources.DataDictionary.No;

            return strResult;
        }

        public string setSeparatedDigits(string propertyValue)
        {
            string strResult = propertyValue;
            
            if (propertyValue != null)
            {
                int intValue;
                long longValue; 
                decimal decimalValue; 
                if (int.TryParse(propertyValue, out intValue))
                {
                    strResult = string.Format("{0:N0}", intValue);
                }
                else if (long.TryParse(propertyValue, out longValue))
                {
                    strResult = string.Format("{0:N0}", longValue);
                }
                else if (decimal.TryParse(propertyValue, out decimalValue))
                {
                    strResult = string.Format("{0:N0}", decimalValue);
                }
            }

            return strResult;
        }

        public async Task<System.Guid?> getUserIdFromTokenAsync(Client.Services.Tokens.ITokensService tokenService)
        {
            try
            {
                var UserId = await tokenService.GetUserId();

                return UserId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // ********************
        /// <summary>
        /// آقا/خانم
        /// </summary>
        public async Task<IEnumerable<ComboClass>> getGenderType1Async()
        {
            IList<ComboClass> lstCombo = new List<ComboClass>()
            {
                new ComboClass { Id = (int)Domain.Enums.GenderTypeEnum.Male, Title = Resources.DataDictionary.Male },
                new ComboClass { Id = (int)Domain.Enums.GenderTypeEnum.Female, Title = Resources.DataDictionary.Female }
            };

            return await Task.FromResult(lstCombo);
        }


        /// <summary>
        /// مرد/زن
        /// </summary>
        public async Task<IEnumerable<ComboClass>> getGenderType2Async()
        {
            IList<ComboClass> lstCombo = new List<ComboClass>
            {
                new ComboClass { Id = (int)Domain.Enums.GenderTypeEnum.Male, Title = Resources.DataDictionary.Man },
                new ComboClass { Id = (int)Domain.Enums.GenderTypeEnum.Female, Title = Resources.DataDictionary.Woman }
            };

            return await Task.FromResult(lstCombo);
        }

        /// <summary>
        /// ماه های شمسی
        /// </summary>
        public async Task<IEnumerable<ComboClass>> getPersianMonthsAsync()
        {
            IList<ComboClass> lstCombo = new List<ComboClass>
            {
                new ComboClass { Id = 1, Title = Resources.DataDictionary.PersianMonth01 },
                new ComboClass { Id = 2, Title = Resources.DataDictionary.PersianMonth02 },
                new ComboClass { Id = 3, Title = Resources.DataDictionary.PersianMonth03 },
                new ComboClass { Id = 4, Title = Resources.DataDictionary.PersianMonth04 },
                new ComboClass { Id = 5, Title = Resources.DataDictionary.PersianMonth05 },
                new ComboClass { Id = 6, Title = Resources.DataDictionary.PersianMonth06 },
                new ComboClass { Id = 7, Title = Resources.DataDictionary.PersianMonth07 },
                new ComboClass { Id = 8, Title = Resources.DataDictionary.PersianMonth08 },
                new ComboClass { Id = 9, Title = Resources.DataDictionary.PersianMonth09 },
                new ComboClass { Id = 10, Title = Resources.DataDictionary.PersianMonth10 },
                new ComboClass { Id = 11, Title = Resources.DataDictionary.PersianMonth11 },
                new ComboClass { Id = 12, Title = Resources.DataDictionary.PersianMonth12 }
            };

            return await Task.FromResult(lstCombo);
        } 

        /// <summary>
        /// ماه های شمسی یک فصل
        /// </summary>
        public async Task<IList<ComboClass>> getPersianMonthsOfSeasonAsync(int seasonId)
        {

            IList<ComboClass> lstCombo = new List<ComboClass>();

            switch (seasonId)
            {
                case 1:
                    lstCombo = new List<ComboClass>
                    {
                        new ComboClass { Id = 1, Title = Resources.DataDictionary.PersianMonth01 },
                        new ComboClass { Id = 2, Title = Resources.DataDictionary.PersianMonth02 },
                        new ComboClass { Id = 3, Title = Resources.DataDictionary.PersianMonth03 },

                    };
                    return await Task.FromResult(lstCombo);
                case 2:
                    lstCombo = new List<ComboClass>
                    {
                        new ComboClass { Id = 4, Title = Resources.DataDictionary.PersianMonth04 },
                        new ComboClass { Id = 5, Title = Resources.DataDictionary.PersianMonth05 },
                        new ComboClass { Id = 6, Title = Resources.DataDictionary.PersianMonth06 },

                    };
                    return await Task.FromResult(lstCombo);
                case 3:
                    lstCombo = new List<ComboClass>
                    {
                        new ComboClass { Id = 7, Title = Resources.DataDictionary.PersianMonth07 },
                        new ComboClass { Id = 8, Title = Resources.DataDictionary.PersianMonth08 },
                        new ComboClass { Id = 9, Title = Resources.DataDictionary.PersianMonth09 },

                    };
                    return await Task.FromResult(lstCombo);
                case 4:
                    lstCombo = new List<ComboClass>
                    {
                        new ComboClass { Id = 10, Title = Resources.DataDictionary.PersianMonth10 },
                        new ComboClass { Id = 11, Title = Resources.DataDictionary.PersianMonth11 },
                        new ComboClass { Id = 12, Title = Resources.DataDictionary.PersianMonth12 }

                    };
                    return await Task.FromResult(lstCombo);
                default:
                    break;
            }

            return await Task.FromResult(lstCombo);
        }        
        
        /// <summary>
        /// مرد/زن
        /// </summary>
        public async Task<IEnumerable<ComboClass>> getSeasonsAsync()
        {
            IList<ComboClass> lstCombo = new List<ComboClass>
            {
                new ComboClass { Id = (int)Domain.Enums.SeasonEnum.Spring, Title = Resources.DataDictionary.Spring },
                new ComboClass { Id = (int)Domain.Enums.SeasonEnum.Summer, Title = Resources.DataDictionary.Summer },
                new ComboClass { Id = (int)Domain.Enums.SeasonEnum.Autumn, Title = Resources.DataDictionary.Autumn },
                new ComboClass { Id = (int)Domain.Enums.SeasonEnum.Winter, Title = Resources.DataDictionary.Winter },
            };

            return await Task.FromResult(lstCombo);
        }


        public async Task<IList<ViewModels.ExcelExportColumns>> getGridColumnListAsync<TViewModel>(IList<Radzen.Blazor.RadzenDataGridColumn<TViewModel>> columns)
        {
            List<ViewModels.ExcelExportColumns> lstColumns = new List<ViewModels.ExcelExportColumns>();

            await Task.Run(() =>
            {
                foreach (var col in columns)
                {
                    if (col.Property != null && col.Visible && lstColumns.Any(c => c.ColumnName == col.Property) == false)
                    {
                        bool isSeparatedDigits = false;
                        if(col.CssClass != null && col.CssClass.Contains("separatedDigits"))
                            isSeparatedDigits = true;

                        ViewModels.ExcelExportColumns exportColumns = new ViewModels.ExcelExportColumns()
                        {
                            ColumnName = col.Property,
                            IsSeparatedDigits = isSeparatedDigits,
                        };
                        lstColumns.Add(exportColumns);
                    }
                }
            });

            return await Task.FromResult(lstColumns);
        }

        public async Task ModalCloseAsync(IJSRuntime jsRuntime, string elementId)
        {
            await jsRuntime.InvokeVoidAsync("modalClose", elementId);
        }

        public async Task FileDownloadAsFileStreamFromAPIAsync(IJSRuntime jsRuntime, string fileName, DotNetStreamReference streamReference)
        {
            await jsRuntime.InvokeAsync<object>(
                "fileDownloadAsFileStreamFromApiFunction",
                fileName,
                streamReference
            );
        }

        public async Task FileDownloadAsDataStreamFromAPIAsync(IJSRuntime jsRuntime, string fileName, byte[] data)
        {
            await jsRuntime.InvokeAsync<object>(
                "fileDownloadAsDataStreamFromApiFunction",
                fileName,
                Convert.ToBase64String(data)
            );
        }

        public async Task FileDownloadAsStreamFromURLAsync(IJSRuntime jsRuntime, string fileName, string fileURL)
        {
            await jsRuntime.InvokeAsync<object>(
                "fileDownloadAsStreamFromUrlFunction",
                fileName,
                fileURL
            );
        }
        
        public async Task<Domain.Enums.FileAllowedTypeEnum?> GetFileExtensionAsync(string fileName)
        {
            Domain.Enums.FileAllowedTypeEnum? allowedType = null;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                allowedType = null;
            }
            else
            {
                var fileExtension = System.IO.Path.GetExtension(fileName);

                if (Domain.Constants.FileAllowedExtensions.PDF.Contains(fileExtension))
                {
                    allowedType = Domain.Enums.FileAllowedTypeEnum.PDF;
                }
                else if (Domain.Constants.FileAllowedExtensions.Text.Contains(fileExtension))
                {
                    allowedType = Domain.Enums.FileAllowedTypeEnum.Text;
                }
                else if (Domain.Constants.FileAllowedExtensions.Image.Contains(fileExtension))
                {
                    allowedType = Domain.Enums.FileAllowedTypeEnum.Image;
                }
                else if (Domain.Constants.FileAllowedExtensions.Compressed.Contains(fileExtension))
                {
                    allowedType = Domain.Enums.FileAllowedTypeEnum.Compressed;
                }
                else if (Domain.Constants.FileAllowedExtensions.Office_Word.Contains(fileExtension))
                {
                    allowedType = Domain.Enums.FileAllowedTypeEnum.Office_Word;
                }
                else if (Domain.Constants.FileAllowedExtensions.Office_Excel.Contains(fileExtension))
                {
                    allowedType = Domain.Enums.FileAllowedTypeEnum.Office_Excel;
                }
                else if (Domain.Constants.FileAllowedExtensions.Office_PowerPoint.Contains(fileExtension))
                {
                    allowedType = Domain.Enums.FileAllowedTypeEnum.Office_PowerPoint;
                }
            }

            return await Task.FromResult(allowedType);
        }

        #endregion
    }
}
