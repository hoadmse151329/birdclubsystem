var day = [];
let numOfweek = document.getElementById('day');
console.log(numOfweek)
for (let i = 1; i < numOfweek.length; i++) {
  day.push({x : Date.parse(numOfweek.options[i].innerText), y : numOfweek.options[i].value})
}

var month = [];
let numOfmonth = document.getElementById('month');
for (let i = 0; i < numOfmonth.length; i++) {
  month.push({x : Date.parse(numOfmonth.options[i].innerText), y : numOfmonth.options[i].value})
}
var year = [];
let numOfyear = document.getElementById('year');
for (let i = 0; i < numOfyear.length; i++) {
    year.push({ x: Date.parse(numOfyear.options[i].innerText), y: numOfyear.options[i].value })
}
const ctx = document.getElementById('numOfAccess').getContext('2d');
const chart = new Chart(ctx, {   
        type: 'line',
        data: {
            
            datasets: [{
                label: 'number of access',
                data: day,
                backgroundColor: [
                    'rgba(39, 174, 96,0.5)'
                ],
                borderColor: [
                    'rgba(39, 174, 96,1.0)'
    
                ],
                borderWidth: 1,
                lineTension: 0.25,
            }]
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

 
  //change-chart
  function changeFrame(period) {
    if (period.value === 'day') {
      chart.options.scales.x.time.unit = period.value;
      chart.data.datasets[0].data = day;

    }
    if (period.value === 'day1') {
      chart.data.datasets[0].data = month;
    }
    chart.update();
  } 