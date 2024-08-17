
const varFont = 'IRANSansWeb';

function createChart(elementId, optionChart) {

    //var myChart = echarts.init(document.getElementById(`${elementId}`));
    var myChart = echarts.init(document.getElementById(`${elementId}`), null, {
        height: 400
    });
    myChart.setOption(optionChart);
};

//-----------------------------------------------------------------------------
function lineCategoryChart(elementId, titleText, titleSubtext, yAxisName, xAxisName, axisData, seriesArray) {

    let seriesItems = [];

    seriesArray.forEach((arr) => {
        seriesItems.push(
            {
                name: `${arr.name}`,
                type: 'line',
                data: arr.data,
                smooth: true,
                label: {
                    show: arr.label,
                },
                lineStyle: {
                    color: `${arr.color}`,
                    type: `${arr.type}`,
                },
                itemStyle: {
                    color: `${arr.color}`,
                },
                emphasis: {
                    focus: 'series'
                },
            }
        );
    });

    var option = {
        title: {
            //text: titleText,
            subtext: titleSubtext,
            left: 'center',
        },
        textStyle: {
            fontFamily: varFont,
        },
        tooltip: {
            trigger: 'axis',
        },
        legend: {
            top: 10,
            left: '20%',
            align: 'right',
            icon: 'circle',
            textStyle: {
                fontSize: 8,
            },
        },
        yAxis: {
            name: yAxisName,
            type: 'value',
            position: 'left',
            alignTicks: true,
            axisLine: {
                show: true,
            },
        },
        xAxis: {
            name: xAxisName,
            type: 'category',
            boundaryGap: false,
            data: axisData,

        },
        series: seriesItems,
    };

    createChart(elementId, option);
};

function barCategoryChart(elementId, titleText, titleSubtext, xAxisName, axisData, seriesArray) {

    // وقتی متغیر آرایه را به متد ارسال نمودی این چند خط(آرایه) کامنت شود
    //seriesArray = [
    //    { name: 'جنگل', data: [320, 332, 301, 334, 390] },
    //    { name: 'کوهستان', data: [220, 182, 191, 234, 290] },
    //    { name: 'کویر', data: [150, 232, 201, 154, 190] },
    //    { name: 'دشت', data: [98, 77, 101, 99, 40] },
    //];

    let seriesItems = [];

    seriesArray.forEach((arr) => {
        seriesItems.push(
            {
                name: `${arr.name}`,
                type: 'bar',
                barGap: 0,
                label: {
                    show: true,
                    position: 'insideBottom',
                    distance: 15,
                    align: 'left',
                    verticalAlign: 'middle',
                    rotate: 90,
                    //formatter: '{a} ({c})',
                    formatter: '{c}',
                    rich: {
                        name: {}
                    }
                },
                emphasis: {
                    focus: 'series'
                },
                data: arr.data
            }
        );
    });

    var option = {
        title: {
            //text: titleText,
            subtext: titleSubtext,
            left: 'center',
        },
        textStyle: {
            fontFamily: varFont,
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        legend: {
            top: 10,
            left: 'center',
        },
        xAxis: [
            {
                name: xAxisName,
                type: 'category',
                // وقتی متغیر زیر را به متد ارسال نمودی این خط کامنت شود و خط زیری فعال گردد
                //data: ['2012', '2013', '2014', '2015', '2016']
                data: axisData,
            }
        ],
        yAxis: [
            {
                type: 'value'
            }
        ],

        series: seriesItems,
    };

    createChart(elementId, option);
};

function barStackedCategoryChart(elementId, titleText, titleSubtext, xAxisName, axisData, seriesArray) {

    let seriesItems = [];

    seriesArray.forEach((arr) => {
        seriesItems.push(
            {
                name: `${arr.name}`,
                type: 'bar',
                stack: 'total',
                label: {
                    show: true,
                    position: 'insideBottom',
                    distance: 15,
                    align: 'left',
                    verticalAlign: 'middle',
                    rotate: 90,
                },
                emphasis: {
                    focus: 'series'
                },
                data: arr.data
            }
        );
    });

    var option = {
        title: {
            //text: titleText,
            subtext: titleSubtext,
            left: 'center',
        },
        textStyle: {
            fontFamily: varFont,
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        legend: {
            top: 10,
            left: 'center',
        },
        grid: {
            left: 100,
            right: 100,
            top: 50,
            bottom: 50
        },
        xAxis: {
            name: xAxisName,
            type: 'category',
            data: axisData,
        },
        yAxis: {
            type: 'value'
        },

        series: seriesItems,
    };

    createChart(elementId, option);
};

function barStackedHorizontalCategoryChart(elementId, titleText, titleSubtext, xAxisName, axisData, seriesArray) {

    let seriesItems = [];

    seriesArray.forEach((arr) => {
        seriesItems.push(
            {
                name: `${arr.name}`,
                type: 'bar',
                stack: 'total',
                label: {
                    show: true,
                },
                emphasis: {
                    focus: 'series'
                },
                data: arr.data
            }
        );
    });

    var option = {
        title: {
            //text: titleText,
            subtext: titleSubtext,
            left: 'center',
        },
        textStyle: {
            fontFamily: varFont,
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        legend: {
            top: 10,
            left: 'center',
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: [
            {
                type: 'value'
            }
        ],
        yAxis: [
            {
                name: xAxisName,
                type: 'category',
                data: axisData,
            }
        ],

        series: seriesItems,
    };

    createChart(elementId, option);
};

