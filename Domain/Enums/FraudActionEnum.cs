using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum FraudActionEnum : int
    {
        /// <summary>
        /// مسدودسازی سیستمی Id:1
        /// </summary>
        SystemBlock = 10,

        /// <summary>
        /// محدودسازی سیستمی Id:2
        /// </summary>
        SystemLimit = 20,

        /// <summary>
        /// مسدودسازی دستی Id:3
        /// </summary>
        ManualBlock = 30,

        /// <summary>
        /// محدودسازی دستی Id:4
        /// </summary>
        ManualLimit = 40,

        /// <summary>
        /// ارسال به شعبه Id:5
        /// </summary>
        SendToBranch = 50,

        /// <summary>
        /// دریافت از شعبه Id:6
        /// </summary>
        ReceiveFromBranch = 60,

        /// <summary>
        /// ثبت نهایی تقلب Id:7
        /// </summary>
        Finalization = 70,

        /// <summary>
        /// لغو مسدودسازی سیستمی Id:9
        /// </summary>
        SystemBlockCancel = 90,

        /// <summary>
        /// لغو محدودسازی سیستمی Id:10
        /// </summary>
        SystemLimitCancel = 100,

        /// <summary>
        /// لغو مسدودسازی دستی Id:11
        /// </summary>
        ManualBlockCancel = 110,

        /// <summary>
        /// لغو محدودسازی دستی Id:12
        /// </summary>
        ManualLimitCancel = 120

    }
}

/*
{
    Id = 8,
    Code = 80,
    Title = "رد نهایی تقلب",
    Sort = 8,
    IsActive = true
},
*/