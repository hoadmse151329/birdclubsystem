
///statical
const numOfHome = document.querySelectorAll('#numOfHome select');
var num = [];

var nameOfHome = [];
for (let i = 0; i < numOfHome.length; i++) {
    eval ('var array' + i + '= []');
    var data = document.getElementById(numOfHome[i].id);
    
    nameOfHome.push(data.name);
    
    for (let j = 0; j < data.length; j++) {
      eval('array' + i).push({x : Date.parse(data.options[j].innerText), y : data.options[j].value})
    }  
    num.push(eval('array'+ i));
}
  const myCtx = document.getElementById('chart-Dashboard').getContext('2d');
  const myChart = new Chart(myCtx, {   
      type: 'line',
      data: {
          datasets: []
      },
      options: {
          scales: {
              x: {
                  type: 'time',
                  time: {
                      unit: 'day'
                  }
              },
              y: {
                  beginAtZero: true
              }
          }
      }
  }); 

    for (let i = 0; i < numOfHome.length; i++) {
      
      let maxVal = 0xFFFFFF; 
      let randomNumber = Math.random() * maxVal; 
      randomNumber = Math.floor(randomNumber);
      randomNumber = randomNumber.toString(16);
      let randColor = randomNumber.padStart(6, 0);   

      const newData = {
        label: `${nameOfHome[i]}`,
        data: num[i],
        backgroundColor: [
            `#${randColor}`
        ],
        borderColor: [
            `#${randColor}`
    
        ],
        borderWidth: 1,
      }
    
      myChart.data.datasets.push(newData);
      myChart.update();
    }
 
  //change-chart
  function timeFrame(period) {
    if (period.value === 'day') {
      myChart.data.datasets[0].data = day;
      myChart.options.scales.x.time.unit = period.value;

    }
    if (period.value === 'week') {
      myChart.data.datasets[0].data = week;
      myChart.options.scales.x.time.unit = period.value;

    }
    if (period.value === 'month') {
      myChart.data.datasets[0].data = month;
      myChart.options.scales.x.time.unit = period.value;
    }
    myChart.update();

    period.classList.addClass('active');
} 
const ctx = document.getElementById('chart-board').getContext('2d');
const chart = new Chart(ctx, {
    type: 'line',
    data: {
        datasets: []
    },
    options: {
        scales: {
            x: {
                type: 'time',
                time: {
                    unit: 'day'
                }
            },
            y: {
                beginAtZero: true
            }
        }
    }
});

for (let i = 0; i < numOfHome.length; i++) {

    let maxVal = 0xFFFFFF;
    let randomNumber = Math.random() * maxVal;
    randomNumber = Math.floor(randomNumber);
    randomNumber = randomNumber.toString(16);
    let randColor = randomNumber.padStart(6, 0);

    const newData1 = {
        label: `${nameOfHome[i]}`,
        data: num[i],
        backgroundColor: [
            `#${randColor}`
        ],
        borderColor: [
            `#${randColor}`

        ],
        borderWidth: 1,
    }

    chart.data.datasets.push(newData1);
    chart.update();
}

//change-chart
function timeFrame(period) {
    if (period.value === 'day') {
        chart.data.datasets[0].data = day;
        chart.options.scales.x.time.unit = period.value;

    }
    if (period.value === 'week') {
        chart.data.datasets[0].data = week;
        chart.options.scales.x.time.unit = period.value;

    }
    if (period.value === 'month') {
        chart.data.datasets[0].data = month;
        chart.options.scales.x.time.unit = period.value;
    }
    chart.update();

    period.classList.addClass('active');
}